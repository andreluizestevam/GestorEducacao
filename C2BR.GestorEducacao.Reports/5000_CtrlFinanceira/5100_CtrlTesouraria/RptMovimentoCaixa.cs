using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5100_CtrlTesouraria
{
    public partial class RptMovimentoCaixa : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        decimal totRecbto, totPagto = 0;

        #region ctor

        public RptMovimentoCaixa()
        {
            InitializeComponent();
        }
        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codEmp,
                              DateTime dtMovto,
                              DateTime? dtPagto,
                              int codCaixa,
                              string situCaixa,
                              string infos,
                              List<DateTime> datas,
                              Boolean alt
                             )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion
                if (alt == false)
                {
                    detailReportBand2.Visible = false;
                   
                }
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region QueryInformacaoCaixa

                //DateTime[] horarios = new DateTime[] { Convert.ToDateTime("2013-03-08 10:17:38"), Convert.ToDateTime("2013-03-08 08:53:52.990") };

                var infoCaixa = (from tb295 in ctx.TB295_CAIXA
                                 join tb113 in ctx.TB113_PARAM_CAIXA on tb295.CO_CAIXA equals tb113.CO_CAIXA
                                 join admUsuAber in ctx.ADMUSUARIO on tb295.CO_USUARIO_ABERT equals admUsuAber.ideAdmUsuario
                                 join tb03Abe in ctx.TB03_COLABOR on admUsuAber.CodUsuario equals tb03Abe.CO_COL
                                 join admUsuFecha in ctx.ADMUSUARIO on tb295.CO_USUARIO_FECHA equals admUsuFecha.ideAdmUsuario into srf
                                 from x in srf.DefaultIfEmpty()
                                 where tb295.CO_CAIXA == codCaixa
                                 && (tb295.DT_MOVIMENTO.Year == dtMovto.Year && tb295.DT_MOVIMENTO.Month == dtMovto.Month
                                 && tb295.DT_MOVIMENTO.Day == dtMovto.Day)
                                 && (situCaixa != "T" ? tb113.CO_SITU_CAIXA == situCaixa : situCaixa == "T")
                                
                                 select new InforCaixa
                                 {
                                     FuncCaixa = tb295.TB03_COLABOR.CO_MAT_COL.Insert(5, "-").Insert(2, ".") + " - " + tb295.TB03_COLABOR.NO_COL,
                                     FuncAbertCaixa = tb03Abe.CO_MAT_COL.Insert(5, "-").Insert(2, ".") + " - " + tb03Abe.NO_COL,
                                     DataAbertura = tb295.DT_ABERTURA_CAIXA,
                                     ValorAbert = tb295.VR_ABERTURA_CAIXA,
                                     CoFuncFechaCaixa = x != null ? x.CodUsuario : 0,
                                     DataFechamento = tb295.DT_FECHAMENTO_CAIXA,
                                     DataMovimento = tb295.DT_MOVIMENTO
                                 });

                #endregion

                CaixaGeral geral = new CaixaGeral();
                geral.lstInforCaixa = new List<InforCaixa>();
                geral.lstMovimCaixa = new List<MovimentacaoCaixa>();
                geral.lstOperCaixa = new List<OperacaoCaixa>();

                //var caixa = infoCaixa.FirstOrDefault();

                foreach (InforCaixa caixa in infoCaixa.Where(x => datas.Contains(x.DataMovimento.Value) ))
                {
                    // Se não encontrou o caixa
                    if (caixa == null)
                        return -1;
                    else
                        caixa.FuncFechaCaixa = caixa.CoFuncFechaCaixa != 0 ? TB03_COLABOR.RetornaPeloCoCol(caixa.CoFuncFechaCaixa).NO_COL : "";

                    geral.lstInforCaixa.Add(caixa);
                    geral.FuncCaixa = caixa.FuncCaixa;

                    #region QueryOperacaoCaixa

                    var vLstOperCaixa = (from tb297 in ctx.TB297_OPER_CAIXA
                                         join tb113 in ctx.TB113_PARAM_CAIXA on tb297.TB295_CAIXA.CO_CAIXA equals tb113.CO_CAIXA
                                         join admUsuAber in ctx.ADMUSUARIO on tb297.CO_USUARIO equals admUsuAber.ideAdmUsuario
                                         join tb03Abe in ctx.TB03_COLABOR on admUsuAber.CodUsuario equals tb03Abe.CO_COL
                                         where tb297.TB295_CAIXA.CO_CAIXA == codCaixa
                                         && (tb297.TB295_CAIXA.DT_MOVIMENTO.Year == dtMovto.Year && tb297.TB295_CAIXA.DT_MOVIMENTO.Month == dtMovto.Month
                                         && tb297.TB295_CAIXA.DT_MOVIMENTO.Day == dtMovto.Day)
                                         && (situCaixa != "T" ? tb113.CO_SITU_CAIXA == situCaixa : situCaixa == "T")
                                         && tb297.TB295_CAIXA.DT_MOVIMENTO == caixa.DataMovimento
                                         select new OperacaoCaixa
                                         {
                                             DataOperacao = tb297.DT_CADASTRO,
                                             HoraOperacao = tb297.HR_CADASTRO,
                                             FuncioOperacao = tb03Abe.CO_MAT_COL.Insert(5, "-").Insert(2, ".") + " - " + tb03Abe.NO_COL,
                                             TipoOperacao = tb297.FLA_TIPO,
                                             FormaOperacao = tb297.TB118_TIPO_RECEB.DE_RECEBIMENTO,
                                             ValorOpera = tb297.VALOR,
                                             DescTipoOperacao = tb297.FLA_TIPO == "A" ? "APORTE FINANCEIRO" : "SANGRIA FINANCEIRA",
                                             ValorAporte = tb297.FLA_TIPO == "A" ? tb297.VALOR : 0,
                                             ValorSangria = tb297.FLA_TIPO == "S" ? tb297.VALOR : 0
                                         }).ToList();

                    geral.lstOperCaixa = vLstOperCaixa;
                    #endregion

                    #region QueryMovimento
                    var vLstMovimento = (from tb296 in ctx.TB296_CAIXA_MOVIMENTO
                                         join tb113 in ctx.TB113_PARAM_CAIXA on tb296.CO_CAIXA equals tb113.CO_CAIXA
                                         join tb156 in ctx.TB156_FormaPagamento on tb296.CO_SEQMOV_CAIXA equals tb156.CO_CAIXA_MOVIMENTO
                                         join tb118 in ctx.TB118_TIPO_RECEB on tb156.CO_TIPO_REC equals tb118.CO_TIPO_REC
                                         join tb47 in ctx.TB47_CTA_RECEB on tb296.NU_DOC_CAIXA equals tb47.NU_DOC
                                         join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU into srf
                                         from x in srf.DefaultIfEmpty()
                                         where tb296.TB295_CAIXA.CO_CAIXA == codCaixa
                                         && (tb296.DT_MOVIMENTO.Year == dtMovto.Year && tb296.DT_MOVIMENTO.Month == dtMovto.Month
                                         && tb296.DT_MOVIMENTO.Day == dtMovto.Day)
                                         && tb296.DT_MOVIMENTO == caixa.DataMovimento
                                         && (dtPagto != null ? (tb296.DT_PAGTO_DOC.Value.Year == dtPagto.Value.Year && tb296.DT_PAGTO_DOC.Value.Month == dtPagto.Value.Month
                                         && tb296.DT_PAGTO_DOC.Value.Day == dtPagto.Value.Day) : dtPagto == null)
                                         && (situCaixa != "T" ? tb113.CO_SITU_CAIXA == situCaixa : situCaixa == "T")
                                         && tb47.NU_PAR == tb296.NU_PAR_DOC_CAIXA
                                         && (tb47.DT_CAD_DOC.Year == tb296.DT_DOC_CAIXA.Value.Year && tb47.DT_CAD_DOC.Month == tb296.DT_DOC_CAIXA.Value.Month &&
                                         tb47.DT_CAD_DOC.Day == tb296.DT_DOC_CAIXA.Value.Day)
                                         && tb47.CO_EMP == tb296.CO_EMP_CAIXA
                                         select new MovimentacaoCaixa
                                         {
                                             ValorPago = tb156.VR_RECEBIDO,
                                             ValorDocto = tb47.VR_PAR_DOC,
                                             ValorDesctoPago = tb47.VR_DES_PAG,
                                             ValorMultaPago = tb47.VR_MUL_PAG,
                                             ValorJurosPago = tb47.VR_JUR_PAG,
                                             NIRE = x != null ? x.NU_NIRE : 0,
                                             Aluno = x != null ? x.NO_ALU : "",
                                             DtMovimento = tb296.DT_REGISTRO,
                                             DtVencimento = tb47.DT_VEN_DOC,
                                             DtPagamento = tb296.DT_PAGTO_DOC,
                                             Parcela = tb47.NU_PAR,
                                             Responsavel = (tb296.TP_CLIENTE_DOC == "A" ? tb47.TB108_RESPONSAVEL.NO_RESP : tb47.TB103_CLIENTE.NO_FAN_CLI),
                                             ResponsavelCPF = (tb296.TP_CLIENTE_DOC == "A" ? tb47.TB108_RESPONSAVEL.NU_CPF_RESP : ""),
                                             Documento = tb296.NU_DOC_CAIXA,
                                             SiglaTpDocumento = tb47.TB086_TIPO_DOC.SIG_TIPO_DOC,
                                             Observacao = tb156.DE_OBS,
                                             DeRecebimento = tb118.DE_RECEBIMENTO,
                                             Tipo = tb296.TP_OPER_CAIXA == "C" ? "REC" : "PAG",
                                             seq = tb296.CO_SEQMOV_CAIXA,
                                             Situacao = tb296.FLA_SITU_DOC,
                                             CodReceb = tb118.CO_TIPO_REC
                                                
                                         }).OrderBy(p => p.DeRecebimento).ThenBy(p => p.DtPagamento).ThenBy(p => p.DtMovimento).ToList();
                    /*foreach (var mov in vLstMovimento)
                    {
                        var res = (from tb156 in ctx.TB156_FormaPagamento
                                 where tb156.CO_CAIXA_MOVIMENTO == mov.seq
                                 select new AlteracaoMovimento
                                 {
                                     ValorPago = tb156.VR_RECEBIDO
                                 }).Sum(x => x.ValorPago);
                        mov.ValorPago = res;
                    }*/

                    

                    foreach (var mov in vLstMovimento)
                    {
                        mov.lstAltCaixa = (                                        
                                           from tb192 in ctx.TB192_CAIXA_LOGMOV 
                                           join tb03 in ctx.TB03_COLABOR on tb192.CO_COL equals tb03.CO_COL
                                           join tb086 in ctx.TB086_TIPO_DOC on tb192.CO_TIPO_DOC equals tb086.CO_TIPO_DOC
                                           join tb193 in ctx.TB193_FORMAPAGAMENTO_LOG on tb192.ID_CAIXA_LOGMOV equals tb193.TB192_CAIXA_LOGMOV.ID_CAIXA_LOGMOV
                                           into srf from tb193 in srf.DefaultIfEmpty()
                                           where tb192.NU_DOC_CAIXA == mov.Documento && (tb193 != null ? tb193.CO_TIPO_REC == mov.CodReceb : 0 == 0) 
                                           && (tb193 != null ? tb193.TB192_CAIXA_LOGMOV.NU_DOC_CAIXA == mov.Documento : 0 == 0)
                                         
                                           select new AlteracaoMovimento
                                           {
                                               data = tb192.DT_LOG,
                                               ip = tb192.NR_IP_ACESS_ATIVI_LOG,
                                               matr = tb03.CO_MAT_COL,                                               
                                               ValorDesctoPago = tb192.VR_DES_DOC,
                                               ValorDocto = tb192.VR_DOCU,
                                               ValorJurosPago = tb192.VR_JUR_DOC,
                                               ValorMultaPago = tb192.VR_MUL_DOC,
                                               DtMovimento = tb192.DT_MOVIMENTO,
                                               Documento = tb192.NU_DOC_CAIXA,
                                               Tipo = tb192.TP_OPER_CAIXA,
                                               Observacao = tb192.DE_OBS_DOC,
                                               Parcela = tb192.NU_PAR_DOC_CAIXA,
                                               Motivo = tb192.DE_MOTIV_CANCEL,
                                               TipoRec = tb192.CO_TIPO_REC,
                                               SiglaTpDocumento = tb086.SIG_TIPO_DOC,
                                               ValorPago = tb193.VR_RECEBIDO,
                                               Situacao = tb192.FLA_SITU_DOC

                                           }).OrderByDescending(x => x.data).Distinct().ToList();
                        if (mov.lstAltCaixa.Count > 0)
                        {
                            mov.TituloAltMov = "ALTERAÇÕES NO MOVIMENTO";
                        }
                        /*if (mov.lstAltCaixa.Count > 0)
                        {
                            foreach (var mov2 in mov.lstAltCaixa)
                            {
                                var Valorpg = (from tb193 in ctx.TB193_FORMAPAGAMENTO_LOG
                                               where tb193.TB192_CAIXA_LOGMOV.NU_DOC_CAIXA == mov2.Documento
                                              && tb193.CO_TIPO_REC == mov2.TipoRec

                                               select new AlteracaoMovimento
                                             {
                                                 ValorPago = tb193.VR_RECEBIDO
                                             }).FirstOrDefault();
                                
                                mov2.ValorPago = Valorpg.ValorPago;
                            }
                        }*/
                    }
                  
                   
                    foreach (var iMov in vLstMovimento)
                    {
                        totRecbto = totRecbto + (iMov.Tipo == "REC" ? (decimal)iMov.ValorPago : 0);
                        totPagto = totPagto + (iMov.Tipo == "PAG" ? (decimal)iMov.ValorPago : 0);
                    }

                   
                    geral.lstMovimCaixa = vLstMovimento;
                    #endregion

                    xrTable6.CanShrink = true;

                }
                bsReport.Clear();
                bsReport.Add(geral);
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Helper Movimentacao Caixa

        public class CaixaGeral
        {
            public string FuncCaixa { get; set; }
            public List<InforCaixa> lstInforCaixa { get; set; }
            public List<OperacaoCaixa> lstOperCaixa { get; set; }
            public List<MovimentacaoCaixa> lstMovimCaixa { get; set; }
        }

        public class InforCaixa
        {
            public string FuncCaixa { get; set; }
            public string FuncAbertCaixa { get; set; }
            public int CoFuncFechaCaixa { get; set; }
            public string FuncFechaCaixa { get; set; }
            public decimal? ValorAbert { get; set; }
            public DateTime? DataAbertura { get; set; }
            public DateTime? DataFechamento { get; set; }
            public DateTime? DataMovimento { get; set; }
        }

        public class OperacaoCaixa
        {
            public string TipoOperacao { get; set; }
            public string DescTipoOperacao { get; set; }
            public decimal? ValorOpera { get; set; }
            public decimal? ValorAporte { get; set; }
            public decimal? ValorSangria { get; set; }
            public DateTime? DataOperacao { get; set; }
            public string HoraOperacao { get; set; }
            public string FormaOperacao { get; set; }
            public string FuncioOperacao { get; set; }
        }

        public class MovimentacaoCaixa
        {
            public int CodReceb { get; set; }
            public string Situacao { get; set; }
            public List<AlteracaoMovimento> lstAltCaixa { get; set; }
            public string Responsavel { get; set; }
            public string ResponsavelCPF { get; set; }
            public string Aluno { get; set; }
            public DateTime DtMovimento { get; set; }
            public DateTime DtVencimento { get; set; }
            public DateTime? DtPagamento { get; set; }
            public string Documento { get; set; }
            public string DeRecebimento { get; set; }
            public string SiglaTpDocumento { get; set; }
            public string Observacao { get; set; }
            public string Tipo { get; set; }
            public decimal ValorDocto { get; set; }
            public decimal? ValorMultaPago { get; set; }
            public decimal? ValorJurosPago { get; set; }
            public decimal? ValorDesctoPago { get; set; }
            public decimal? ValorPago { get; set; }
            public int Parcela { get; set; }
            public int NIRE { get; set; }
            public int seq { get; set; }
            public string DescNIRE
            {
                get
                {
                    return (this.NIRE != 0) ? this.NIRE.ToString() : "XXXXX";
                }
            }

            public string DescResponsavel
            {
                get
                {
                    return (this.Responsavel.Length > 10) ? this.Responsavel.Substring(0, 10) + "..." : this.Responsavel;
                }
            }

            public string AlunoResponsavel
            {
                get
                {
                    return "Aluno: " + DescNIRE + " - " + Aluno + " - Responsável: " + ResponsavelCPF.Format(TipoFormat.CPF) + " - " + Responsavel;
                }
            }

            public string UpperDeRecebimento
            {
                get
                {
                    return this.DeRecebimento.ToUpper();
                }
            }

            public string TituloAltMov { get; set; }
        }

        public class AlteracaoMovimento
        {
            public string Situacao { get; set; }
            public int? TipoRec { get; set; }
            public string Motivo { get; set; }
            public DateTime data { get; set; }
            public string hora
            {
                get
                {
                    return this.data.ToString("hh:mm");
                } 
            }
            public string ip { get; set; }
            public string matr { get; set; }
            public DateTime DtMovimento { get; set; }
            public DateTime DtVencimento { get; set; }
            public DateTime? DtPagamento { get; set; }
            public string Documento { get; set; }
            public string DeRecebimento { get; set; }
            public string SiglaTpDocumento { get; set; }
            public string Observacao { get; set; }
            public string Tipo { get; set; }
            public string TipoDesc
            {
                get
                {
                    string retorno;
                    if (this.Tipo == "C")
                    {
                        retorno = "REC";
                    }else{
                        retorno =  "PAG";
                    }
                    return retorno;
                }
            }
            public decimal ValorDocto { get; set; }
            public decimal? ValorMultaPago { get; set; }
            public decimal? ValorJurosPago { get; set; }
            public decimal? ValorDesctoPago { get; set; }
            public decimal? ValorPago { get; set; }
            public int? Parcela { get; set; }
            public int NIRE { get; set; }
            public string descMovimento
            {
                get {
                    var retorno =  " IP: " + this.ip + " - Matricula Colab.: " + this.matr.ToString();
                    if (this.Motivo != null)
                    {
                        retorno = retorno + " - Motivo Cancelamento: " + this.Motivo;

                    }
                    return retorno;
                }
            }
           
        }
        #endregion

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotRecbto.Text = String.Format("{0:#,##0.00}", totRecbto);
            lblTotPagto.Text = String.Format("{0:#,##0.00}", totPagto);
            lblTotResul.Text = String.Format("{0:#,##0.00}", totRecbto - totPagto);
        }

        private void xrTable6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
         
        }
        //Variáveis para Calcular o Total por Tipo de Movimentação
        decimal valorDocTipoMov;
        decimal mulDocTipoMov;
        decimal jurDocTipoMov;
        decimal descDocTipoMov;
        decimal pagoDocTipoMov;

        //Variáveis para Calcular o Total por Geral de Movimentação
        decimal tvalorDocTipoMov;
        decimal tmulDocTipoMov;
        decimal tjurDocTipoMov;
        decimal tdescDocTipoMov;
        decimal tpagoDocTipoMov;

        #region Calcular Total do Vlor Doc por Tipo de Movimentação
        private void xrTableCell13_AfterPrint(object sender, EventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "" && xrTableCell79.Text == "A")
            {
                valorDocTipoMov = valorDocTipoMov + decimal.Parse(obj.Text);
                tvalorDocTipoMov = tvalorDocTipoMov + decimal.Parse(obj.Text);
            }
        }

        private void xrLabel22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel22.Text = valorDocTipoMov.ToString();
            
        }

        private void xrLabel22_AfterPrint(object sender, EventArgs e)
        {
            valorDocTipoMov = 0;
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel4.Text = tvalorDocTipoMov.ToString();

        }

        #endregion

        #region Calcular Total de Multa por Tipo de Movimentação
        private void xrTableCell45_AfterPrint(object sender, EventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "" && xrTableCell79.Text == "A")
            {
                mulDocTipoMov = mulDocTipoMov + decimal.Parse(obj.Text);
                tmulDocTipoMov = tmulDocTipoMov + decimal.Parse(obj.Text);
            }
        }
        
        private void xrLabel24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel24.Text = mulDocTipoMov.ToString();
        }

        private void xrLabel24_AfterPrint(object sender, EventArgs e)
        {
            mulDocTipoMov = 0;
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel2.Text = tmulDocTipoMov.ToString();
        }

        #endregion

        #region Calcular Total de Juro por Tipo de Movimentação
        private void xrTableCell46_AfterPrint(object sender, EventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "" && xrTableCell79.Text == "A")
            {
                jurDocTipoMov = jurDocTipoMov + decimal.Parse(obj.Text);
                tjurDocTipoMov = tjurDocTipoMov + decimal.Parse(obj.Text);
            }
        }

        private void xrLabel25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel25.Text = jurDocTipoMov.ToString();
        }

        private void xrLabel25_AfterPrint(object sender, EventArgs e)
        {
            jurDocTipoMov = 0;
        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel3.Text = tjurDocTipoMov.ToString();
        }

        #endregion

        #region Calcular Total de Desc por Tipo de Movimentação
        private void xrTableCell14_AfterPrint(object sender, EventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "" && xrTableCell79.Text == "A")
            {
                descDocTipoMov = descDocTipoMov + decimal.Parse(obj.Text);
                tdescDocTipoMov = tdescDocTipoMov + decimal.Parse(obj.Text);
            }
        }

        private void xrLabel26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel26.Text = descDocTipoMov.ToString();
        }

        private void xrLabel26_AfterPrint(object sender, EventArgs e)
        {
            descDocTipoMov = 0;
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel6.Text = tdescDocTipoMov.ToString();
        }

        #endregion

        #region Calcular Total Pago por Tipo de Movimentação
        private void xrTableCell47_AfterPrint(object sender, EventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "" && xrTableCell79.Text == "A")
            {
                pagoDocTipoMov = pagoDocTipoMov + decimal.Parse(obj.Text);
                tpagoDocTipoMov = tpagoDocTipoMov + decimal.Parse(obj.Text);
            }
        }

        private void xrLabel27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel27.Text = pagoDocTipoMov.ToString();
        }

        private void xrLabel27_AfterPrint(object sender, EventArgs e)
        {
            pagoDocTipoMov = 0;
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel5.Text = tpagoDocTipoMov.ToString();
        }


        #endregion

    }
}
