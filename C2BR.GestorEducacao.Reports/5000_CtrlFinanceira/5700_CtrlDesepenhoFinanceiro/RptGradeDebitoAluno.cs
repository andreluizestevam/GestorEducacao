using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5700_CtrlDesepenhoFinanceiro
{
    public partial class RptGradeDebitoAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totGeralValorTitul = 0;

        public RptGradeDebitoAluno()
        {
            InitializeComponent();
        }

        #region Init Report
        //strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF
        public int InitReport(string parametros,
                                 int strP_CO_EMP,
                                 string strP_CO_ALU,
                                 string strP_CO_AN0_REF,
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


                #region Query
                int intCO_ALU = (strP_CO_ALU != "T") ? Convert.ToInt32(strP_CO_ALU) : 0;

                var lst = from c in ctx.TB47_CTA_RECEB
                          from a in
                              (from a in ctx.TB07_ALUNO
                               where c.CO_ALU == a.CO_ALU && c.CO_EMP == a.CO_EMP
                               select a).DefaultIfEmpty()
                          from t in
                              (from t in ctx.TB01_CURSO
                               where c.CO_CUR == t.CO_CUR && c.CO_EMP == t.CO_EMP && c.CO_MODU_CUR == t.CO_MODU_CUR
                               select t).DefaultIfEmpty()
                          where c.IC_SIT_DOC == "A"
                                && c.DT_VEN_DOC < DateTime.Now
                                && c.CO_EMP == strP_CO_EMP
                                && (strP_CO_AN0_REF != "T" ? c.CO_ANO_MES_MAT == strP_CO_AN0_REF : strP_CO_AN0_REF == "T")
                                && (strP_CO_ALU != "T" ? c.CO_ALU == intCO_ALU : strP_CO_ALU == "T")
                                && (strP_CO_AGRUP != 0 ? c.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                          group c by new
                          {
                              a.CO_ALU,
                              a.NO_ALU,
                              a.NU_CPF_ALU,
                              a.CO_RG_ALU,
                              a.NU_TELE_RESI_ALU,
                              c.CO_CUR,
                              t.NO_CUR
                          } into g
                          orderby g.Key.NO_ALU, g.Key.CO_CUR
                          select new RelDebitoAluno
                          {
                              Aluno = g.Key.NO_ALU,
                              Telefone = (g.Key.NU_TELE_RESI_ALU != "" ? g.Key.NU_TELE_RESI_ALU : "-"),
                              Serie = (g.Key.NO_CUR != "" ? g.Key.NO_CUR : "-"),
                              Vr_Debito = g.Sum(p => p.IC_SIT_DOC == "A" ? p.VR_PAR_DOC : 0)
                              //Vr_Pag = g.Sum(p => p.VR_PAG.HasValue ? p.VR_PAG.Value : 0),
                              //Vr_Mul_Doc = g.Sum(p => p.CO_FLAG_TP_VALOR_MUL == "V" ? (p.VR_MUL_DOC.HasValue ? p.VR_MUL_DOC.Value : 0) :
                              //    p.CO_FLAG_TP_VALOR_MUL == "P" ? ((p.VR_PAR_DOC * (p.VR_MUL_DOC.HasValue ? p.VR_MUL_DOC.Value : 0)) / 100) : 0),
                              //Vr_Jur_Doc = g.Sum(p => p.CO_FLAG_TP_VALOR_JUR == "V" ? (p.VR_JUR_DOC.HasValue ? p.VR_JUR_DOC.Value : 0) :
                              //p.CO_FLAG_TP_VALOR_JUR == "P" ? ((p.VR_PAR_DOC * (p.VR_JUR_DOC.HasValue ? p.VR_JUR_DOC.Value : 0)) / 100) : 0),
                              //Vr_Par_Doc = g.Sum(p => p.VR_PAR_DOC)
                          };

                var res = lst.ToList();
                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os titulos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelDebitoAluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }
        #endregion

        #region Classe
        public class RelDebitoAluno
        {
            public string Aluno { get; set; }
            public string Telefone { get; set; }
            public string Serie { get; set; }
            public decimal Vr_Debito { get; set; }
            //public decimal Vr_Pag { get; set; }
            //public decimal Vr_Mul_Doc { get; set; }
            //public decimal Vr_Jur_Doc { get; set; }
            //public decimal Vr_Par_Doc { get; set; }
        }
        #endregion

        private void lblDebtioParc_AfterPrint(object sender, EventArgs e)
        {
            totGeralValorTitul = totGeralValorTitul + decimal.Parse(lblDebtioParc.Text);
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotValorTitulos.Text = String.Format("{0:#,##0.00}", totGeralValorTitul);
        }

    }
}
