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
    public partial class RptPosicInadimPorRespon : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptPosicInadimPorRespon()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_EMP_UNID_CONT,
                              int strP_CO_AGRUP,
                              string infos,
                              DateTime strDt_Ini,
                              DateTime strDt_Fim)
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

                var lsRel = (from tb47 in ctx.TB47_CTA_RECEB
                             join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                             where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                             && tb47.CO_EMP == strP_CO_EMP_REF
                             && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "P" || tb47.IC_SIT_DOC == "R")
                             && tb47.DT_VEN_DOC < DateTime.Now
                             && (tb47.DT_VEN_DOC > strDt_Ini && tb47.DT_VEN_DOC < strDt_Fim)
                             && (strP_CO_AGRUP != -1 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : 0 == 0)
                             && (strP_CO_EMP_UNID_CONT != -1 ? tb47.CO_EMP_UNID_CONT == strP_CO_EMP_UNID_CONT : 0 == 0)
                             select new PosicaoInadResponsavel
                             {
                                 Responsavel = tb47.TB108_RESPONSAVEL.NO_RESP,
                                 CPFResponsavel = tb47.TB108_RESPONSAVEL.NU_CPF_RESP,
                                 TelefoneCelular = tb47.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                                 TelefoneResid = tb47.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                                 Aluno = tb07.CO_ALU,
                                 VrTitulo = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? tb47.VR_PAR_DOC : tb47.VR_PAR_DOC - (tb47.VR_PAG ?? 0),
                                 Multa = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? (tb47.CO_FLAG_TP_VALOR_MUL == "V" ? (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC.Value : 0) : (tb47.VR_PAR_DOC * tb47.VR_MUL_DOC.Value) / 100) : (tb47.VR_PAR_DOC * tb47.VR_MUL_DOC.Value) / 100,
                                 Juros = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? (tb47.CO_FLAG_TP_VALOR_JUR == "V" ? (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC.Value : 0) : (tb47.VR_PAR_DOC * tb47.VR_JUR_DOC.Value) / 100) : (tb47.VR_PAR_DOC * tb47.VR_JUR_DOC.Value) / 100,
                                 Desconto = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC.Value : 0) : (tb47.VR_PAR_DOC * tb47.VR_DES_DOC.Value) / 100) : (tb47.VR_PAR_DOC * tb47.VR_DES_DOC.Value) / 100,
                                 VrPago = tb47.IC_SIT_DOC == "P" ? tb47.VR_PAG.Value : 0,
                                 DataVencimento = tb47.DT_VEN_DOC
                             });

                var lst = (from obj in lsRel
                           group obj by new
                           {
                               obj.CPFResponsavel,
                               obj.Responsavel,
                               obj.TelefoneCelular,
                               obj.TelefoneResid

                           } into g
                           select new RelPosicInadPorRespon
                           {
                               CPFResponsavel = g.Key.CPFResponsavel,
                               Responsavel = g.Key.Responsavel,
                               TelefoneCelular = g.Key.TelefoneCelular,
                               TelefoneResid = g.Key.TelefoneResid,
                               QtdeAlunos = g.Select(p => p.Aluno).Distinct().Count(),
                               QtdeTitulos = g.Count(),

                               TotalValorTitulo = g.Sum(p => p.VrTitulo),
                               Multa = g.Sum(p => p.Multa),
                               Juros = g.Sum(p => p.Juros),
                               Desconto = g.Sum(p => p.Desconto),
                               VrPago = g.Sum(p => p.VrPago),
                               DataVencimento = g.Select(p => p.DataVencimento).Min()
                           }).OrderBy(p => p.Responsavel);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPosicInadPorRespon at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Posição de Inadimplência por Responsável

        public class PosicaoInadResponsavel
        {
            public string CPFResponsavel { get; set; }
            public string Responsavel { get; set; }
            public int Aluno { get; set; }
            public string TelefoneCelular { get; set; }
            public string TelefoneResid { get; set; }
            public DateTime DataVencimento { get; set; }
            public decimal VrTitulo { get; set; }
            public decimal Multa { get; set; }
            public decimal Juros { get; set; }
            public decimal Desconto { get; set; }
            public decimal VrTotal
            {
                get
                {
                    return (VrTitulo + Multa + Juros) - Desconto;
                }
            }
           
            public decimal VrPago { get; set; }
            public decimal VrSaldo
            {
                get
                {
                    return VrTotal - VrSaldo;
                }
            }
        }


        public class RelPosicInadPorRespon
        {

            public string CPFResponsavel { get; set; }
            public string Responsavel { get; set; }
            public string TelefoneCelular { get; set; }
            public string TelefoneResid { get; set; }
            public DateTime DataVencimento { get; set; }
            public string Documento { get; set; }
            public decimal? Juros { get; set; }
            public decimal? Multa { get; set; }
            public decimal? Desconto { get; set; }
            public decimal? VrPago { get; set; }
            public decimal? Result { get; set; }


            public decimal TotalValorTitulo { get; set; }
            public int QtdeTitulos { get; set; }
            public int QtdeAlunos { get; set; }


            public string NomeTruncado
            {
                get {
                    if (Responsavel.Length >= 20)
                    {
                        return Responsavel.Substring(0, 20) + "...";
                    }
                    else
                    {
                        return Responsavel;
                    }
                
                }
            }


            public int QtdeDias
            {
                get
                {
                    TimeSpan DataDif = DateTime.Now.Subtract(this.DataVencimento);
                    return DataDif.Days;
                }
            }

            public decimal VrValor { get { return Juros.HasValue ? (Juros.Value * QtdeDias) : 0; } }

            public string CPFDesc
            {
                get
                {
                    return Funcoes.Format(this.CPFResponsavel, TipoFormat.CPF);
                }
            }

            public string CelularDesc
            {
                get
                {
                    return this.TelefoneCelular != "" ? Funcoes.Format(this.TelefoneCelular, TipoFormat.Telefone) : "";
                }
            }

            public string TelResidDesc
            {
                get
                {
                    return this.TelefoneResid != "" ? Funcoes.Format(this.TelefoneResid, TipoFormat.Telefone) : "";
                }
            }

            public decimal? TotalDesc
            {
                get
                {
                    return (this.TotalValorTitulo + this.VrValor +
                        (this.Multa.HasValue ? this.Multa.Value : 0)) - (this.Desconto.HasValue ? this.Desconto.Value : 0);
                }
            }

            public decimal? Saldo
            {
                get
                {
                    return TotalDesc - (VrPago.HasValue ? VrPago.Value : 0);
                }
            }
        }
        #endregion

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblTotParcDocto.Text = String.Format("{0:#,##0.00}", totParcValorDocto);
            //lblTotParcMulta.Text = String.Format("{0:#,##0.00}", totParcValorMulta);
            //lblTotParcJuros.Text = String.Format("{0:#,##0.00}", totParcValorJuros);
            //lblTotParcDescto.Text = String.Format("{0:#,##0.00}", totParcValorDescto);
            //lblTotParcTot.Text = String.Format("{0:#,##0.00}", totParcValorTotal);

            //lblDescDescTotal.Text = "(Qtde = Resp.: " + this.qtdeTotalRespo.ToString() + " - Alunos: " + this.qtdeTotalAluno.ToString() + " - Títulos: "
            //    + this.qtdeTotalTitulo.ToString() + ") (Títulos = Valor Médio: " + String.Format("{0:#,##0.00}", totParcValorDocto / this.qtdeTotalTitulo) + ")" +
            //    " (% Inadim.: " + String.Format("{0:#,##0.00}", (totParcValorDocto * 100) / this.valorSumTitulos) + "%)";
        }

        private void lblDeficiencia_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (int.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "-" + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (int.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "+" + obj.Text.Replace("+", "").Replace("-", "");
                }
                else { obj.Text = ""; }
            }
        }
    }
}
