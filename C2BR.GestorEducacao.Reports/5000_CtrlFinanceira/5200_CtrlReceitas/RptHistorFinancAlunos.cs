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
using System.Resources;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptHistorFinancAlunos : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totValorDif, totValorMulta, totValorJuros, totValorDescto = 0;
        private decimal totValorDoctoG, totValorMultaG, totValorJurosG, totValorDesctoG = 0;

        public RptHistorFinancAlunos()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_EMP_UNID_CONT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_ALU,
                              int strP_CO_RESP,
                              string strP_IC_SIT_DOC,
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

                string coTipoEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP).CO_TIPO_UNID;
                if (coTipoEmp == "PGS")
                    lblTitulo.Text = "HISTÓRICO FINANCIERO DE PACIENTES";
                else
                    lblTitulo.Text = "HISTÓRICO FINANCIERO DE ALUNOS";

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                ///Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada


                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                           join tb108 in ctx.TB108_RESPONSAVEL on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           join tb44 in ctx.TB44_MODULO on tb47.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb01 in ctx.TB01_CURSO on tb47.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb47.CO_TUR equals tb129.CO_TUR
                           join tb25 in ctx.TB25_EMPRESA on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP
                           where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strP_CO_EMP_REF != 0 ? tb47.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                           && (strP_IC_SIT_DOC != "-1" ? tb47.IC_SIT_DOC == strP_IC_SIT_DOC : 0 == 0)
                           && (strP_CO_MODU_CUR != 0 ? tb47.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                           && (strP_CO_CUR != 0 ? tb47.CO_CUR == strP_CO_CUR : 0 == 0)
                           && (strP_CO_ANO_MES_MAT != "0" ? tb47.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT : 0 == 0)
                           && (strP_CO_ALU != 0 ? tb47.CO_ALU == strP_CO_ALU : 0 == 0)
                           && (strP_CO_TUR != 0 ? tb47.CO_TUR == strP_CO_TUR : 0 == 0)
                           && (strP_CO_AGRUP != -1 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : 0 == 0)
                           && (strP_CO_EMP_UNID_CONT != 0 ? tb47.CO_EMP_UNID_CONT == strP_CO_EMP_UNID_CONT : 0 == 0)
                           && (strP_CO_RESP != 0 ? tb47.TB108_RESPONSAVEL.CO_RESP == strP_CO_RESP : 0 == 0)
                           && (!incluCance && strP_IC_SIT_DOC != "C" ? tb47.IC_SIT_DOC != "C" : 0 == 0)
                           select new RelHistoFinanAluno
                           {
                               CO_EMP_LOGADA = strP_CO_EMP,
                               SiglaUnid = tb25.sigla,
                               Nome = tb07.NO_ALU,
                               coAluno = (tb47.CO_ALU ?? 0),
                               Responsavel = tb47.TB108_RESPONSAVEL.NO_RESP ,
                               Status = tb47.IC_SIT_DOC,
                               Documento = tb47.NU_DOC,
                               DataVencimento = tb47.DT_VEN_DOC,
                               DataPagamento = tb47.DT_REC_DOC,
                               ValorDocumento = tb47.VR_PAR_DOC,
                               Juros = (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0),
                               Multa = (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0),
                               Desconto = (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0),
                               DescontoBolsa = (tb47.VL_DES_BOLSA_ALUNO != null ? tb47.VL_DES_BOLSA_ALUNO : 0),
                               TpDescBolsa = tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO,
                               DescontoPago = (tb47.VR_DES_PAG != null ? tb47.VR_DES_PAG : 0),
                               TpMulta = tb47.CO_FLAG_TP_VALOR_MUL,
                               TpJuros = tb47.CO_FLAG_TP_VALOR_JUR,
                               TpDesc = tb47.CO_FLAG_TP_VALOR_DES,
                               JurosPago = (tb47.VR_JUR_PAG != null ? tb47.VR_JUR_PAG : 0),
                               MultaPago = (tb47.VR_MUL_PAG != null ? tb47.VR_MUL_PAG : 0),
                               ValorPago = tb47.VR_PAG,
                               Parcela = tb47.NU_PAR,
                               NIRE = tb07.NU_NIRE,
                               CpfResp = tb108.NU_CPF_RESP,
                               noMod = tb44.DE_MODU_CUR,
                               noCur = tb01.NO_CUR,
                               noTur = tb129.NO_TURMA,
                               coTipoBolsa = tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA
                           }).OrderBy(p => p.Nome).ThenBy(p => p.DataVencimento).ThenBy(p => p.Parcela);

                var res = lst.ToList();

                foreach (var iLst in res)
                {
                    iLst.DataDif = DateTime.Now.Subtract(iLst.DataVencimento);
                }

                #endregion

                ///Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                ///Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelHistoFinanAluno at in res)
                {

                    switch (at.CO_TIPO_EMP)
                    {
                        case "PGS":
                            xrLabel7.Text = "Paciente: ";
                            break;
                        case "PGE":
                        default:
                            xrLabel7.Text = "Aluno: ";
                            break;
                    }

                    if (at.coTipoBolsa != null)
                    {
                        at.noTipoBolsa = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(at.coTipoBolsa.Value).NO_TIPO_BOLSA;
                    }
                    else
                    {
                        at.noTipoBolsa = "";
                    }
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Histórico Financeiro do Aluno

        public class RelHistoFinanAluno
        {
            public int CO_EMP_LOGADA { get; set; } 
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
            public string CpfResp{get;set;}
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
            public string noMod { get; set; }
            public string noCur { get; set; }
            public string noTur { get; set; }
            public int? coTipoBolsa { get; set; }
            public string noTipoBolsa { get; set; }

            public string DescResp
            {
                get
                {
                    return "( Responsável Financeiro: " + this.CpfRespDesc + " - " + this.Responsavel + ")";
                }
            }

            public string CO_TIPO_EMP
            {
                get
                {
                    return TB25_EMPRESA.RetornaPelaChavePrimaria(this.CO_EMP_LOGADA).CO_TIPO_UNID;
                }
            }
            public string DescAlu
            {
                get
                {
                    if(this.CO_TIPO_EMP == "PGS")
                        return "( NIRE: " + this.NIRE + " )";
                    else
                        return "( NIRE: " + this.NIRE + " - Mod: " + this.noMod + " - Série: " + this.noCur + " - Turma: " + this.noTur + " - Convênio: " + (this.noTipoBolsa != "" ? this.noTipoBolsa + " )" : "Nenhum )");
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

            public string DescricaoDesc
            {
                get
                {
                    switch (this.Status)
                    {
                        case "A":
                            return this.Documento + " / " + "Em Aberto";
                            break;
                        case "Q":
                            return this.Documento + " / " + "Quitado";
                            break;
                        case "C":
                            return this.Documento + " / " + "Cancelado";
                            break;
                        case "R":
                            return this.Documento + " / " + "Pré-Mat";
                            break;
                        default:
                            return null;
                            break;
                    }
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

            public int DiasDesc
            {
                get
                {
                    double dias = 0;
                    if (DataPagamento != null)
                    {
                        if (DataPagamento > this.DataVencimento)
                        { //atrasado
                            dias = this.DataPagamento.Value.Subtract(DataVencimento).TotalDays;
                        }
                        else if (DataPagamento.Value < this.DataVencimento)
                        {
                            dias = this.DataPagamento.Value.Subtract(DataVencimento).TotalDays;
                        }
                        else
                            return 0;
                    }
                    else
                    {
                        dias = DateTime.Today.Subtract(this.DataVencimento).TotalDays;
                    }
                    return (int)Math.Round(dias, 0);
                }
            }

            public decimal? ValorDifParc
            {
                get
                {
                    return (this.ValorDocumento + (this.JurosDesc ?? 0) + (this.MultaDesc ?? 0) - (this.DescontoDesc ?? 0)) - (this.ValorPago ?? 0);
                }
            }

            public int coAluno
            {
                set
                {
            //        var matricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
            //                         where tb08.CO_ALU == value
            //                         orderby tb08.DT_CAD_MAT descending 
            //                         select tb08).FirstOrDefault();
            //        if (matricula != null)
            //        {
            //            var dados = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
            //                         join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
            //                         join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
            //                         join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
            //                         where tb08.CO_ALU_CAD == matricula.CO_ALU_CAD
            //                         select new
            //                         {
            //                             tb08,
            //                             tb01,
            //                             tb07,
            //                             tb129
            //                         }).FirstOrDefault();
            //            if (dados != null)
            //            {
            //                if (dados.tb01.TB44_MODULO == null)
            //                    dados.tb01.TB44_MODULOReference.Load();
            //                if (dados.tb07.TB108_RESPONSAVEL == null)
            //                    dados.tb07.TB108_RESPONSAVELReference.Load();
            //                if (dados.tb07.TB148_TIPO_BOLSA == null)
            //                    dados.tb07.TB148_TIPO_BOLSAReference.Load();
            //                this.DescSerie = " - Mod: " + dados.tb01.TB44_MODULO.DE_MODU_CUR + " - Série: " + dados.tb01.NO_CUR +
            //                           " - Turma: " + dados.tb129.NO_TURMA + " - Convênio: " + (dados.tb07.TB148_TIPO_BOLSA != null ? dados.tb07.TB148_TIPO_BOLSA.NO_TIPO_BOLSA + " )" : "Nenhum )");

            //                this.Nome = dados.tb07.NO_ALU;
            //                this.NIRE = dados.tb07.NU_NIRE;
            //                this.CpfResp = dados.tb07.TB108_RESPONSAVEL.NU_CPF_RESP;
            //            }
            //        }
                }
            }
        }
        #endregion

        #region Evento de componentes da página

        private void lblValorPago_AfterPrint(object sender, EventArgs e)
        {
        }

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

        private void lblValorTotDocto_AfterPrint(object sender, EventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (!obj.Text.Contains('-'))
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (obj.Text.Contains('-'))
                {
                    obj.ForeColor = Color.Blue;
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

        private void lbTotalPago_AfterPrint(object sender, EventArgs e)
        {

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
                if (obj.Text.Contains('-'))
                {
                    //obj.ForeColor = Color.Blue;
                    obj.ForeColor = Color.Black;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (!obj.Text.Contains('-'))
                {
                    //obj.ForeColor = Color.Red;
                    obj.ForeColor = Color.Black;
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
                if (obj.Text.Contains('-'))
                {
                    //obj.ForeColor = Color.Blue;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (!obj.Text.Contains('-'))
                {
                    //obj.ForeColor = Color.Red;
                    obj.Text = "+ " + obj.Text.Replace("+", "").Replace("-", "");
                }
            }
        }

        private void xrTableCell32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (obj.Text.Contains('-'))
                {
                    //obj.ForeColor = Color.Blue;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (!obj.Text.Contains('-'))
                {
                    //obj.ForeColor = Color.Red;
                    obj.Text = "+ " + obj.Text.Replace("+", "").Replace("-", "");
                }
            }
        }

        #endregion
    }
}
