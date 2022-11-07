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

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar
{
    public partial class RptDeclaracaoParcelasEmAberto : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totValorDif, totValorMulta, totValorJuros, totValorDescto = 0;
        private decimal totValorDoctoG, totValorMultaG, totValorJurosG, totValorDesctoG = 0;

        public RptDeclaracaoParcelasEmAberto()
        {
            InitializeComponent();
        }

        #region Init Report
        public int InitReport(string parametros
            , int codInst
            , int strCO_EMP
            , int strCO_EMP_REF
            , string strCO_ANO_INICIO
            , string strCO_ANO_FIM
            , int strCO_MOD_CUR
            , int strCO_CUR
            , int strCO_TUR
            , int strCO_ALU_CAD
            , int strCO_RES
            , DateTime dtInicio
            , DateTime dtFim
            , string strINFOS)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = strINFOS;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strCO_EMP);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada


                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                           join tb01 in ctx.TB01_CURSO on tb47.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb47.CO_TUR equals tb129.CO_TUR
                           join tb25 in ctx.TB25_EMPRESA on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP
                           where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strCO_EMP_REF != 0 ? tb47.CO_EMP == strCO_EMP_REF : strCO_EMP_REF == 0)
                           && tb47.IC_SIT_DOC == "A"
                           && (strCO_MOD_CUR != 0 ? strCO_MOD_CUR == -1 ? 0 == 0 : tb47.CO_MODU_CUR == strCO_MOD_CUR : strCO_MOD_CUR == 0)
                           && (strCO_CUR != 0 ? strCO_CUR == -1 ? 0 == 0 : tb47.CO_CUR == strCO_CUR : strCO_CUR == 0)
                           && (strCO_ALU_CAD != 0 ? strCO_ALU_CAD == -1 ? 0 == 0 : tb47.CO_ALU == strCO_ALU_CAD : strCO_ALU_CAD == 0)
                           && (strCO_TUR != 0 ? strCO_TUR == -1 ? 0 == 0 : tb47.CO_TUR == strCO_TUR : strCO_TUR == 0)
                           && (strCO_RES != 0 ? strCO_RES == -1 ? 0 == 0 : tb47.TB108_RESPONSAVEL.CO_RESP == strCO_RES : strCO_RES == 0)
                           && tb47.DT_VEN_DOC < DateTime.Now
                           && (tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim)
                           select new DecParcAberto
                           {
                               SiglaUnid = tb25.sigla,
                               Nome = tb07.NO_ALU,
                               NIRE = tb07.NU_NIRE,
                               DescSerie = " - Mod: " + tb01.TB44_MODULO.DE_MODU_CUR + " - Série: " + tb01.NO_CUR +
                               " - Turma: " + tb129.NO_TURMA + " - Convênio: " + (tb07.TB148_TIPO_BOLSA != null ? tb07.TB148_TIPO_BOLSA.NO_TIPO_BOLSA + " )" : "Nenhum )"),
                               Responsavel = tb47.TB108_RESPONSAVEL.NO_RESP,
                               Status = tb47.IC_SIT_DOC,
                               Documento = tb47.NU_DOC,
                               DataVencimento = tb47.DT_VEN_DOC,
                               DataPagamento = tb47.DT_REC_DOC,
                               ValorDocumento = tb47.VR_PAR_DOC,
                               Juros = tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0,
                               Multa = tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0,
                               Desconto = tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0,
                               DescontoBolsa = tb47.VL_DES_BOLSA_ALUNO != null ? tb47.VL_DES_BOLSA_ALUNO : 0,
                               TpDescBolsa = tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO,
                               DescontoPago = tb47.VR_DES_PAG != null ? tb47.VR_DES_PAG : 0,
                               TpMulta = tb47.CO_FLAG_TP_VALOR_MUL,
                               TpJuros = tb47.CO_FLAG_TP_VALOR_JUR,
                               TpDesc = tb47.CO_FLAG_TP_VALOR_DES,
                               JurosPago = tb47.VR_JUR_PAG != null ? tb47.VR_JUR_PAG : 0,
                               MultaPago = tb47.VR_MUL_PAG != null ? tb47.VR_MUL_PAG : 0,
                               ValorPago = tb47.VR_PAG,
                               Parcela = tb47.NU_PAR,
                               CpfResp = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                            
                           }).DistinctBy(p => p.Documento).OrderBy(p => p.Nome).ThenBy(p => p.DataVencimento).ThenBy(p => p.Parcela);

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
                foreach (DecParcAberto at in res)
                    bsReport.Add(at);
                xrLabelResp.Text = res.First().DescResp.ToString();
                GroupHeader1.GroupFields.Add(new GroupField("Nome", XRColumnSortOrder.Ascending));
                if (strCO_ALU_CAD != -1)
                    xrTable4.Visible = false;
                return 1;
            }
            catch { return 0; }
        }
        #endregion

        #region Classe Histórico Financeiro do Aluno

        public class DecParcAberto
        {
            public string SiglaUnid { get; set; }
            public string Nome { get; set; }
            public int NIRE { get; set; }
            public string Responsavel { get; set; }
            public string DescSerie { get; set; }
            public DateTime DataVencimento { get; set; }
            public DateTime? DataPagamento { get; set; }
            public string Status { get; set; }
            public string Documento { get; set; }
            public decimal ValorDocumento { get; set; }
            public decimal? Juros { get; set; }
            public decimal? Multa { get; set; }
            public decimal? Desconto { get; set; }
            public decimal? DescontoBolsa { get; set; }
            public string TpMulta { get; set; }
            public string TpJuros { get; set; }
            public string TpDesc { get; set; }
            public string TpDescBolsa { get; set; }
            public decimal? Result { get; set; }
            public decimal? ValorPago { get; set; }
            public decimal? JurosPago { get; set; }
            public decimal? MultaPago { get; set; }
            public decimal? DescontoPago { get; set; }
            public TimeSpan DataDif { get; set; }
            public int Parcela { get; set; }
            public string CpfResp { get; set; }
            public string CpfRespDesc
            {
                get
                {
                    string retorno;
                    if (this.CpfResp != null)
                    {
                        retorno = this.CpfResp.Insert(3, ".");
                        retorno = retorno.Insert(7, ".");
                        retorno = retorno.Insert(11, "-");
                    }
                    else
                    {
                        retorno = "-";

                    }
                    return retorno;
                }
            }

            public string DescResp
            {
                get
                {
                    return "( Responsável Financeiro: " + this.CpfRespDesc + " - " + this.Responsavel + ")";
                }
            }

            public string DescAlu
            {
                get
                {
                    return "( NIRE: " + this.NIRE + this.DescSerie;
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
                        if (Convert.ToDecimal(this.DataDif.Days) < 0)
                        {
                            if (this.TpDesc == "P")
                            {
                                Result = ((this.ValorDocumento / 100) * this.Desconto);

                                if (this.TpDescBolsa == "P" && this.DescontoBolsa != null)
                                {
                                    Result = Result + ((this.ValorDocumento / 100) * this.DescontoBolsa);
                                }
                                else if (this.DescontoBolsa != null)
                                {
                                    Result = Result + this.DescontoBolsa;
                                }

                                return Result;
                            }
                            else
                            {
                                Result = this.Desconto;

                                if (this.TpDescBolsa == "P" && this.DescontoBolsa != null)
                                {
                                    Result = Result + ((this.ValorDocumento / 100) * this.DescontoBolsa);
                                }
                                else if (this.DescontoBolsa != null)
                                {
                                    Result = Result + this.DescontoBolsa;
                                }

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
                    //if (this.Status == "A")
                    //{
                    //    return this.Documento + " / " + "Em Aberto";
                    //}
                    //else if (this.Status == "Q")
                    //{
                    //    return this.Documento + " / " + "Quitado";
                    //}
                    //else if (this.Status == "C")
                    //{
                    //    return this.Documento + " / " + "Cancelado";
                    //}

                    return this.Documento;
                }
            }

            public decimal? ValorDif
            {
                get
                {
                    if (this.Status == "Q" || this.Status == "P")
                        return ((this.ValorPago ?? 0) - (this.ValorDocumento + this.JurosDesc + this.MultaDesc - this.DescontoDesc)) * (-1);
                    else
                    {
                        return ((this.ValorDocumento + (this.JurosDesc != null ? this.JurosDesc : 0) + (this.MultaDesc != null ? this.MultaDesc : 0)) -
                            (this.DescontoDesc != null ? this.DescontoDesc : 0));

                    }
                }
            }

            public double DiasDesc
            {
                get
                {
                    if (DataPagamento != null)
                    {
                        if (DataPagamento > this.DataVencimento)
                        { //atrasado
                            return this.DataPagamento.Value.Subtract(DataVencimento).TotalDays;
                        }
                        else if (DataPagamento.Value < this.DataVencimento)
                        {
                            return this.DataPagamento.Value.Subtract(DataVencimento).TotalDays;
                        }
                        else
                            return 0;
                    }
                    else
                    {
                        return DateTime.Today.Subtract(this.DataVencimento).TotalDays;
                    }
                }
            }

            public decimal? ValorDifParc
            {
                get
                {
                    return (this.ValorDocumento + (this.JurosDesc ?? 0) + (this.MultaDesc ?? 0) - (this.DescontoDesc ?? 0)) - (this.ValorPago ?? 0);
                }
            }
        }
        #endregion

        #region Eventos print

        private void lblJurosDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblJurosDocto.Text != "")
            {
                totValorJuros = totValorJuros + decimal.Parse(lblJurosDocto.Text);
                totValorJurosG = totValorJurosG + decimal.Parse(lblJurosDocto.Text);
            }
        }

        private void lblMultaDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblMultaDocto.Text != "")
            {
                totValorMulta = totValorMulta + decimal.Parse(lblMultaDocto.Text);
                totValorMultaG = totValorMultaG + decimal.Parse(lblMultaDocto.Text);
            }
        }

        private void lblDesctoDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblDesctoDocto.Text != "")
            {
                totValorDescto = totValorDescto + decimal.Parse(lblDesctoDocto.Text);
                totValorDesctoG = totValorDesctoG + decimal.Parse(lblDesctoDocto.Text);
            }
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (int.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (int.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "+ " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else
                    obj.ForeColor = Color.Black;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            totValorDoctoG = totValorMultaG = totValorJurosG = totValorDesctoG = 0;
        }

        private void lblJurosDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
        }

        private void lblValorDif_AfterPrint(object sender, EventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                totValorDif = totValorDif + decimal.Parse(obj.Text);
            }
        }

        private void lblValorTotDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (decimal.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (decimal.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "+ " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else
                {
                    obj.ForeColor = Color.Black;
                }
            }
        }

        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            
            if (obj.Text != "")
            {
                decimal valorObj = decimal.Parse(obj.Text.Replace("+", "").Replace("-", ""));
                if (valorObj < 0)
                {
                    //obj.ForeColor = Color.Blue;
                    obj.Text = "- " + string.Format("{0:n2}", valorObj);
                }
                else if (valorObj > 0)
                {
                    //obj.ForeColor = Color.Red;
                    obj.Text = "+ " + string.Format("{0:n2}", valorObj);
                }
            }
        }

        private void xrTableCell32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                decimal valorObj = decimal.Parse(obj.Text.Replace("+", "").Replace("-", ""));
                if (decimal.Parse(obj.Text) < 0)
                {
                    //obj.ForeColor = Color.Blue;
                    obj.Text = "- " + string.Format("{0:n2}", valorObj);
                }
                else if (decimal.Parse(obj.Text) > 0)
                {
                    //obj.ForeColor = Color.Red;
                    obj.Text = "+ " + string.Format("{0:n2}", valorObj);
                }
            }
        }
        #endregion
    }
}
