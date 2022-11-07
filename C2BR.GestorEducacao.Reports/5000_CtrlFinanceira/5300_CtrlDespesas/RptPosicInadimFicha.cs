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
    public partial class RptPosicInadimFicha : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptPosicInadimFicha()
        {
            InitializeComponent();
        } 

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_FORN,
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
                           && (strP_CO_FORN != 0 ? tb38.TB41_FORNEC.CO_FORN== strP_CO_FORN : strP_CO_FORN == 0)
                           && tb38.IC_SIT_DOC == "A"
                           && (strP_CO_AGRUP != 0 ? tb38.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                           select new RelPosicInadFichaFornec
                           {
                               Fornecedor = tb38.TB41_FORNEC.NO_FAN_FOR,
                               DataMov = tb38.DT_CAD_DOC,
                               Documento = tb38.NU_DOC,
                               DataVencimento = tb38.DT_VEN_DOC,
                               ValorDocumento = tb38.VR_PAR_DOC,
                               Juros = tb38.VR_JUR_DOC,
                               Multa = tb38.VR_MUL_DOC,
                               Desconto = tb38.VR_DES_DOC,
                               TpMulta = tb38.CO_FLAG_TP_VALOR_MUL,
                               TpJuros = tb38.CO_FLAG_TP_VALOR_JUR,
                               TpDesc = tb38.CO_FLAG_TP_VALOR_DES,
                               Parcela = tb38.NU_PAR
                           }).OrderBy(p => p.Fornecedor);

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
                foreach (RelPosicInadFichaFornec at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Posição de Inadimplência por Forncedor

        public class RelPosicInadFichaFornec
        {
            public string Fornecedor { get; set; }
            public DateTime DataMov { get; set; }
            public DateTime DataVencimento { get; set; }
            public string Documento { get; set; }
            public decimal ValorDocumento { get; set; }
            public decimal? Juros { get; set; }
            public decimal? Multa { get; set; }
            public decimal? Desconto { get; set; }
            public string TpMulta { get; set; }
            public string TpJuros { get; set; }
            public string TpDesc { get; set; }
            public decimal? Result { get; set; }
            public TimeSpan DataDif { get; set; }
            public int Parcela { get; set; }

            public int QtdeDias
            {
                get
                {
                    return this.DataDif.Days;
                }
            }

            public decimal? JurosDesc
            {
                get
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

                    return null;
                }
            }

            public decimal? MultaDesc
            {
                get
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
                        return null;                    
                }
            }

            public decimal? DescontoDesc
            {
                get
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
            }

            public decimal? TotalDesc
            {
                get
                {
                    if (Convert.ToDecimal(this.DataDif.Days) > 0)
                    {
                        return this.ValorDocumento +
                            (this.JurosDesc != null ? this.JurosDesc : 0) +
                            (this.MultaDesc != null ? this.MultaDesc : 0) -
                            (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }
                    else
                    {
                        return this.ValorDocumento - (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }
                }
            }
        }
        #endregion

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var obj = (XRTableCell)sender;

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
