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
    public partial class RptHistorFinancFornecedor : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totValorDocto, totValorMulta, totValorJuros, totValorDescto, totValorTotal, totValorPagto = 0;
        private decimal totValorPDocto, totValorPMulta, totValorPJuros, totValorPDescto, totValorPTotal, totValorPPagto = 0;

        public RptHistorFinancFornecedor()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              string strP_IC_SIT_DOC,
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
                           && (strP_CO_EMP_REF != 0 ? tb38.CO_EMP == strP_CO_EMP_REF : strP_CO_EMP_REF == 0)
                           && (strP_IC_SIT_DOC != "T" ? tb38.IC_SIT_DOC == strP_IC_SIT_DOC : strP_IC_SIT_DOC == "T")
                           && (strP_CO_AGRUP != 0 ? tb38.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                           select new RelHistoFinanFornec
                           {
                               Nome = tb38.TB41_FORNEC.NO_FAN_FOR,
                               CPFCNPJ = tb38.TB41_FORNEC.CO_CPFCGC_FORN,
                               TpFornec = tb38.TB41_FORNEC.TP_FORN,
                               Status = tb38.IC_SIT_DOC,
                               Documento = tb38.NU_DOC,
                               DataVencimento = tb38.DT_VEN_DOC,
                               DataPagamento = tb38.DT_REC_DOC,
                               ValorDocumento = tb38.VR_PAR_DOC,
                               Juros = tb38.VR_JUR_DOC,
                               Multa = tb38.VR_MUL_DOC,
                               Desconto = tb38.VR_DES_DOC,
                               TpMulta = tb38.CO_FLAG_TP_VALOR_MUL,
                               TpJuros = tb38.CO_FLAG_TP_VALOR_JUR,
                               TpDesc = tb38.CO_FLAG_TP_VALOR_DES,
                               JurosPago = tb38.VR_JUR_PAG,
                               MultaPago = tb38.VR_MUL_PAG,
                               DescontoPago = tb38.VR_DES_PAG,
                               ValorPago = tb38.VR_PAG,
                               Parcela = tb38.NU_PAR
                           }).OrderBy(p => p.Nome).ThenBy(p => p.DataVencimento).ThenBy(p => p.Parcela);

                var res = lst.ToList();

                foreach (var iLst in res)
                {
                    iLst.DataDif = DateTime.Now.Subtract(iLst.DataVencimento);
                }

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelHistoFinanFornec at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Histórico Financeiro do Fornecedor

        public class RelHistoFinanFornec
        {
            public string Nome { get; set; }
            public string TpFornec { get; set; }
            public string CPFCNPJ { get; set; }
            public DateTime DataVencimento { get; set; }
            public DateTime? DataPagamento { get; set; }
            public string Status { get; set; }
            public string Documento { get; set; }
            public decimal ValorDocumento { get; set; }
            public decimal? Juros { get; set; }
            public decimal? Multa { get; set; }
            public decimal? Desconto { get; set; }
            public string TpMulta { get; set; }
            public string TpJuros { get; set; }
            public string TpDesc { get; set; }
            public decimal? Result { get; set; }
            public decimal? ValorPago { get; set; }
            public decimal? JurosPago { get; set; }
            public decimal? MultaPago { get; set; }
            public decimal? DescontoPago { get; set; }
            public TimeSpan DataDif { get; set; }
            public int Parcela { get; set; }

            public decimal? JurosDesc
            {
                get
                {
                    if (this.Status == "A")
                    {
                        if (Convert.ToDecimal(this.DataDif.Days) > 0)
                        {
                            if (this.TpJuros == "P")
                            {
                                Result = ((this.ValorDocumento / 100) * this.Juros) * Convert.ToDecimal(this.DataDif.Days);
                                return Result;

                            }
                            else if (this.TpJuros == "V")
                            {
                                Result = (this.Juros) * Convert.ToDecimal(this.DataDif.Days);
                                return Result;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (this.Status == "Q")
                    {
                        return this.JurosPago;
                    }

                    return null;
                }
            }

            public decimal? MultaDesc
            {
                get
                {
                    if (this.Status == "A")
                    {
                        if (Convert.ToDecimal(this.DataDif.Days) > 0)
                        {
                            if (this.TpMulta == "P")
                            {
                                Result = ((this.ValorDocumento / 100) * this.Multa);
                                return Result;
                            }
                            else
                            {
                                return Multa;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (this.Status == "Q")
                    {
                        return this.MultaPago;
                    }

                    return null;
                }
            }

            public decimal? DescontoDesc
            {
                get
                {
                    if (this.Status == "A")
                    {
                        if (this.TpDesc == "P")
                        {
                            Result = ((this.ValorDocumento / 100) * this.Desconto);
                            return Result;
                        }
                        else
                        {
                            return this.Desconto;
                        }
                    }
                    else if (this.Status == "Q")
                    {
                        return this.DescontoPago;
                    }

                    return null;
                }
            }

            public decimal? TotalDesc
            {
                get
                {
                    if (this.Status == "Q")
                    {
                        return this.ValorPago;
                    }
                    else if (this.Status == "A" && (Convert.ToDecimal(this.DataDif.Days) > 0))
                    {
                        return this.ValorDocumento +
                            (this.JurosDesc != null ? this.JurosDesc : 0) +
                            (this.MultaDesc != null ? this.MultaDesc : 0) -
                            (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }
                    else if (this.Status == "A")
                    {
                        return this.ValorDocumento - (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }

                    return null;
                }
            }

            public string DescricaoDesc
            {
                get
                {
                    if (this.Status == "A")
                    {
                        return this.Documento + " / " + "Em Aberto";
                    }
                    else if (this.Status == "Q")
                    {
                        return this.Documento + " / " + "Quitado";
                    }
                    else if (this.Status == "C")
                    {
                        return this.Documento + " / " + "Cancelado";
                    }

                    return null;
                }
            }

            public string DiasDesc
            {
                get
                {
                    if (this.Status == "A" && (Convert.ToDecimal(this.DataDif.Days) > 0))
                    {
                        return "+" + Convert.ToDecimal(this.DataDif.Days).ToString();
                    }
                    else if (this.Status == "Q")
                    {
                        return "";
                    }
                    else if (this.Status == "C")
                    {
                        return "";
                    }

                    return (Convert.ToDecimal(this.DataDif.Days)).ToString();
                }
            }

            public string NomeDesc
            {
                get
                {
                    return this.TpFornec == "F" ? Funcoes.Format(this.CPFCNPJ, TipoFormat.CPF) + " - " + this.Nome : Funcoes.Format(this.CPFCNPJ, TipoFormat.CNPJ) + " - " + this.Nome;
                }
            }
        }
        #endregion

        private void DetailContent_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorDocto.Text != "")
            {
                totValorDocto = totValorDocto + decimal.Parse(lblValorDocto.Text);
                totValorPDocto = totValorPDocto + decimal.Parse(lblValorDocto.Text);                
            }
            if (lblValorPago.Text != "")
            {
                totValorPagto = totValorPagto + decimal.Parse(lblValorPago.Text);
                totValorPPagto = totValorPPagto + decimal.Parse(lblValorPago.Text);
            }
            if (lblJurosDocto.Text != "")
            {
                totValorJuros = totValorJuros + decimal.Parse(lblJurosDocto.Text);
                totValorPJuros = totValorPJuros + decimal.Parse(lblJurosDocto.Text);
            }
            if (lblMultaDocto.Text != "")
            {
                totValorMulta = totValorMulta + decimal.Parse(lblMultaDocto.Text);
                totValorPMulta = totValorPMulta + decimal.Parse(lblMultaDocto.Text);
            }
            if (lblDesctoDocto.Text != "")
            {
                totValorDescto = totValorDescto + decimal.Parse(lblDesctoDocto.Text);
                totValorPDescto = totValorPDescto + decimal.Parse(lblDesctoDocto.Text);
            }
            if (lblValorTotDocto.Text != "")
            {
                totValorTotal = totValorTotal + decimal.Parse(lblValorTotDocto.Text);
                totValorPTotal = totValorPTotal + decimal.Parse(lblValorTotDocto.Text);
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotParcDocto.Text = String.Format("{0:#,##0.00}", totValorDocto);
            lblTotParcMulta.Text = String.Format("{0:#,##0.00}", totValorMulta);
            lblTotParcJuros.Text = String.Format("{0:#,##0.00}", totValorJuros);
            lblTotParcDescto.Text = String.Format("{0:#,##0.00}", totValorDescto);
            lblTotParcTot.Text = String.Format("{0:#,##0.00}", totValorTotal);
            lblTotParcPago.Text = String.Format("{0:#,##0.00}", totValorPagto);
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblvp.Text = String.Format("{0:#,##0.00}", totValorPDocto);
            lblvmul.Text = String.Format("{0:#,##0.00}", totValorPMulta);
            lblvjur.Text = String.Format("{0:#,##0.00}", totValorPJuros);
            lblvdes.Text = String.Format("{0:#,##0.00}", totValorPDescto);
            lblvtot.Text = String.Format("{0:#,##0.00}", totValorPTotal);
            lblvpag.Text = String.Format("{0:#,##0.00}", totValorPPagto);

            totValorPDocto = totValorPMulta = totValorPJuros = totValorPDescto = totValorPTotal = totValorPPagto = 0;
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
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
}
