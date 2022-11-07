using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5700_CtrlDesepenhoFinanceiro
{
    public partial class RptGradeAnualRecebiSerieTurma : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptGradeAnualRecebiSerieTurma()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              string strP_ANO_REF,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              int strP_CO_ALU,
                              int strP_CO_AGRUP,
                              string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada
                DateTime dtMax = new DateTime(DateTime.Now.Year, 12, 31);

                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                           where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && tb47.CO_EMP == strP_CO_EMP_REF
                           && tb47.IC_SIT_DOC != "C"
                           && tb47.DT_VEN_DOC <= dtMax
                           && (strP_ANO_REF != "T" ? tb47.CO_ANO_MES_MAT == strP_ANO_REF : strP_ANO_REF == "T")
                           && (strP_CO_MODU_CUR != 0 ? tb47.CO_MODU_CUR == strP_CO_MODU_CUR : strP_CO_MODU_CUR == 0)
                           && (strP_CO_CUR != 0 ? tb47.CO_CUR == strP_CO_CUR : strP_CO_CUR == 0)
                           && (strP_CO_TUR != 0 ? tb47.CO_TUR == strP_CO_TUR : strP_CO_TUR == 0)
                           && (strP_CO_ALU != 0 ? tb47.CO_ALU == strP_CO_ALU : strP_CO_ALU == 0)
                           && (strP_CO_AGRUP != 0 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                           group tb47 by tb07.NO_ALU into g
                           orderby g.Key
                           select new RelTitulosAluno
                           {
                               Aluno = g.Key,
                               TotalValor1 = g.Where(p => p.DT_VEN_DOC.Month == 1).Sum(p => p.VR_PAR_DOC),
                               TotalValor2 = g.Where(p => p.DT_VEN_DOC.Month == 2).Sum(p => p.VR_PAR_DOC),
                               TotalValor3 = g.Where(p => p.DT_VEN_DOC.Month == 3).Sum(p => p.VR_PAR_DOC),
                               TotalValor4 = g.Where(p => p.DT_VEN_DOC.Month == 4).Sum(p => p.VR_PAR_DOC),
                               TotalValor5 = g.Where(p => p.DT_VEN_DOC.Month == 5).Sum(p => p.VR_PAR_DOC),
                               TotalValor6 = g.Where(p => p.DT_VEN_DOC.Month == 6).Sum(p => p.VR_PAR_DOC),
                               TotalValor7 = g.Where(p => p.DT_VEN_DOC.Month == 7).Sum(p => p.VR_PAR_DOC),
                               TotalValor8 = g.Where(p => p.DT_VEN_DOC.Month == 8).Sum(p => p.VR_PAR_DOC),
                               TotalValor9 = g.Where(p => p.DT_VEN_DOC.Month == 9).Sum(p => p.VR_PAR_DOC),
                               TotalValor10 = g.Where(p => p.DT_VEN_DOC.Month == 10).Sum(p => p.VR_PAR_DOC),
                               TotalValor11 = g.Where(p => p.DT_VEN_DOC.Month == 11).Sum(p => p.VR_PAR_DOC),
                               TotalValor12 = g.Where(p => p.DT_VEN_DOC.Month == 12).Sum(p => p.VR_PAR_DOC),

                               CorValor1 = g.Where(p => p.DT_VEN_DOC.Month == 1)
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor2 = g.Where(p => p.DT_VEN_DOC.Month == 2)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor3 = g.Where(p => p.DT_VEN_DOC.Month == 3)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor4 = g.Where(p => p.DT_VEN_DOC.Month == 4)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor5 = g.Where(p => p.DT_VEN_DOC.Month == 5)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor6 = g.Where(p => p.DT_VEN_DOC.Month == 6)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor7 = g.Where(p => p.DT_VEN_DOC.Month == 7)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor8 = g.Where(p => p.DT_VEN_DOC.Month == 8)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor9 = g.Where(p => p.DT_VEN_DOC.Month == 9)                        
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor10 = g.Where(p => p.DT_VEN_DOC.Month == 10)                      
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor11 = g.Where(p => p.DT_VEN_DOC.Month == 11)                      
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,
                               CorValor12 = g.Where(p => p.DT_VEN_DOC.Month == 12)                      
                                                .Any(p => p.IC_SIT_DOC == "A") ? Color.Red.Name : Color.Blue.Name,

                               TotalValorAberto = g.Where(p => p.IC_SIT_DOC == "A").Sum(p => p.VR_PAR_DOC),
                               TotalValorPago = g.Where(p => p.IC_SIT_DOC == "Q").Sum(p => p.VR_PAR_DOC)
                           }).OrderBy(p => p.Aluno);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os titulos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelTitulosAluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Titulos Por Alunos

        public class RelTitulosAluno
        {
            public string Aluno { get; set; }
            public DateTime DataVencimento { get; set; }
            public string Documento { get; set; }
            public decimal? TotalValor1 { get; set; }
            public decimal? TotalValor2 { get; set; }
            public decimal? TotalValor3 { get; set; }
            public decimal? TotalValor4 { get; set; }
            public decimal? TotalValor5 { get; set; }
            public decimal? TotalValor6 { get; set; }
            public decimal? TotalValor7 { get; set; }
            public decimal? TotalValor8 { get; set; }
            public decimal? TotalValor9 { get; set; }
            public decimal? TotalValor10 { get; set; }
            public decimal? TotalValor11 { get; set; }
            public decimal? TotalValor12 { get; set; }
            public string CorValor1 { get; set; }
            public string CorValor2 { get; set; }
            public string CorValor3 { get; set; }
            public string CorValor4 { get; set; }
            public string CorValor5 { get; set; }
            public string CorValor6 { get; set; }
            public string CorValor7 { get; set; }
            public string CorValor8 { get; set; }
            public string CorValor9 { get; set; }
            public string CorValor10 { get; set; }
            public string CorValor11 { get; set; }
            public string CorValor12 { get; set; }
            public decimal? TotalValorAberto { get; set; }
            public decimal? TotalValorPago { get; set; }
        }
        #endregion

        #region Label events

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cel = sender as XRTableCell;
            string cor = cel.Tag as string;
            cel.ForeColor = Color.FromName(cor);
        } 
        #endregion
    }
}
