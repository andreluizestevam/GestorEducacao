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

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3200_ExtratoEntrega
{
    public partial class RptExtratoEntrega : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totValorDif, totValorMulta, totValorJuros, totValorDescto = 0;
        private decimal totValorDoctoG, totValorMultaG, totValorJurosG, totValorDesctoG = 0;

       public RptExtratoEntrega()
        {
            InitializeComponent();
        }
          #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int intP_ID_REGIAO,
                              int intP_ID_AREA,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_ALU,
                              int strP_CO_RESP,
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

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                ///Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada


                var lst = (

                           from tb094 in ctx.TB094_ITEM_RESER_MEDIC
                           join tb092 in ctx.TB092_RESER_MEDIC on tb094.ID_ITEM_RESER_MEDIC equals tb092.ID_RESER_MEDIC
                           join tb90 in ctx.TB90_PRODUTO on tb094.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD                    
                           join tb07 in ctx.TB07_ALUNO on tb092.CO_ALU equals tb07.CO_ALU
                           join tb89 in ctx.TB89_UNIDADES on tb90.TB89_UNIDADES.CO_UNID_ITEM equals tb89.CO_UNID_ITEM

                           where tb07.TB906_REGIAO.ID_REGIAO == intP_ID_REGIAO

                           //where tb094.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst

                           //&& (strP_CO_EMP_REF != -1 ? tb47.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                           //&& (strP_IC_SIT_DOC != "-1" ? tb47.IC_SIT_DOC == strP_IC_SIT_DOC : 0 == 0)
                           //&& (strP_CO_MODU_CUR != -1 ? tb47.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                           //&& (strP_CO_CUR != -1 ? tb47.CO_CUR == strP_CO_CUR : 0 == 0)
                           //&& (strP_CO_ANO_MES_MAT != "-1" ? tb47.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT : 0 == 0)
                           //&& (strP_CO_ALU != -1 ? tb47.CO_ALU == strP_CO_ALU : 0 == 0)
                           //&& (strP_CO_TUR != -1 ? tb47.CO_TUR == strP_CO_TUR : 0 == 0)
                           //&& (strP_CO_AGRUP != -1 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : 0 == 0)
                           //&& (strP_CO_EMP_UNID_CONT != -1 ? tb47.CO_EMP_UNID_CONT == strP_CO_EMP_UNID_CONT : 0 == 0)
                           //&& (strP_CO_RESP != -1 ? tb47.TB108_RESPONSAVEL.CO_RESP == strP_CO_RESP : 0 == 0)
                           //&& (!incluCance && strP_IC_SIT_DOC != "C" ? tb47.IC_SIT_DOC != "C" : 0 == 0)
                           select new RelHistoFinanAluno
                           {
                               SiglaUnid = tb89.SG_UNIDADE,
                               DataEntrega = tb092.DT_RESER_MEDIC,
                               DataSolicitacao = tb092.DT_CADAS_RESER_MEDIC,
                               Medicamento = tb90.NO_PROD,
                               Unidade = tb89.NO_UNID_ITEM,
                               QuantidadeEntrega1 = tb094.QT_ENTR_MES1,
                               QuantidadeEntrega2 = tb094.QT_ENTR_MES2,
                               QuantidadeEntrega3 = tb094.QT_ENTR_MES3,
                               QuantidadeEntrega4 = tb094.QT_ENTR_MES4,
                               Quantidade1 = tb094.QT_MES1,
                               Quantidade2 = tb094.QT_MES2,
                               Quantidade3 = tb094.QT_MES3,
                               Quantidade4 = tb094.QT_MES4,
                               
                           }).OrderBy(p => p.Nome);

                var res = lst.ToList();

                foreach (var iLst in res)
                {
                    //iLst.DataEntrega = DateTime.Now.Subtract(iLst.DataEntrega);
                }

                #endregion

                ///Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                ///Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelHistoFinanAluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

         #region Classe Histórico Financeiro do Aluno

        public class RelHistoFinanAluno
        {
            public string SiglaUnid { get; set; }
            public string Nome { get; set; }
            public DateTime DataEntrega { get; set; }
            public DateTime? DataSolicitacao { get; set; }
            public string Medicamento { get; set; }
            public string Unidade { get; set; }
            public decimal QuantidadeEntrega1 { get; set; }
            public decimal QuantidadeEntrega2 { get; set; }
            public decimal QuantidadeEntrega3 { get; set; }
            public decimal QuantidadeEntrega4 { get; set; }
            public decimal? QuantidadeEntregue { get; set; }
            public decimal Quantidade1 { get; set; }
            public decimal Quantidade2 { get; set; }
            public decimal Quantidade3 { get; set; }
            public decimal Quantidade4 { get; set; }
            public decimal TotalQTD
            {
                get
                {
                    return this.Quantidade1 + this.Quantidade2 + this.Quantidade3 + this.Quantidade4;
                }
            }
            public decimal TotalEntre
            {
                get
                {
                    return this.QuantidadeEntrega1 + this.QuantidadeEntrega2 + this.QuantidadeEntrega3 + this.QuantidadeEntrega4;
                }
            }

            
            
        //    public string SiglaUnid { get; set; }
        //    public string Nome { get; set; }
        //    public int NIRE { get; set; }
        //    public string Responsavel { get; set; }
        //    public string DescSerie { get; set; }
        //    public DateTime DataVencimento { get; set; }
        //    public DateTime? DataPagamento { get; set; }
        //    public string Status { get; set; }
        //    public string Documento { get; set; }
        //    public decimal ValorDocumento { get; set; }
        //    public decimal? Juros { get; set; }
        //    public decimal? Multa { get; set; }
        //    public decimal? Desconto { get; set; }
        //    public decimal? DescontoBolsa { get; set; }
        //    public string TpMulta { get; set; }
        //    public string TpJuros { get; set; }
        //    public string TpDesc { get; set; }
        //    public string TpDescBolsa { get; set; }
        //    public decimal? Result { get; set; }
        //    public decimal? ValorPago { get; set; }
        //    public decimal? JurosPago { get; set; }
        //    public decimal? MultaPago { get; set; }
        //    public decimal? DescontoPago { get; set; }
        //    public TimeSpan DataDif { get; set; }
        //    public int Parcela { get; set; }
        //    public string CpfResp{get;set;}
        //    public string CpfRespDesc
            //{
            //    get
            //    {
                    //string retorno;
                    //if (this.CpfResp != null)
                    //{
                    //    retorno = this.CpfResp.Insert(3, ".");
                    //    retorno = retorno.Insert(7, ".");
                    //    retorno = retorno.Insert(11, "-");
            //        }
            //        else
            //        {
            //            retorno = "-";

            //        }
            //        return retorno;
            //    }
            //}

        //    public string DescResp
        //    {
        //        get
        //        {
        //            return "( Responsável Financeiro: " + this.CpfRespDesc + " - " + this.Responsavel + ")";
        //        }
        //    }
            
        //    public string DescAlu
        //    {
        //        get
        //        {
        //            return "( NIRE: " + this.NIRE + this.DescSerie;
        //        }
        //    }

        //    public decimal? JurosDesc
        //    {
        //        get
        //        {
        //            if (this.Status == "A" || this.Status == "R")
        //            {
        //                if (Convert.ToDecimal(this.DataDif.Days) > 0)
        //                {
        //                    if (this.TpJuros == "P")
        //                    {
        //                        Result = ((this.ValorDocumento / 100) * this.Juros) * Convert.ToDecimal(this.DataDif.Days);
        //                        return Result;

        //                    }
        //                    else if (this.TpJuros == "V")
        //                    {
        //                        Result = (this.Juros) * Convert.ToDecimal(this.DataDif.Days);
        //                        return Result;
        //                    }
        //                }
        //                else
        //                {
        //                    return null;
        //                }
        //            }
        //            else if (this.Status == "Q")
        //            {
        //                return this.JurosPago;
        //            }

        //            return null;
        //        }
        //    }

        //    public decimal? MultaDesc
        //    {
        //        get
        //        {
        //            if (this.Status == "A" || this.Status == "R")
        //            {
        //                if (Convert.ToDecimal(this.DataDif.Days) > 0)
        //                {
        //                    if (this.TpMulta == "P")
        //                    {
        //                        Result = ((this.ValorDocumento / 100) * this.Multa);
        //                        return Result;
        //                    }
        //                    else
        //                    {
        //                        return Multa;
        //                    }
        //                }
        //                else
        //                {
        //                    return null;
        //                }
        //            }
        //            else if (this.Status == "Q")
        //            {
        //                return this.MultaPago;
        //            }

        //            return null;
        //        }
        //    }

        //    public decimal? DescontoDesc
        //    {
        //        get
        //        {
        //            if (this.Status == "A" || this.Status == "R")
        //            {
        //                if (Convert.ToDecimal(this.DataDif.Days) < 0)
        //                {
        //                    if (this.TpDesc == "P")
        //                    {
        //                        Result = ((this.ValorDocumento / 100) * this.Desconto);

        //                        if (this.TpDescBolsa == "P" && this.DescontoBolsa != null)
        //                        {
        //                            Result = Result + ((this.ValorDocumento / 100) * this.DescontoBolsa);
        //                        }
        //                        else if (this.DescontoBolsa != null)
        //                        {
        //                            Result = Result + this.DescontoBolsa;
        //                        }

        //                        return Result;
        //                    }
        //                    else
        //                    {
        //                        Result = this.Desconto;

        //                        if (this.TpDescBolsa == "P" && this.DescontoBolsa != null)
        //                        {
        //                            Result = Result + ((this.ValorDocumento / 100) * this.DescontoBolsa);
        //                        }
        //                        else if (this.DescontoBolsa != null)
        //                        {
        //                            Result = Result + this.DescontoBolsa;
        //                        }

        //                        return Result;
        //                    }
        //                }
        //                else
        //                    return null;

        //            }
        //            else if (this.Status == "Q")
        //            {
        //                return this.DescontoPago;
        //            }

        //            return null;
        //        }
        //    }

        //    public decimal? TotalDesc
        //    {
        //        get
        //        {
        //            if (this.Status == "Q")
        //            {
        //                return this.ValorPago;
        //            }
        //            else if ((this.Status == "A" || this.Status == "R") && (Convert.ToDecimal(this.DataDif.Days) > 0))
        //            {
        //                return this.ValorDocumento +
        //                    (this.JurosDesc != null ? this.JurosDesc : 0) +
        //                    (this.MultaDesc != null ? this.MultaDesc : 0) -
        //                    (this.DescontoDesc != null ? this.DescontoDesc : 0);
        //            }
        //            else if (this.Status == "A" || this.Status == "R")
        //            {
        //                return this.ValorDocumento - (this.DescontoDesc != null ? this.DescontoDesc : 0);
        //            }

        //            return null;
        //        }
        //    }

        //    public string DescricaoDesc
        //    {
        //        get
        //        {
        //            switch (this.Status)
        //            {
        //                case "A":
        //                    return this.Documento + " / " + "Em Aberto";
        //                    break;
        //                case "Q":
        //                    return this.Documento + " / " + "Quitado";
        //                    break;
        //                case "C":
        //                    return this.Documento + " / " + "Cancelado";
        //                    break;
        //                case "R":
        //                    return this.Documento + " / " + "Pré-Mat";
        //                    break;
        //                default:
        //                    return null;
        //                    break;
        //            }
        //        }
        //    }

        //    public decimal? ValorDif
        //    {
        //        get
        //        {
        //            if (this.Status == "Q" || this.Status == "P")
        //                return ((this.ValorPago ?? 0) - (this.ValorDocumento + this.JurosDesc + this.MultaDesc - this.DescontoDesc)) * (-1);
        //            else
        //            {
        //                return ((this.ValorDocumento + (this.JurosDesc != null ? this.JurosDesc : 0) + (this.MultaDesc != null ? this.MultaDesc : 0)) -
        //                    (this.DescontoDesc != null ? this.DescontoDesc : 0));
                        
        //            }
        //        }
        //    }            

        //    public int DiasDesc
        //    {
        //        get
        //        {
        //            double dias = 0;
        //            if (DataPagamento != null)
        //            {
        //                if (DataPagamento > this.DataVencimento)
        //                { //atrasado
        //                    dias = this.DataPagamento.Value.Subtract(DataVencimento).TotalDays;
        //                }
        //                else if (DataPagamento.Value < this.DataVencimento)
        //                {
        //                    dias = this.DataPagamento.Value.Subtract(DataVencimento).TotalDays;
        //                }
        //                else
        //                    return 0;
        //            }
        //            else
        //            {
        //                dias = DateTime.Today.Subtract(this.DataVencimento).TotalDays;
        //            }
        //            return (int)Math.Round(dias, 0);
        //        }
        //    }

        //    public decimal? ValorDifParc
        //    {
        //        get
        //        {
        //            return (this.ValorDocumento + (this.JurosDesc ?? 0) + (this.MultaDesc ?? 0) - (this.DescontoDesc ?? 0)) - (this.ValorPago ?? 0);
        //        }
        //    }

        //    public int coAluno
        //    {
        //        set
        //        {
        //            var matricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
        //                             where tb08.CO_ALU == value
        //                             orderby tb08.DT_CAD_MAT descending 
        //                             select tb08).FirstOrDefault();
        //            if (matricula != null)
        //            {
        //                var dados = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
        //                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
        //                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
        //                             join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
        //                             where tb08.CO_ALU_CAD == matricula.CO_ALU_CAD
        //                             select new
        //                             {
        //                                 tb08,
        //                                 tb01,
        //                                 tb07,
        //                                 tb129
        //                             }).FirstOrDefault();
        //                if (dados != null)
        //                {
        //                    if (dados.tb01.TB44_MODULO == null)
        //                        dados.tb01.TB44_MODULOReference.Load();
        //                    if (dados.tb07.TB108_RESPONSAVEL == null)
        //                        dados.tb07.TB108_RESPONSAVELReference.Load();
        //                    if (dados.tb07.TB148_TIPO_BOLSA == null)
        //                        dados.tb07.TB148_TIPO_BOLSAReference.Load();
        //                    this.DescSerie = " - Mod: " + dados.tb01.TB44_MODULO.DE_MODU_CUR + " - Série: " + dados.tb01.NO_CUR +
        //                               " - Turma: " + dados.tb129.NO_TURMA + " - Convênio: " + (dados.tb07.TB148_TIPO_BOLSA != null ? dados.tb07.TB148_TIPO_BOLSA.NO_TIPO_BOLSA + " )" : "Nenhum )");

        //                    this.Nome = dados.tb07.NO_ALU;
        //                    this.NIRE = dados.tb07.NU_NIRE;
        //                    this.CpfResp = dados.tb07.TB108_RESPONSAVEL.NU_CPF_RESP;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
         #region Evento de componentes da página

        private void lblValorPago_AfterPrint(object sender, EventArgs e)
        {
        }

        //private void lblJurosDocto_AfterPrint(object sender, EventArgs e)
        //{
        //    if (lblJurosDocto.Text != "")
        //    {
        //        totValorJuros = totValorJuros + decimal.Parse(lblJurosDocto.Text);
        //        totValorJurosG = totValorJurosG + decimal.Parse(lblJurosDocto.Text);
        //    }
        //}

        //private void lblMultaDocto_AfterPrint(object sender, EventArgs e)
        //{
        //    if (lblMultaDocto.Text != "")
        //    {
        //        totValorMulta = totValorMulta + decimal.Parse(lblMultaDocto.Text);
        //        totValorMultaG = totValorMultaG + decimal.Parse(lblMultaDocto.Text);
        //    }
        //}

        //private void lblDesctoDocto_AfterPrint(object sender, EventArgs e)
        //{
        //    if (lblDesctoDocto.Text != "")
        //    {
        //        totValorDescto = totValorDescto + decimal.Parse(lblDesctoDocto.Text);
        //        totValorDesctoG = totValorDesctoG + decimal.Parse(lblDesctoDocto.Text);
        //    }
        //}

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
                    obj.ForeColor = Color.Blue;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (obj.Text.Contains('-'))
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "+ " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else
                    obj.ForeColor = Color.Black;
            }
        }

        //private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    totValorDoctoG = totValorMultaG = totValorJurosG = totValorDesctoG = 0;
        //}

        private void lblJurosDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
        }

        private void lbTotalPago_AfterPrint(object sender, EventArgs e)
        {

        }

        //private void lblValorDif_AfterPrint(object sender, EventArgs e)
        //{
        //    XRTableCell obj = (XRTableCell)sender;
        //    if (obj.Text != "")
        //    {
        //        totValorDif = totValorDif + decimal.Parse(obj.Text);
        //    }
        }

        private void lblValorTotDocto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (obj.Text.Contains('-'))
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "- " + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (!obj.Text.Contains('-'))
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

        public int InitReport(string strParametrosRelatorio, int strP_CO_EMP_REF, int intP_ID_REGIAO, int intP_ID_AREA, int strP_CO_ALU, string strP_CO_ANO_MES_MAT, int strP_CO_RESP, bool p, string strINFOS)
        {
            throw new NotImplementedException();
        }

        public int InitReport(string strParametrosRelatorio, int strP_CO_EMP_REF, int intP_ID_REGIAO, int intP_ID_AREA, int strP_CO_ALU, int strP_CO_RESP, bool p, string strINFOS)
        {
            throw new NotImplementedException();
        }
    }

    }