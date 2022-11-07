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
    public partial class RptRelacAlunosRecuperacaoBimestral : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacAlunosRecuperacaoBimestral()
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
                              int strP_CO_MAT,
                              string strP_CATEG
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
                           join tb01 in ctx.TB01_CURSO on tb079.CO_CUR equals tb01.CO_CUR
                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR && tb079.CO_CUR == strP_CO_CUR && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT)
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && tb079.CO_EMP == strP_CO_EMP_REF && tb079.CO_MAT == strP_CO_MAT
                               //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           && (strP_CATEG != "T" ?
                               //se for recuperação
                           strP_CATEG == "R" ?
                           ((tb079.VL_NOTA_BIM1_ORI != null ? tb079.VL_NOTA_BIM1_ORI < (tb01.MED_FINAL_CUR ?? 6) : false)
                           || (tb079.VL_NOTA_BIM2_ORI != null ? tb079.VL_NOTA_BIM2_ORI < (tb01.MED_FINAL_CUR ?? 6) : false)
                           || (tb079.VL_NOTA_BIM3_ORI != null ? tb079.VL_NOTA_BIM3_ORI < (tb01.MED_FINAL_CUR ?? 6) : false)
                           || (tb079.VL_NOTA_BIM4_ORI != null ? tb079.VL_NOTA_BIM4_ORI < (tb01.MED_FINAL_CUR ?? 6) : false))
                               //se for sem recuperação
                           : ((tb079.VL_NOTA_BIM1_ORI != null ? tb079.VL_NOTA_BIM1_ORI < (tb01.MED_FINAL_CUR ?? 6) : true)
                           || (tb079.VL_NOTA_BIM2_ORI != null ? tb079.VL_NOTA_BIM2_ORI < (tb01.MED_FINAL_CUR ?? 6) : true)
                           || (tb079.VL_NOTA_BIM3_ORI != null ? tb079.VL_NOTA_BIM3_ORI < (tb01.MED_FINAL_CUR ?? 6) : true)
                           || (tb079.VL_NOTA_BIM4_ORI != null ? tb079.VL_NOTA_BIM4_ORI < (tb01.MED_FINAL_CUR ?? 6) : true))
                           : 0 == 0)
                           select new AlunoRecuperacao
                           {
                               NomeAluno = alu.NO_ALU.ToUpper(),
                               NireAluno = alu.NU_NIRE,
                               MediaB1 = tb079.VL_NOTA_BIM1_ORI,
                               MediaB2 = tb079.VL_NOTA_BIM2_ORI,
                               MediaB3 = tb079.VL_NOTA_BIM3_ORI,
                               MediaB4 = tb079.VL_NOTA_BIM4_ORI,
                               MediaRecuBim1 = tb079.VL_RECU_BIM1,
                               MediaRecuBim2 = tb079.VL_RECU_BIM2,
                               MediaRecuBim3 = tb079.VL_RECU_BIM3,
                               MediaRecuBim4 = tb079.VL_RECU_BIM4,
                               MedMinima = tb01.MED_FINAL_CUR ?? 6,
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
        public class AlunoRecuperacao
        {
            public int NireAluno { get; set; }
            public string NomeAluno { get; set; }
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public decimal? MediaRecuBim1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaRecuBim2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaRecuBim3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? MediaRecuBim4 { get; set; }
            public decimal MedMinima { get; set; }

            public string DescNIRE
            {
                get
                {
                    return this.NireAluno.ToString().PadLeft(9, '0');
                }
            }

            public string DescRecNtr1
            {
                get
                {
                    return (this.MediaB1 != null ? (this.MediaB1 >= this.MedMinima ? "Não" : "Sim") : "XXX")
                        + " / " + (this.MediaRecuBim1 != null ? "Sim" : "Não");
                }
            }
            public string DescRecNtr2
            {
                get
                {
                    return (this.MediaB2 != null ? (this.MediaB2 >= this.MedMinima ? "Não" : "Sim") : "XXX")
                        + " / " + (this.MediaRecuBim2 != null ? "Sim" : "Não");
                }
            }
            public string DescRecNtr3
            {
                get
                {
                    return (this.MediaB3 != null ? (this.MediaB3 >= this.MedMinima ? "Não" : "Sim") : "XXX")
                        + " / " + (this.MediaRecuBim3 != null ? "Sim" : "Não");
                }
            }
            public string DescRecNtr4
            {
                get
                {
                    return (this.MediaB4 != null ? (this.MediaB4 >= this.MedMinima ? "Não" : "Sim") : "XXX")
                        + " / " + (this.MediaRecuBim4 != null ? "Sim" : "Não");
                }
            }
        }

        #endregion
    }
}
