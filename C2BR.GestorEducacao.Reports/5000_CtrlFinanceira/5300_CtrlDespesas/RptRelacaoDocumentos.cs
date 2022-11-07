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
    public partial class RptRelacaoDocumentos : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        private decimal totGeralValorDocto, totGeralValorMulta, totGeralValorJuros, totGeralValorDescto, totGeralValorTotal = 0;
        private string TipoPesq;

        public RptRelacaoDocumentos()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              string strTipoPesq,
                              int strP_CO_FORN,
                              int strP_CO_HIST,
                              string strP_IC_SIT_DOC,
                              DateTime strP_DT_INI,
                              DateTime strP_DT_FIM,
                              string strP_DT_VECTO,
                              string strP_NU_DOC,
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

                TipoPesq = strTipoPesq;

                #region Query Contas a Pagar Parametrizada

                DateTime dtVencto = strP_DT_VECTO != "" ? DateTime.Parse(strP_DT_VECTO) : DateTime.Now;

                var lst = (from tb38 in ctx.TB38_CTA_PAGAR
                           where tb38.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strP_NU_DOC != "" ? tb38.NU_DOC == strP_NU_DOC : strP_NU_DOC == "")
                           && tb38.CO_EMP == strP_CO_EMP_REF
                           && (strP_IC_SIT_DOC != "T" ? tb38.IC_SIT_DOC == strP_IC_SIT_DOC : strP_IC_SIT_DOC == "T")
                           && (strP_CO_FORN != 0 ? tb38.TB41_FORNEC.CO_FORN == strP_CO_FORN : strP_CO_FORN == 0)
                           && (strP_CO_HIST != 0 ? tb38.TB39_HISTORICO.CO_HISTORICO == strP_CO_HIST : strP_CO_HIST == 0)
                           && (TipoPesq == "M" ? tb38.DT_CAD_DOC : (TipoPesq == "V" ?
                                        tb38.DT_VEN_DOC : TipoPesq == "P" ? tb38.DT_REC_DOC : tb38.DT_CAD_DOC)) >= strP_DT_INI
                           && (TipoPesq == "M" ? tb38.DT_CAD_DOC : (TipoPesq == "V" ?
                                        tb38.DT_VEN_DOC : TipoPesq == "P" ? tb38.DT_REC_DOC : tb38.DT_CAD_DOC)) <= strP_DT_FIM                           
                          // && (strP_DT_VECTO != "" ? tb38.DT_VEN_DOC.Year == dtVencto.Year && tb38.DT_VEN_DOC.Month == dtVencto.Month && tb38.DT_VEN_DOC.Day == dtVencto.Day : strP_DT_VECTO == "")
                           && (strP_CO_AGRUP != 0 ? tb38.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                           select new RelTitulosPagar
                           {
                               Identificacao = tb38.TB41_FORNEC.CO_CPFCGC_FORN,
                               TpForn = tb38.TB41_FORNEC.TP_FORN,
                               Data = (TipoPesq == "M" ? tb38.DT_CAD_DOC : (TipoPesq == "V" ?
                                        tb38.DT_VEN_DOC : TipoPesq == "P" ? tb38.DT_REC_DOC : tb38.DT_CAD_DOC)),
                               Status = tb38.IC_SIT_DOC ,
                               Documento = tb38.NU_DOC,
                               DataMov = tb38.DT_CAD_DOC,
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
                               Historico = tb38.TB39_HISTORICO.DE_HISTORICO,
                               Parcela = tb38.NU_PAR
                           }).OrderBy(p => p.Data);

                var res = lst.ToList();

                foreach (var iLst in res)
                {
                    iLst.DataDif = DateTime.Now.Subtract(iLst.DataVencimento);
                }

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os titulos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelTitulosPagar at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Relacao de Títulos a Pagar

        public class RelTitulosPagar
        {
            public string Identificacao { get; set; }
            public string TpForn { get; set; }
            public string Historico { get; set; }
            public DateTime? Data { get; set; }
            public DateTime DataMov { get; set; }
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

            public string CNPJCPFDesc
            {
                get
                {
                    return this.TpForn == "F" ? Funcoes.Format(this.Identificacao, TipoFormat.CPF) : Funcoes.Format(this.Identificacao, TipoFormat.CNPJ);
                }
            }

            public string DocParc
            {
                get { 
                    return  Documento +"/"+ Parcela.ToString();
                }
            }

            public double Dias
            {
                get
                {
                    if (DataPagamento != null)
                        return DataVencimento.Subtract(DataPagamento.Value).TotalDays;
                    // return DataPagamento.Value.Subtract(DataVencimento).TotalDays;
                    else
                    {
                        return DataVencimento.Subtract(DateTime.Today).TotalDays;
                        //return DateTime.Today.Subtract(DataVencimento).TotalDays;
                    }
                }
            }

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
                            return this.Multa;
                        }
                        else
                            return null;
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
                        return this.Desconto;
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

            public string StatusDesc
            {
                get
                {
                    if (this.Status == "A")
                    {
                        return "Em Aberto";
                    }
                    else if (this.Status == "Q")
                    {
                        return "Quitado";
                    }
                    else if (this.Status == "C")
                    {
                        return "Cancelado";
                    }
                    else if (this.Status == "P")
                    {
                        return "Parcialmente Quitado";
                    }

                    return null;
                }
            }
        }
        #endregion

        private void lblValorDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorDocto.Text != "")
            {
                totGeralValorDocto = totGeralValorDocto + decimal.Parse(lblValorDocto.Text);
            }
        }

        private void lblJurosDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblJurosDocto.Text != "")
            {
                totGeralValorJuros = totGeralValorJuros + decimal.Parse(lblJurosDocto.Text);
            }
        }

        private void lblMultaDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblMultaDocto.Text != "")
            {
                totGeralValorMulta = totGeralValorMulta + decimal.Parse(lblMultaDocto.Text);
            }
        }

        private void lblDesctoDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblDesctoDocto.Text != "")
            {
                totGeralValorDescto = totGeralValorDescto + decimal.Parse(lblDesctoDocto.Text);
            }
        }

        private void lblValorTotDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorTotDocto.Text != "")
            {
                totGeralValorTotal = totGeralValorTotal + decimal.Parse(lblValorTotDocto.Text);
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotGeralDocto.Text = String.Format("{0:#,##0.00}", totGeralValorDocto);
            lblTotGeralMulta.Text = String.Format("{0:#,##0.00}", totGeralValorMulta);
            lblTotGeralJuros.Text = String.Format("{0:#,##0.00}", totGeralValorJuros);
            lblTotDesctoGeral.Text = String.Format("{0:#,##0.00}", totGeralValorDescto);
            lblTotGeralTot.Text = String.Format("{0:#,##0.00}", totGeralValorTotal);
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (TipoPesq == "V") xrTableCell7.Text = "DT VENC";
            else if (TipoPesq == "M") xrTableCell7.Text = "DT MOV";
            else if (TipoPesq == "P") xrTableCell7.Text = "DT PAG";
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var obj = (XRTableCell)sender;
            
            if (int.Parse(obj.Text) < 0)
            {
                obj.ForeColor = Color.Red;
                obj.Text = "+ " + obj.Text.Replace("-", "").Replace("+", "");
            }
            else if (int.Parse(obj.Text) > 0)
            {
                obj.ForeColor = Color.Blue;
                obj.Text = "- " + obj.Text.Replace("-", "").Replace("+", ""); 
            }
            else
            {
                obj.ForeColor = Color.Black;
            }
        }
    }
}
