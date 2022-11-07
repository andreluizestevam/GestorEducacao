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
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptRelatoPosicaoRecursosReceitas : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        private decimal totGeralValorQuitado, totGeralValorAbertos, totGeralValorParcQuitados = 0;

        public RptRelatoPosicaoRecursosReceitas()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              string strUniContr,
                              string srtPesqPor,
                              int strP_CO_ALU,
                              int strP_CO_CLIENTE,
                              string strP_IC_SIT_DOC,
                              DateTime strP_DT_INI,
                              DateTime strP_DT_FIM,
                              string strP_NU_DOC,
                              int strP_CO_AGRUP,
                              bool incluCance,
                              string infos,
                              int strP_CO_RESP,
                              string NO_RELATORIO)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "POSIÇÃO DE TÍTULOS DE RECEITAS/RECURSOS - GERAL*");

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Titulos Parametrizada

                int IntUniContr = strUniContr != "T" ? int.Parse(strUniContr) : 0;

                if (srtPesqPor != "R")
                {
                    #region Por Datas

                    //=====> Altera o label da coluna de DATA
                    switch (srtPesqPor)
                    {
                        case "M":
                            tcData.Text = "DT MOV";
                            break;
                        case "V":
                            tcData.Text = "DT VENC";
                            break;
                        case "P":
                            tcData.Text = "DT PAG";
                            tcDtVenc.Text = "DT VENC";
                            break;
                    }

                    var lst = (from tbs47 in ctx.TBS47_CTA_RECEB
                               join tb07 in ctx.TB07_ALUNO on tbs47.CO_ALU equals tb07.CO_ALU into sr
                               from x in sr.DefaultIfEmpty()
                               where tbs47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                               && (IntUniContr != 0 ? tbs47.CO_EMP_UNID_CONT == IntUniContr : IntUniContr == 0)
                               && (strP_NU_DOC != "" ? tbs47.NU_DOC == strP_NU_DOC : strP_NU_DOC == "")
                               && (strP_CO_EMP_REF != 0 ? tbs47.CO_EMP == strP_CO_EMP_REF : strP_CO_EMP_REF == 0)
                               && (strP_IC_SIT_DOC != "T" ? tbs47.IC_SIT_DOC == strP_IC_SIT_DOC : strP_IC_SIT_DOC == "T")
                               && (strP_CO_ALU != 0 ? tbs47.CO_ALU == strP_CO_ALU : strP_CO_ALU == 0)
                               && (strP_CO_CLIENTE != 0 ? tbs47.TB103_CLIENTE.CO_CLIENTE == strP_CO_CLIENTE : strP_CO_CLIENTE == 0)
                                   //&& tbs47.DT_VEN_DOC >= strP_DT_INI && tbs47.DT_VEN_DOC <= strP_DT_FIM
                               && (srtPesqPor == "M" ? (tbs47.DT_CAD_DOC >= strP_DT_INI && tbs47.DT_CAD_DOC <= strP_DT_FIM) :
                               (srtPesqPor == "V" ? (tbs47.DT_VEN_DOC >= strP_DT_INI && tbs47.DT_VEN_DOC <= strP_DT_FIM) :
                               (tbs47.DT_REC_DOC >= strP_DT_INI && tbs47.DT_REC_DOC <= strP_DT_FIM)))
                               && (strP_CO_AGRUP != 0 ? tbs47.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                               && (!incluCance && strP_IC_SIT_DOC != "C" ? tbs47.IC_SIT_DOC != "C" : 0 == 0)
                               select new RelPosRecReceitas
                               {
                                   TipoCliente = tbs47.TP_CLIENTE_DOC,
                                   Identificacao = (tbs47.TP_CLIENTE_DOC == "A" ? tbs47.TB108_RESPONSAVEL.NU_CPF_RESP : tbs47.TB103_CLIENTE.CO_CPFCGC_CLI),
                                   Nome = (tbs47.TP_CLIENTE_DOC == "A" ? tbs47.TB108_RESPONSAVEL.NO_RESP : tbs47.TB103_CLIENTE.NO_FAN_CLI),
                                   Codigo = (tbs47.TP_CLIENTE_DOC == "A" ? (x != null ? x.NU_NIRE : 0) : tbs47.TB103_CLIENTE.CO_CLIENTE),
                                   Data = (srtPesqPor == "M" ? tbs47.DT_CAD_DOC : (srtPesqPor == "V" ? tbs47.DT_VEN_DOC :
                                                (srtPesqPor == "P") ? tbs47.DT_REC_DOC : null)),
                                   DataMov = tbs47.DT_CAD_DOC,
                                   Parcela = tbs47.NU_PAR,
                                   Status = tbs47.IC_SIT_DOC,
                                   Documento = tbs47.NU_DOC,
                                   DataVencimento = tbs47.DT_VEN_DOC,
                                   DataPagamento = (srtPesqPor != "P" ? tbs47.DT_REC_DOC : tbs47.DT_VEN_DOC),
                                   ValorDocumento = tbs47.VL_PAR_DOC,
                                   Juros = tbs47.VL_JUR_DOC,
                                   Multa = tbs47.VL_MUL_DOC,
                                   Desconto = tbs47.VL_DES_DOC,
                                   DescontoPago = tbs47.VL_DES_PAG,
                                   TpMulta = tbs47.CO_FLAG_TP_VALOR_MUL,
                                   TpJuros = tbs47.CO_FLAG_TP_VALOR_JUR,
                                   TpDesc = tbs47.CO_FLAG_TP_VALOR_DES,
                                   JurosPago = tbs47.VL_JUR_PAG,
                                   MultaPago = tbs47.VL_MUL_PAG,
                                   ValorPago = tbs47.VL_PAG
                               }).OrderBy(p => p.Data);

                    var res = lst.ToList();

                    foreach (var iLst in res)
                    {
                        iLst.DataDif = DateTime.Now.Subtract(iLst.DataVencimento);
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    totGeralValorAbertos = res.Where(x => x.Status == "A").Sum(x => x.ValorDocumento);
                    totGeralValorQuitado = (res.Where(x => x.Status == "Q").Sum(x => x.ValorPago) ?? 0);
                    totGeralValorParcQuitados = (res.Where(x => x.Status == "P").Sum(x => x.ValorPago) ?? 0);

                    // Seta os alunos no DataSource do Relatorio
                    bsReport.Clear();
                    foreach (RelPosRecReceitas at in res)
                        bsReport.Add(at);
                    #endregion
                }
                else
                {
                    #region Por Responsável
                    var lst = (from tbs47 in ctx.TBS47_CTA_RECEB
                               join tb07 in ctx.TB07_ALUNO on tbs47.CO_ALU equals tb07.CO_ALU into sr
                               from x in sr.DefaultIfEmpty()
                               where tbs47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                               && (IntUniContr != 0 ? tbs47.CO_EMP_UNID_CONT == IntUniContr : IntUniContr == 0)
                               && (strP_NU_DOC != "" ? tbs47.NU_DOC == strP_NU_DOC : strP_NU_DOC == "")
                               && (strP_CO_EMP_REF != 0 ? tbs47.CO_EMP == strP_CO_EMP_REF : strP_CO_EMP_REF == 0)
                               && (strP_IC_SIT_DOC != "T" ? tbs47.IC_SIT_DOC == strP_IC_SIT_DOC : strP_IC_SIT_DOC == "T")
                               && (strP_CO_ALU != 0 ? tbs47.CO_ALU == strP_CO_ALU : strP_CO_ALU == 0)
                               && (strP_CO_CLIENTE != 0 ? tbs47.TB103_CLIENTE.CO_CLIENTE == strP_CO_CLIENTE : strP_CO_CLIENTE == 0)
                               && (tbs47.DT_VEN_DOC >= strP_DT_INI && tbs47.DT_VEN_DOC <= strP_DT_FIM)
                               //&& (srtPesqPor == "M" ? (tbs47.DT_CAD_DOC >= strP_DT_INI && tbs47.DT_CAD_DOC <= strP_DT_FIM) :
                               //(srtPesqPor == "V" ? (tbs47.DT_VEN_DOC >= strP_DT_INI && tbs47.DT_VEN_DOC <= strP_DT_FIM) :
                               //(tbs47.DT_REC_DOC >= strP_DT_INI && tbs47.DT_REC_DOC <= strP_DT_FIM)))
                               && (strP_CO_AGRUP != 0 ? tbs47.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                               && (!incluCance && strP_IC_SIT_DOC != "C" ? tbs47.IC_SIT_DOC != "C" : 0 == 0)
                               && (strP_CO_RESP != 0 ? tbs47.TB108_RESPONSAVEL.CO_RESP == strP_CO_RESP : strP_CO_RESP == 0)
                               select new RelPosRecReceitas
                               {
                                   TipoCliente = tbs47.TP_CLIENTE_DOC,
                                   Identificacao = tbs47.TP_CLIENTE_DOC == "A" ? tbs47.TB108_RESPONSAVEL.NU_CPF_RESP : tbs47.TB103_CLIENTE.CO_CPFCGC_CLI,
                                   Nome = tbs47.TP_CLIENTE_DOC == "A" ? tbs47.TB108_RESPONSAVEL.NO_RESP : tbs47.TB103_CLIENTE.NO_FAN_CLI,
                                   Codigo = tbs47.TP_CLIENTE_DOC == "A" ? (x != null ? x.NU_NIRE : 0) : tbs47.TB103_CLIENTE.CO_CLIENTE,
                                   Data = tbs47.DT_VEN_DOC,
                                   DataMov = tbs47.DT_CAD_DOC,
                                   Parcela = tbs47.NU_PAR,
                                   Status = tbs47.IC_SIT_DOC,
                                   Documento = tbs47.NU_DOC,
                                   DataVencimento = tbs47.DT_VEN_DOC,
                                   DataPagamento = tbs47.DT_REC_DOC,
                                   ValorDocumento = tbs47.VL_PAR_DOC,
                                   Juros = tbs47.VL_JUR_DOC,
                                   Multa = tbs47.VL_MUL_DOC,
                                   Desconto = tbs47.VL_DES_DOC,
                                   DescontoPago = tbs47.VL_DES_PAG,
                                   TpMulta = tbs47.CO_FLAG_TP_VALOR_MUL,
                                   TpJuros = tbs47.CO_FLAG_TP_VALOR_JUR,
                                   TpDesc = tbs47.CO_FLAG_TP_VALOR_DES,
                                   JurosPago = tbs47.VL_JUR_PAG,
                                   MultaPago = tbs47.VL_MUL_PAG,
                                   ValorPago = tbs47.VL_PAG
                               }).OrderBy(p => p.Nome).ThenBy(t => t.Parcela);

                    var res = lst.ToList();

                    foreach (var iLst in res)
                    {
                        iLst.DataDif = DateTime.Now.Subtract(iLst.DataVencimento);
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    totGeralValorAbertos = res.Where(x => x.Status == "A").Sum(x => x.ValorDocumento);
                    totGeralValorQuitado = (res.Where(x => x.Status == "Q").Sum(x => x.ValorPago) ?? 0);
                    totGeralValorParcQuitados = (res.Where(x => x.Status == "P").Sum(x => x.ValorPago) ?? 0);

                    // Seta os alunos no DataSource do Relatorio
                    bsReport.Clear();
                    foreach (RelPosRecReceitas at in res)
                        bsReport.Add(at);
                    #endregion
                #endregion
                }


                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Relacao de Receitas Parametrizado do Relatorio

        public class RelPosRecReceitas
        {
            public string TipoCliente { get; set; }
            public string Nome { get; set; }
            public int Codigo { get; set; }
            public string Identificacao { get; set; }

            public DateTime? Data { get; set; }
            public DateTime DataMov { get; set; }
            public DateTime DataVencimento { get; set; }
            public DateTime? DataPagamento { get; set; }
            public int Parcela { get; set; }
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

            public string Referencia
            {
                get
                {
                    return (this.TipoCliente == "A") ? "(P) " + this.Codigo.ToString().PadLeft(7, '0') : "(C) " + this.Codigo.ToString();
                }
            }

            public string CPFDesc
            {
                get
                {
                    return Funcoes.Format(this.Identificacao, TipoFormat.CPF);
                }
            }

            public int Dias
            {
                get
                {
                    double valor = 0;
                    if (DataPagamento != null)
                        valor = DataVencimento.Subtract(DataPagamento.Value).TotalDays;
                    else
                        valor = DataVencimento.Subtract(DateTime.Today).TotalDays;
                    return (int)Math.Round(valor, 0);
                }
            }

            public decimal? JurosDesc
            {
                get
                {
                    if (this.Status == "A" || this.Status == "R")
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
                    else if (this.Status == "Q" || this.Status == "P")
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
                    if (this.Status == "A" || this.Status == "R")
                    {
                        if (Convert.ToDecimal(this.DataDif.Days) > 0)
                        {
                            if (this.TpJuros == "P")
                            {
                                Result = ((this.ValorDocumento / 100) * this.Multa);
                                return Result;

                            }
                            else if (this.TpJuros == "V")
                            {
                                return this.Multa;
                            }
                        }
                        else
                            return null;
                    }
                    else if (this.Status == "Q" || this.Status == "P")
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
                    if (this.Status == "A" || this.Status == "R")
                    {
                        if (Convert.ToDecimal(this.DataDif.Days) <= 0)
                        {
                            if (this.TpDesc == "P")
                            {
                                Result = ((this.ValorDocumento / 100) * this.Desconto);

                                return Result;
                            }
                            else
                            {
                                Result = this.Desconto;

                                return Result;
                            }
                        }
                        else
                            return null;
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
                        return (this.ValorPago ?? 0);
                    }
                    else if ((this.Status == "A" || this.Status == "R") && (Convert.ToDecimal(this.DataDif.Days) > 0))
                    {
                        return this.ValorDocumento +
                            (this.JurosDesc != null ? this.JurosDesc : 0) +
                            (this.MultaDesc != null ? this.MultaDesc : 0) -
                            (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }
                    else if (this.Status == "A" || this.Status == "R")
                    {
                        return this.ValorDocumento - (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }
                    else if (this.Status == "P")
                    {
                        return this.ValorDocumento - ((this.ValorPago ?? 0));
                    }

                    return null;
                }
            }

            public decimal? DiferDesc
            {
                get
                {
                    if (this.Status == "P" || this.Status == "Q")
                        return (this.ValorPago ?? 0) - (this.ValorDocumento + (this.MultaDesc ?? 0) + (this.JurosDesc ?? 0) - (this.DescontoDesc ?? 0));
                    else
                        return (this.ValorDocumento + (this.MultaDesc ?? 0) + (this.JurosDesc ?? 0) - (this.DescontoDesc ?? 0));
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
                        return "Parc. Quitado";
                    }
                    else if (this.Status == "R")
                    {
                        return "Pré Mat";
                    }
                    return null;
                }
            }
        }
        #endregion

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            totGeralValorQuitado *= -1;

            decimal res = (totGeralValorQuitado + totGeralValorAbertos + totGeralValorParcQuitados);

            if (res < 0)
                lblTotGeralTot.ForeColor = Color.DarkGreen;
            else if (res > 0)
                lblTotGeralTot.ForeColor = Color.Blue;
            else
                lblTotGeralTot.ForeColor = Color.Black;

            lblTotGeralTot.Text = string.Format("{0:'+'#,##0.00;'-'#,##0.00}", res);
        }

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "" && obj.Text != "0,00")
            {
                if (int.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "-" + obj.Text.Replace("-", "").Replace("+", "");
                }
                else if (int.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "+" + obj.Text.Replace("-", "").Replace("+", "");
                }
            }
            else { obj.ForeColor = Color.Black; }
        }

        private void lblNome_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            obj.Text = obj.Text.Replace(".", "").Replace("-", "");
            if (obj.Text.Length == 14)
            {
                obj.Text = obj.Text.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".");
            }
            else if (obj.Text.Length == 11)
            {
                obj.Text = obj.Text.Insert(9, "-").Insert(6, ".").Insert(3, ".");
            }
        }

        private void lblValorTotDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            decimal value;
            if (decimal.TryParse(obj.Text, out value))
            {
                if (value > 0)
                    obj.ForeColor = Color.Blue;
                else if (value < 0)
                    obj.ForeColor = Color.Red;
                else
                    obj.ForeColor = Color.Black;
            }
            else
                obj.ForeColor = Color.Black;
        }

        private void txtDias_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "" && obj.Text != "0")
            {
                if (int.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "+" + obj.Text.Replace("-", "").Replace("+", "");
                }
                else if (int.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "-" + obj.Text.Replace("-", "").Replace("+", "");
                }
            }
            else { obj.ForeColor = Color.Black; }
        }
    }
}
