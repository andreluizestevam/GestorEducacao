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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5700_CtrlDesepenhoFinanceiro
{
    public partial class RptCurvaABCDespesaComprom : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totGeralValorTitul = 0;
        private int numGeralTitul = 0;
        private int numSeq = 1;

        public RptCurvaABCDespesaComprom()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int coEmpRef,
                               string situDoc,
                               DateTime dtInicio,
                               DateTime dtFim,
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
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Titulos/Fornecedor

                var lst = situDoc == "A" ? from tb38 in ctx.TB38_CTA_PAGAR
                                           where tb38.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                                            && tb38.DT_VEN_DOC >= dtInicio && tb38.DT_VEN_DOC <= dtFim
                                            && tb38.IC_SIT_DOC == situDoc
                                            && (strP_CO_AGRUP != 0 ? tb38.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                                           group tb38 by tb38.TB41_FORNEC.NO_FAN_FOR into g
                                           orderby g.Key
                                           select new TitulosFornecedor { Fornecedor = g.Key, NumeroTitulos = g.Count(), TotalValorTitulo = g.Sum(p => p.VR_PAR_DOC) } :
                          from tb38 in ctx.TB38_CTA_PAGAR
                          where tb38.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && tb38.DT_VEN_DOC >= dtInicio && tb38.DT_VEN_DOC <= dtFim
                           && tb38.IC_SIT_DOC == situDoc
                          group tb38 by tb38.TB41_FORNEC.NO_FAN_FOR into g
                          orderby g.Key
                          select new TitulosFornecedor { Fornecedor = g.Key, NumeroTitulos = g.Count(), TotalValorTitulo = g.Sum(p => p.VR_PAG ?? 0) };

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta a lista no DataSource do Relatorio
                bsReport.Clear();
                foreach (TitulosFornecedor at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Titulos/Fornecedor do Relatorio

        public class TitulosFornecedor
        {
            public string Fornecedor { get; set; }
            public int CodigoForne { get; set; }
            public int NumeroTitulos { get; set; }
            public decimal TotalValorTitulo { get; set; }            
        }
        #endregion

        private void lblNumSeq_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblNumSeq.Text = numSeq.ToString();

            numSeq++;
        }

        private void lblDeficiencia_AfterPrint(object sender, EventArgs e)
        {
            numGeralTitul = numGeralTitul + int.Parse(lblDeficiencia.Text);
        }

        private void lblValorTotalTit_AfterPrint(object sender, EventArgs e)
        {
            totGeralValorTitul = totGeralValorTitul + decimal.Parse(lblValorTotalTit.Text);
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotTitulos.Text = numGeralTitul.ToString().PadLeft(3, '0');
            lblTotValorTitulos.Text = String.Format("{0:#,##0.00}",totGeralValorTitul);
        }
    }
}
