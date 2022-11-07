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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptRelacaoTituloAlunosRecursReceitasMod2 : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        private decimal totParcValorDocto, totParcValorMulta, totParcValorJuros, totParcValorDescto, totParcValorTotal, totParcValorSaldo = 0;
        private decimal totGeralValorSaldo = 0;

        public RptRelacaoTituloAlunosRecursReceitasMod2()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int strP_Ordem,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_UNID_CONTR,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              int strP_CO_RESP,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_ALU,
                              string strP_IC_SIT_DOC,
                              string strP_DT_INI_VENC,
                              string strP_DT_FIM_VENC,
                              string strP_DT_INI_CADAS,
                              string strP_DT_FIM_CADAS,
                              string strP_NU_DOC,
                              int strP_CO_AGRUP,
                              bool incluCance,
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

                string coTipoUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP).CO_TIPO_UNID;
                if(coTipoUnid == "PGS")
                    lblTitulo.Text = "POSIÇÃO DE TÍTULOS DE RECEITAS - DE PACIENTES";
                else
                    lblTitulo.Text = "POSIÇÃO DE TÍTULOS DE RECEITAS - DE ALUNOS";

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Titulos Parametrizada

                DateTime dtIniCadas = strP_DT_INI_CADAS != "" ? DateTime.Parse(strP_DT_INI_CADAS) : DateTime.Now;
                DateTime dtFimCadas = strP_DT_FIM_CADAS != "" ? DateTime.Parse(strP_DT_FIM_CADAS) : DateTime.Now;
                DateTime dtIniVenc = strP_DT_INI_VENC != "" ? DateTime.Parse(strP_DT_INI_VENC) : DateTime.Now;
                DateTime dtFimVenc = strP_DT_FIM_VENC != "" ? DateTime.Parse(strP_DT_FIM_VENC) : DateTime.Now;

                

                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                           join tb01 in ctx.TB01_CURSO on tb47.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb47.CO_TUR equals tb129.CO_TUR
                           join tb315 in ctx.TB315_AGRUP_RECDESP on tb47.CO_AGRUP_RECDESP equals tb315.ID_AGRUP_RECDESP into agr
                           from tb315 in agr.DefaultIfEmpty()
                           where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strP_NU_DOC != "" ? tb47.NU_DOC == strP_NU_DOC : 0 == 0)
                           && (strP_CO_EMP_REF != -1 ? tb47.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                           && (strP_CO_UNID_CONTR != -1 ? tb47.CO_EMP_UNID_CONT == strP_CO_UNID_CONTR : 0 == 0)
                           && (strP_IC_SIT_DOC != "-1" ? tb47.IC_SIT_DOC == strP_IC_SIT_DOC : 0 == 0)
                           && (strP_CO_ALU != -1 ? tb47.CO_ALU == strP_CO_ALU : 0 == 0)
                           && (strP_CO_MODU_CUR != -1 ? tb47.CO_MODU_CUR == 0 : 0 == 0)
                           && (strP_CO_CUR != -1 ? tb47.CO_CUR == strP_CO_CUR : 0 == 0)
                           && (strP_CO_ANO_MES_MAT != "-1" ? tb47.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT : 0 == 0)
                           && (strP_CO_TUR != -1 ? tb47.CO_TUR == strP_CO_TUR : 0 == 0)
                           && (strP_DT_INI_VENC != "" ? tb47.DT_VEN_DOC >= dtIniVenc : 0 == 0)
                           && (strP_DT_FIM_VENC != "" ? tb47.DT_VEN_DOC <= dtFimVenc : 0 == 0)
                           && (strP_DT_INI_CADAS != "" ? tb47.DT_VEN_DOC >= dtIniCadas : 0 == 0)
                           && (strP_DT_FIM_CADAS != "" ? tb47.DT_VEN_DOC <= dtFimCadas : 0 == 0)
                           && (strP_CO_AGRUP != -1 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : 0 == 0)
                           && (!incluCance && strP_IC_SIT_DOC != "C" ? tb47.IC_SIT_DOC != "C" : 0 == 0)
                           && (strP_CO_RESP != -1 ? tb47.TB108_RESPONSAVEL.CO_RESP == strP_CO_RESP : 0 == 0)

                           select new RelTitulosAluno
                           {
                               CO_EMP_LOGADA = strP_CO_EMP,

                               // Dados do responsável
                               nuTelCel = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                               nuTelCom = tb07.TB108_RESPONSAVEL.NU_TELE_COME_RESP,
                               nuTelRes = tb07.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                               emailResp = tb07.TB108_RESPONSAVEL.DES_EMAIL_RESP,

                               Matricula = tb07.NU_NIRE,
                               NIRE = tb07.NU_NIRE,
                               DescSerie = " - Mod: " + tb01.TB44_MODULO.DE_MODU_CUR + " - Série: " + tb01.NO_CUR +
                               " - Turma: " + tb129.NO_TURMA + " - Convênio: " + (tb07.TB148_TIPO_BOLSA != null ? tb07.TB148_TIPO_BOLSA.NO_TIPO_BOLSA + " )" : "Nenhum )"),
                               Nome = tb07.NO_ALU,
                               Responsavel = " ( Resp: " + tb47.TB108_RESPONSAVEL.NO_RESP + " )",
                               noResp = tb47.TB108_RESPONSAVEL.NO_RESP.Trim(),
                               DataMov = tb47.DT_CAD_DOC,
                               Status = tb47.IC_SIT_DOC,
                               Documento = tb47.NU_DOC,
                               UnidContrato = tb47.TB25_EMPRESA.sigla,
                               DataVencimento = tb47.DT_VEN_DOC,
                               DataPagamento = tb47.DT_REC_DOC,
                               ValorDocumento = tb47.VR_PAR_DOC,
                               Juros = tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0,
                               Multa = tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0,
                               Desconto = tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0,
                               DescontoBolsa = tb47.VL_DES_BOLSA_ALUNO != null ? tb47.VL_DES_BOLSA_ALUNO : 0,
                               TpDescBolsa = tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO,
                               TpMulta = tb47.CO_FLAG_TP_VALOR_MUL,
                               TpJuros = tb47.CO_FLAG_TP_VALOR_JUR,
                               TpDesc = tb47.CO_FLAG_TP_VALOR_DES,
                               JurosPago = tb47.VR_JUR_PAG != null ? tb47.VR_JUR_PAG : 0,
                               MultaPago = tb47.VR_MUL_PAG != null ? tb47.VR_MUL_PAG : 0,
                               DescontoPago = tb47.VR_DES_PAG != null ? tb47.VR_DES_PAG : 0,
                               ValorPago = tb47.VR_PAG,
                               Parcela = tb47.NU_PAR,
                               Bolsa = tb07.TB148_TIPO_BOLSA.DE_TIPO_BOLSA,
                               Agrupador = tb315.DE_SITU_AGRUP_RECDESP

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
                foreach (RelTitulosAluno at in res)
                    bsReport.Add(at);

                return 1;
            }


            catch { return 0; }
        }






        #endregion

        #region Classe Relacao de Titulos do Aluno Parametrizado do Relatorio

        public class RelTitulosAluno
        {
            public int CO_EMP_LOGADA { get; set; }
            public int Matricula { get; set; }
            public string Nome { get; set; }
            public DateTime DataMov { get; set; }
            public DateTime DataVencimento { get; set; }
            public DateTime? DataPagamento { get; set; }
            public string Status { get; set; }
            public int NIRE { get; set; }
            public string DescSerie { get; set; }
            public string Responsavel { get; set; }
            public string noResp { get; set; }
            public string Documento { get; set; }
            public string UnidContrato { get; set; }
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
            public decimal? DescontoBolsa { get; set; }
            public string TpDescBolsa { get; set; }
            public int Parcela { get; set; }
            public string Bolsa { get; set; }
            public string Agrupador { get; set; }
            public string nuTelCel { get; set; }
            public string nuTelRes { get; set; }
            public string nuTelCom { get; set; }
            public string emailResp { get; set; }

            public string DescAlu
            {
                get
                {
                    string coTipoUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(this.CO_EMP_LOGADA).CO_TIPO_UNID;
                    return "( NIRE: " + this.NIRE + (coTipoUnid == "PGE" ? this.DescSerie : " )") + this.Responsavel;
                }
            }

            public string DescContato
            {
                get
                {
                    string nuCel = this.nuTelCel != null ? string.Format("{0:## ####-####}", decimal.Parse(this.nuTelCel)) : "** ****-****";
                    string nuCom = this.nuTelCom != null ? string.Format("{0:## ####-####}", decimal.Parse(this.nuTelCom)) : "** ****-****";
                    string nuRes = this.nuTelRes != null ? string.Format("{0:## ####-####}", decimal.Parse(this.nuTelRes)) : "** ****-****";
                    string email = this.emailResp != null ? this.emailResp : "**************";

                    return "( Contato: (M) " + nuCel + " - (R) " + nuRes + " - (C) " + nuCom + " - Email: " + email + " )";
                }
            }

            public double Dias
            {
                get
                {
                    if (DataPagamento != null)
                        return DataVencimento.Subtract(DataPagamento.Value).TotalDays;
                    else
                        return DataVencimento.Subtract(DateTime.Today).TotalDays;
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
                            return 0;
                        }
                    }
                    else if (this.Status == "Q" || this.Status == "P")
                    {
                        return this.JurosPago;
                    }

                    return 0;
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
                            return 0;
                        }
                    }
                    else if (this.Status == "Q")
                    {
                        return this.MultaPago;
                    }

                    return 0;
                }
            }

            public decimal? DescontoDesc
            {
                get
                {
                    if (this.Status == "A" || this.Status == "R")
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

            public decimal Total
            {
                get
                {
                    return (this.ValorDocumento + (this.MultaDesc ?? 0) + (this.JurosDesc ?? 0) - (this.DescontoDesc ?? 0));
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

                    return null;
                }
            }

            public decimal? Saldo
            {
                get
                {
                    if (this.Status == "P" || this.Status == "Q")
                        return this.ValorPago != null ? this.ValorPago.Value - (this.ValorDocumento + (this.MultaDesc ?? 0) + (this.JurosDesc ?? 0) - (this.DescontoDesc ?? 0)) : 0 - (this.ValorDocumento + (this.MultaDesc ?? 0) + (this.JurosDesc ?? 0) - (this.DescontoDesc ?? 0));
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
                        return "Pré-Mat";
                    }

                    return null;
                }
            }

            public string NomeDesc
            {
                get
                {
                    return this.Matricula.ToString() + "-" + this.Nome;
                }
            }
        }
        #endregion

        private void lblValorDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorDocto.Text != "")
            {
                totParcValorDocto = totParcValorDocto + decimal.Parse(lblValorDocto.Text);
            }
        }

        private void lblJurosDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblJurosDocto.Text != "")
            {
                totParcValorJuros = totParcValorJuros + decimal.Parse(lblJurosDocto.Text);
            }
        }

        private void lblMultaDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblMultaDocto.Text != "")
            {
                totParcValorMulta = totParcValorMulta + decimal.Parse(lblMultaDocto.Text);
            }
        }

        private void lblDesctoDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblDesctoDocto.Text != "")
            {
                totParcValorDescto = totParcValorDescto + decimal.Parse(lblDesctoDocto.Text);
            }
        }

        private void lblValorTotDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorTotDocto.Text != "")
            {
                totParcValorTotal = totParcValorTotal + decimal.Parse(lblValorTotDocto.Text);
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotParcSaldo.Text = String.Format("{0:#,##0.00}", totParcValorSaldo);
            totGeralValorSaldo = totGeralValorSaldo + totParcValorSaldo;
            totParcValorSaldo = 0;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotGeralSaldo.Text = String.Format("{0:#,##0.00}", totGeralValorSaldo);
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (int.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "- " + obj.Text.Replace("-", "").Replace("+", "");
                }
                else if (int.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "+ " + obj.Text.Replace("-", "").Replace("+", "");
                }
                else
                {
                    obj.ForeColor = Color.Black;
                    obj.Text = "0";
                }
            }
        }

        private void lblValorSaldo_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorSaldo.Text != "")
            {
                totParcValorSaldo = totParcValorSaldo + decimal.Parse(lblValorSaldo.Text);
            }
        }

        private void lblValorSaldo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {

                if (decimal.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = obj.Text.Replace("-", "").Replace("+", "");
                }
                else if (decimal.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = obj.Text.Replace("-", "").Replace("+", "");
                }
                else
                {
                    obj.ForeColor = Color.Black;
                    obj.Text = "0";
                }
            }
        }
    }
}
