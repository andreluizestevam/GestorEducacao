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
    public partial class RptRelacaoTituloFornecDespesas : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totParcValorDocto, totParcValorMulta, totParcValorJuros, totParcValorDescto, totParcValorTotal = 0;
        private decimal totGeralValorDocto, totGeralValorMulta, totGeralValorJuros, totGeralValorDescto, totGeralValorTotal = 0;

        public RptRelacaoTituloFornecDespesas()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_FORN,
                              string strP_IC_SIT_DOC,
                              string strP_DT_INI_VENC,
                              string strP_DT_FIM_VENC,
                              string strP_DT_INI_CADAS,
                              string strP_DT_FIM_CADAS,
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

                #region Query Colaborador Parametrizada

                DateTime dtIniCadas = strP_DT_INI_CADAS != "" ? DateTime.Parse(strP_DT_INI_CADAS) : DateTime.Now;
                DateTime dtFimCadas = strP_DT_FIM_CADAS != "" ? DateTime.Parse(strP_DT_FIM_CADAS) : DateTime.Now;
                DateTime dtIniVenc = strP_DT_INI_VENC != "" ? DateTime.Parse(strP_DT_INI_VENC) : DateTime.Now;
                DateTime dtFimVenc = strP_DT_FIM_VENC != "" ? DateTime.Parse(strP_DT_FIM_VENC) : DateTime.Now;

                var lst = (from tb38 in ctx.TB38_CTA_PAGAR
                           where tb38.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strP_NU_DOC != "" ? tb38.NU_DOC == strP_NU_DOC : strP_NU_DOC == "")
                           && (strP_CO_EMP_REF != 0 ? tb38.CO_EMP == strP_CO_EMP_REF : strP_CO_EMP_REF == 0)
                           && (strP_IC_SIT_DOC != "T" ? tb38.IC_SIT_DOC == strP_IC_SIT_DOC : strP_IC_SIT_DOC == "T")
                           && (strP_CO_FORN != 0 ? tb38.TB41_FORNEC.CO_FORN == strP_CO_FORN : strP_CO_FORN == 0)
                           && (strP_DT_INI_VENC != "" ? tb38.DT_VEN_DOC >= dtIniVenc : strP_DT_INI_VENC == "")
                           && (strP_DT_FIM_VENC != "" ? tb38.DT_VEN_DOC <= dtFimVenc : strP_DT_FIM_VENC == "")
                           && (strP_DT_INI_CADAS != "" ? tb38.DT_VEN_DOC >= dtIniCadas : strP_DT_INI_CADAS == "")
                           && (strP_DT_FIM_CADAS != "" ? tb38.DT_VEN_DOC <= dtFimCadas : strP_DT_FIM_CADAS == "")
                           && (strP_CO_AGRUP != 0 ? tb38.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                           select new RelTitulosFornecedor
                           {
                               CNPJCPF = tb38.TB41_FORNEC.CO_CPFCGC_FORN,
                               Nome = tb38.TB41_FORNEC.NO_FAN_FOR,
                               TpFornec = tb38.TB41_FORNEC.TP_FORN,
                               DataMov = tb38.DT_CAD_DOC,
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
                               ValorPago = tb38.VR_PAG
                           }).OrderBy(p => p.Nome).ThenBy(p => p.DataMov);

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
                foreach (RelTitulosFornecedor at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Relacao de Titulos do Fornecedor Parametrizado do Relatorio

        public class RelTitulosFornecedor
        {
            public string CNPJCPF { get; set; }
            public string Nome { get; set; }
            public string TpFornec { get; set; }
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

                    return null;
                }
            }

            public string NomeDesc
            {
                get
                {
                    return this.TpFornec == "F" ? Funcoes.Format(this.CNPJCPF, TipoFormat.CPF) + " - " + this.Nome : Funcoes.Format(this.CNPJCPF, TipoFormat.CNPJ) + " - " + this.Nome;
                }
            }

        }
        #endregion

        private void DetailContent_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorDocto.Text != "")
            {
                totParcValorDocto = totParcValorDocto + decimal.Parse(lblValorDocto.Text);
            }
            if (lblJurosDocto.Text != "")
            {
                totParcValorJuros = totParcValorJuros + decimal.Parse(lblJurosDocto.Text);
            }
            if (lblMultaDocto.Text != "")
            {
                totParcValorMulta = totParcValorMulta + decimal.Parse(lblMultaDocto.Text);
            }
            if (lblDesctoDocto.Text != "")
            {
                totParcValorDescto = totParcValorDescto + decimal.Parse(lblDesctoDocto.Text);
            }
            if (lblValorTotDocto.Text != "")
            {
                totParcValorTotal = totParcValorTotal + decimal.Parse(lblValorTotDocto.Text);
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotParcDocto.Text = String.Format("{0:#,##0.00}", totParcValorDocto);
            lblTotParcMulta.Text = String.Format("{0:#,##0.00}", totParcValorMulta);
            lblTotParcJuros.Text = String.Format("{0:#,##0.00}", totParcValorJuros);
            lblTotParcDescto.Text = String.Format("{0:#,##0.00}", totParcValorDescto);
            lblTotParcTot.Text = String.Format("{0:#,##0.00}", totParcValorTotal);

            totGeralValorDocto = totGeralValorDocto + totParcValorDocto;
            totGeralValorMulta = totGeralValorMulta + totParcValorMulta;
            totGeralValorJuros = totGeralValorJuros + totParcValorJuros;
            totGeralValorDescto = totGeralValorDescto + totParcValorDescto;
            totGeralValorTotal = totGeralValorTotal + totParcValorTotal;

            totParcValorDocto = totParcValorMulta = totParcValorJuros = totParcValorDescto = totParcValorTotal = 0;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotGeralDocto.Text = String.Format("{0:#,##0.00}", totGeralValorDocto);
            lblTotGeralMulta.Text = String.Format("{0:#,##0.00}", totGeralValorMulta);
            lblTotGeralJuros.Text = String.Format("{0:#,##0.00}", totGeralValorJuros);
            lblTotGeralDescto.Text = String.Format("{0:#,##0.00}", totGeralValorDescto);
            lblTotGeralTot.Text = String.Format("{0:#,##0.00}", totGeralValorTotal);
        }
    }
}
