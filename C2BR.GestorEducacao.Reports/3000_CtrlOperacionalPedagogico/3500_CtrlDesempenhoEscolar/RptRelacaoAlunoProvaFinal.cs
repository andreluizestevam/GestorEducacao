using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar
{
    public partial class RptRelacaoAlunoProvaFinal : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacaoAlunoProvaFinal()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int coEmp,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              int strP_CO_ALU,
                              int strP_CO_MAT
                              )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisiblePageHeader = true;
                this.VisibleNumeroPage = false;
                this.VisibleDataHeader = false;
                this.VisibleHoraHeader = false;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Aluno Recuperação

                var lst = (from tb079 in ctx.TB079_HIST_ALUNO
                           join alu in ctx.TB07_ALUNO on tb079.CO_ALU equals alu.CO_ALU
                           join mat in ctx.TB02_MATERIA on tb079.CO_MAT equals mat.CO_MAT
                           join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA
                           join tb25 in ctx.TB25_EMPRESA on tb079.CO_EMP equals tb25.CO_EMP
                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR && tb079.CO_CUR == strP_CO_CUR && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT)
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && tb079.CO_EMP == strP_CO_EMP_REF && tb079.CO_MAT == strP_CO_MAT
                           //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           && (tb079.VL_NOTA_BIM1 != null && tb079.VL_NOTA_BIM2 != null && tb079.VL_NOTA_BIM3 != null && tb079.VL_NOTA_BIM4 != null ?
                           (tb079.VL_NOTA_BIM1 + tb079.VL_NOTA_BIM2 + tb079.VL_NOTA_BIM3 + tb079.VL_NOTA_BIM4) < 24 :
                           false)
                           select new AlunoProvaFinal
                           {
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               NireAluno = alu.NU_NIRE,
                               MediaB1 = tb079.VL_NOTA_BIM1,
                               MediaB2 = tb079.VL_NOTA_BIM2,
                               MediaB3 = tb079.VL_NOTA_BIM3,
                               MediaB4 = tb079.VL_NOTA_BIM4,
                               MS1 = (tb079.VL_NOTA_BIM1 + tb079.VL_NOTA_BIM2)/2,
                               MS2 = (tb079.VL_NOTA_BIM3 + tb079.VL_NOTA_BIM4) / 2
                           }
                  );

                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ToList();

                #endregion

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Aluno Recuperação
        public class AlunoProvaFinal
        {
            public int NireAluno { get; set; }
            public string NomeAluno { get; set; }
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MS1 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? MS2 { get; set; }

            public string DescNIRE
            {
                get
                {
                    return this.NireAluno.ToString().PadLeft(9, '0');
                }
            }
            
            public string Pts
            {
                get
                {
                    return this.MediaB1 != null && this.MediaB2 != null && this.MediaB3 != null && this.MediaB4 != null ? 
                        ((((this.MediaB1 + this.MediaB2 + this.MediaB3 + this.MediaB4) - 30) / 2)*(-1)).Value.ToString("N1") : "-";
                }
            }
        }

        #endregion
    }
}
