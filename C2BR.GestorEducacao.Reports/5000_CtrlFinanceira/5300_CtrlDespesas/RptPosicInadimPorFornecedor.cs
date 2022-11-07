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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas
{
    public partial class RptPosicInadimPorFornecedor : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region Fields

        private decimal totParcValorDocto, totParcValorMulta, totParcValorJuros, totParcValorDescto, totParcValorTotal, valorSumTitulos = 0;
        int qtdeTotalForn, qtdeTotalTitulo = 0; 

        #endregion

        #region ctor

        public RptPosicInadimPorFornecedor()
        {
            InitializeComponent();
        } 

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
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

                var lst = (from tb38 in ctx.TB38_CTA_PAGAR
                           where tb38.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && tb38.CO_EMP == strP_CO_EMP_REF
                           && tb38.IC_SIT_DOC == "A"
                           && tb38.DT_VEN_DOC < DateTime.Now
                           && (strP_CO_AGRUP != 0 ? tb38.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                           group tb38 by tb38.TB41_FORNEC.CO_CPFCGC_FORN into g
                           orderby g.Key
                           select new RelPosicInadPorForn
                           {
                               DocFornecedor = g.Key,
                               QtdeTitulos = g.Count(),
                               TotalValorTitulo = g.Sum(p => p.VR_PAR_DOC),
                               TotalValorMulta = g.Sum(p => (p.CO_FLAG_TP_VALOR_MUL == "V" ? p.VR_MUL_DOC ?? 0 : ((p.VR_PAR_DOC * p.VR_MUL_DOC) / 100) ?? 0)),
                               TotalValorJuros = g.Sum(p => (p.CO_FLAG_TP_VALOR_JUR == "V" ? p.VR_JUR_DOC ?? 0 : ((p.VR_PAR_DOC * p.VR_JUR_DOC) / 100) ?? 0)), //* DateTime.Now.Subtract(p.DT_VEN_DOC).Days) ?? 0)),
                               TotalValorDescto = g.Sum(p => (p.CO_FLAG_TP_VALOR_DES == "V" ? p.VR_DES_DOC ?? 0 : ((p.VR_PAR_DOC * p.VR_DES_DOC) / 100) ?? 0)),
                               Fornecedor = g.FirstOrDefault().TB41_FORNEC.NO_FAN_FOR,
                               Telefone1 = g.FirstOrDefault().TB41_FORNEC.CO_TEL1_FORN,
                               Telefone2 = g.FirstOrDefault().TB41_FORNEC.CO_TEL2_FORN
                           }).OrderBy(p => p.Fornecedor);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                this.qtdeTotalForn = res.Count;

                this.valorSumTitulos = (from tb38 in ctx.TB38_CTA_PAGAR
                                        where tb38.CO_EMP == strP_CO_EMP_REF
                                        && (tb38.IC_SIT_DOC == "A" || tb38.IC_SIT_DOC == "Q")
                                        && tb38.DT_VEN_DOC < DateTime.Now
                                        select new { tb38.VR_PAR_DOC }).Sum(p => p.VR_PAR_DOC);

                foreach (var iLst in res)
                {
                    iLst.DataVencimento = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                                           where tb38.TB41_FORNEC.CO_CPFCGC_FORN == iLst.DocFornecedor
                                           && tb38.CO_EMP == strP_CO_EMP_REF
                                           select tb38.DT_VEN_DOC).Min();

                    iLst.DataDif = DateTime.Now.Subtract(iLst.DataVencimento);
                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPosicInadPorForn at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Posição de Inadimplência por Fornecedor

        public class RelPosicInadPorForn
        {
            public string DocFornecedor { get; set; }
            public string Fornecedor { get; set; }
            public DateTime DataVencimento { get; set; }
            public string Documento { get; set; }
            public decimal TotalValorTitulo { get; set; }
            public decimal TotalValorMulta { get; set; }
            public decimal TotalValorJuros { get; set; }
            public decimal TotalValorDescto { get; set; }
            public int QtdeTitulos { get; set; }
            public TimeSpan DataDif { get; set; }
            public string Telefone1 { get; set; }
            public string Telefone2 { get; set; }

            public decimal? Juros { get; set; }
            public decimal? Multa { get; set; }
            public decimal? Desconto { get; set; }
            public string TpMulta { get; set; }
            public string TpJuros { get; set; }
            public string TpDesc { get; set; }
            public decimal? Result { get; set; }

            public int QtdeDias { get { return this.DataDif.Days; } }

            public string DocDesc
            {
                get
                {
                    if (this.DocFornecedor.Length == 11)
                        return Funcoes.Format(this.DocFornecedor, TipoFormat.CPF);
                    else
                        return Funcoes.Format(this.DocFornecedor, TipoFormat.CNPJ);
                }
            }

            public string Tel1Desc
            {
                get
                {
                    return this.Telefone1 != "" ? Funcoes.Format(this.Telefone1, TipoFormat.Telefone) : "";
                }
            }

            public string Tel2Desc
            {
                get
                {
                    return this.Telefone2 != "" ? Funcoes.Format(this.Telefone2, TipoFormat.Telefone) : "";
                }
            }

            public decimal? TotalDesc
            {
                get
                {

                    return this.TotalValorTitulo +
                        (this.TotalValorJuros) +
                        (this.TotalValorMulta) -
                        (this.TotalValorDescto);
                }
            }
        }

        #endregion

        #region Events

        private void lblValorDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorDocto.Text != "")
            {
                totParcValorDocto = totParcValorDocto + decimal.Parse(lblValorDocto.Text);
            }
        }

        private void lblMultaDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (lblMultaDocto.Text != "")
            {
                totParcValorMulta = totParcValorMulta + decimal.Parse(lblMultaDocto.Text);
            }
        }

        private void lblJurosDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (lblJurosDocto.Text != "")
            {
                totParcValorJuros = totParcValorJuros + decimal.Parse(lblJurosDocto.Text);
            }
        }

        private void lblDesctoDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (lblDesctoDocto.Text != "")
            {
                totParcValorDescto = totParcValorDescto + decimal.Parse(lblDesctoDocto.Text);
            }
        }

        private void lblValorTotDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (lblValorTotDocto.Text != "")
            {
                totParcValorTotal = totParcValorTotal + decimal.Parse(lblValorTotDocto.Text);
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblTotParcDocto.Text = String.Format("{0:#,##0.00}", totParcValorDocto);
            //lblTotParcMulta.Text = String.Format("{0:#,##0.00}", totParcValorMulta);
            //lblTotParcJuros.Text = String.Format("{0:#,##0.00}", totParcValorJuros);
            //lblTotParcDescto.Text = String.Format("{0:#,##0.00}", totParcValorDescto);
            //lblTotParcTot.Text = String.Format("{0:#,##0.00}", totParcValorTotal);

            lblDescDescTotal.Text = "(Qtde = Forn.: " + this.qtdeTotalForn.ToString() + " - Títulos: "
                + this.qtdeTotalTitulo.ToString() + ") (Títulos = Valor Médio: " + String.Format("{0:#,##0.00}", totParcValorDocto / this.qtdeTotalTitulo) + ")" +
                " (% Inadim.: " + String.Format("{0:#,##0.00}", (totParcValorDocto * 100) / this.valorSumTitulos) + "%)";
        }

        private void lblQTT_AfterPrint(object sender, EventArgs e)
        {
            qtdeTotalTitulo = qtdeTotalTitulo + int.Parse(lblQTT.Text);
        } 

        #endregion

        private void lblDeficiencia_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var obj = (XRTableCell)sender;

            if (int.Parse(obj.Text) > 0)
            {
                obj.ForeColor = Color.Red;
                obj.Text = "+ " + obj.Text.Replace("-", "").Replace("+", ""); 
            }
            else if (int.Parse(obj.Text) < 0)
            {
                obj.ForeColor = Color.Blue;
                obj.Text = "- " + obj.Text.Replace("-", "").Replace("+", "");
            }
            else
            {
                obj.Text = "";
            }
        }
    }
}
