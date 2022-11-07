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
    public partial class RptRelatorioMediaAlunos : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelatorioMediaAlunos()
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
                              bool mostraAgrupadora
                              )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

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
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_MAT equals tb43.CO_MAT
                           where tb079.CO_MODU_CUR == strP_CO_MODU_CUR && tb079.CO_CUR == strP_CO_CUR && tb079.CO_TUR == strP_CO_TUR
                           && (tb079.CO_ANO_REF == strP_CO_ANO_MES_MAT)
                           && (strP_CO_ALU != 0 ? tb079.CO_ALU == strP_CO_ALU : 0 == 0)
                           && (strP_CO_MAT != 0 ? tb079.CO_MAT == strP_CO_MAT : 0 == 0)
                           && tb079.CO_EMP == strP_CO_EMP_REF
                           && tb43.CO_CUR == tb079.CO_CUR
                           && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                           && tb43.CO_MAT == tb079.CO_MAT
                           && tb43.CO_EMP == tb079.CO_EMP
                            //mostra ou não as diciplinas agrupadoras e agrupadas de acordo com bool recebido como parâmetro da página
                           && (mostraAgrupadora ? tb43.ID_MATER_AGRUP == null : ((tb43.FL_DISCI_AGRUPA == "N") || (tb43.FL_DISCI_AGRUPA == null)))
                               //Listar as disciplinas diferentes da classificação "Não se Aplica"
                           && cadMat.CO_CLASS_BOLETIM != 4
                           select new MediaAluno
                           {
                               ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                               nomeAlunoReceb = alu.NO_ALU.ToUpper(),
                               NireAluno = alu.NU_NIRE,
                               DisciplinaReceb = cadMat.NO_MATERIA.ToUpper(),
                               MediaB1 = tb079.VL_MEDIA_BIM1,
                               MediaB2 = tb079.VL_MEDIA_BIM2,
                               MediaB3 = tb079.VL_MEDIA_BIM3,
                               MediaB4 = tb079.VL_MEDIA_BIM4,
                               NotaB1 = tb079.VL_NOTA_BIM1_ORI,
                               NotaB2 = tb079.VL_NOTA_BIM2_ORI,
                               NotaB3 = tb079.VL_NOTA_BIM3_ORI,
                               NotaB4 = tb079.VL_NOTA_BIM4_ORI,
                               RecupB1 = tb079.VL_RECU_BIM1,
                               RecupB2 = tb079.VL_RECU_BIM2,
                               RecupB3 = tb079.VL_RECU_BIM3,
                               RecupB4 = tb079.VL_RECU_BIM4,

                               //MediaRecuSem1 = tb079.VL_RECU_SEM1,
                               //MediaRecuSem2 = tb079.VL_RECU_SEM2,
                               MediaFinal = tb079.VL_MEDIA_FINAL
                           }
                  );

                var res = lst.Distinct().OrderBy(r => r.nomeAlunoReceb).ThenBy(o=>o.ordImp).ThenBy(r => r.DisciplinaReceb).ToList();

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

        #region Classe Média Aluno
        public class MediaAluno
        {
            public int? ordImp { get; set; }
            public int NireAluno { get; set; }
            public string nomeAlunoReceb { get; set; }
            public string NomeAluno
            {
                get
                {
                    return (this.nomeAlunoReceb.Length > 25 ? this.nomeAlunoReceb.Substring(0, 25) + "..." : this.nomeAlunoReceb);
                }
            }
            public string DescNIRE
            {
                get
                {
                    return this.NireAluno.ToString().PadLeft(7, '0');
                }
            }
            public string DisciplinaReceb { get; set; }
            public string Disciplina
            {
                get
                {
                    return (this.DisciplinaReceb.Length > 27 ? this.DisciplinaReceb.Substring(0, 27) + "..." : this.DisciplinaReceb);
                }
            }

            //public decimal? MediaB1 { get; set; }
            //public decimal? MediaB2 { get; set; }
            //public decimal? MediaRecuSem1 { get; set; }
            //public decimal? MediaB3 { get; set; }
            //public decimal? MediaB4 { get; set; }
            //public decimal? MediaRecuSem2 { get; set; }
            //public decimal? MediaFinal { get; set; }


            public decimal? MediaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public decimal? NotaB1 { get; set; }
            public decimal? NotaB2 { get; set; }
            public decimal? NotaB3 { get; set; }
            public decimal? NotaB4 { get; set; }
            public decimal? RecupB1 { get; set; }
            public decimal? RecupB2 { get; set; }
            public decimal? RecupB3 { get; set; }
            public decimal? RecupB4 { get; set; }
            public decimal? MediaFinal { get; set; }
            public decimal? RecupFinal { get; set; }
            public string NTB1
            {
                get
                {
                    decimal? d = this.NotaB1 != null ? Math.Round(this.NotaB1.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string NTB2
            {
                get
                {
                    decimal? d = this.NotaB2 != null ? Math.Round(this.NotaB2.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string NTB3
            {
                get
                {
                    decimal? d = this.NotaB3 != null ? Math.Round(this.NotaB3.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string NTB4
            {
                get
                {
                    decimal? d = this.NotaB4 != null ? Math.Round(this.NotaB4.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MB1
            {
                get
                {
                    decimal? d = this.MediaB1 != null ? Math.Round(this.MediaB1.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MB2
            {
                get
                {
                    decimal? d = this.MediaB2 != null ? Math.Round(this.MediaB2.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MB3
            {
                get
                {
                    decimal? d = this.MediaB3 != null ? Math.Round(this.MediaB3.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MB4
            {
                get
                {
                    decimal? d = this.MediaB4 != null ? Math.Round(this.MediaB4.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string NR1
            {
                get
                {
                    decimal? d = this.RecupB1 != null ? Math.Round(this.RecupB1.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string NR2
            {
                get
                {
                    decimal? d = this.RecupB2 != null ? Math.Round(this.RecupB2.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string NR3
            {
                get
                {
                    decimal? d = this.RecupB3 != null ? Math.Round(this.RecupB3.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string NR4
            {
                get
                {
                    decimal? d = this.RecupB4 != null ? Math.Round(this.RecupB4.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string RecupFinalValid
            {
                get
                {
                    decimal? d = this.RecupFinal != null ? Math.Round(this.RecupFinal.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }
            public string MediaFinalValid
            {
                get
                {
                    decimal? d = this.MediaFinal != null ? Math.Round(this.MediaFinal.Value, 1) : (decimal?)null;
                    return d != null ? d.ToString() : "-";
                }
            }

            public string MediaSemestre1
            {
                get
                {
                    if(this.MediaB1 != null && this.MediaB2 != null)
                    {
                        decimal aux = ((this.MediaB1 ?? 0) + (this.MediaB2 ?? 0));
                        decimal aux2 = aux/2;
                        return aux2.ToString("N1");
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }
            public string MediaSemestre2
            {
                get
                {
                    if (this.MediaB3 != null && this.MediaB4 != null)
                    {
                        decimal aux = ((this.MediaB3 ?? 0) + (this.MediaB4 ?? 0));
                        decimal aux2 = aux / 2;
                        return aux2.ToString("N1");
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }
        }

        #endregion
    }
}
