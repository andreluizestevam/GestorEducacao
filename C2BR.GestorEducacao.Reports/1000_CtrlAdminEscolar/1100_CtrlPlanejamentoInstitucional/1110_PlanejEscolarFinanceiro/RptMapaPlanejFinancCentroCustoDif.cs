using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro
{
    public partial class RptMapaPlanejFinancCentroCustoDif : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        decimal vp1, vp2, vp3, vp4, vp5, vp6, vp7, vp8, vp9, vp10, vp11, vp12, vpTotal = 0;
        decimal vt1, vt2, vt3, vt4, vt5, vt6, vt7, vt8, vt9, vt10, vt11, vt12, vtTotal = 0;
        
        public RptMapaPlanejFinancCentroCustoDif()
        {
            InitializeComponent();
        }

        public int InitReport(string parametros,
                             int codInst,
                             int strP_CO_EMP,
                             string strP_DEPTO,
                             int strP_ANO_INI,
                             int strP_ANO_FIM,
                             string strP_TP_CONTA,
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

                int intP_DEPTO = (strP_DEPTO != "T" ? int.Parse(strP_DEPTO) : 0);

                #region Query
                var lst = from tb099 in ctx.TB099_CENTRO_CUSTO
                          from tb112 in tb099.TB112_PLANCUSTO

                          where
                           tb112.TB53_GRP_CTA.TP_GRUP_CTA == strP_TP_CONTA
                              && tb112.CO_EMP == strP_CO_EMP
                                && tb112.CO_ANO_REF >= strP_ANO_INI
                                && tb112.CO_ANO_REF <= strP_ANO_FIM
                          && (intP_DEPTO != 0 ? tb099.TB14_DEPTO.CO_DEPTO == intP_DEPTO : intP_DEPTO == 0)
                          orderby tb099.NU_CTA_CENT_CUSTO, tb112.CO_ANO_REF
                          select new RelPlanejCentroCustoDif
                          {
                              CoGrupo = tb112.TB56_PLANOCTA.TB54_SGRP_CTA.CO_GRUP_CTA,
                              CoSubGrupo = tb112.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA,
                              SubGrupo = tb112.TB56_PLANOCTA.TB54_SGRP_CTA.DE_SGRUP_CTA,
                              CoConta = tb112.TB56_PLANOCTA.CO_CONTA_PC,
                              PlanoConta = tb112.TB56_PLANOCTA.DE_CONTA_PC,
                              ValorPlan1 = tb112.VL_PLAN_MES1,
                              ValorPlan2 = tb112.VL_PLAN_MES2,
                              ValorPlan3 = tb112.VL_PLAN_MES3,
                              ValorPlan4 = tb112.VL_PLAN_MES4,
                              ValorPlan5 = tb112.VL_PLAN_MES5,
                              ValorPlan6 = tb112.VL_PLAN_MES6,
                              ValorPlan7 = tb112.VL_PLAN_MES7,
                              ValorPlan8 = tb112.VL_PLAN_MES8,
                              ValorPlan9 = tb112.VL_PLAN_MES9,
                              ValorPlan10 = tb112.VL_PLAN_MES10,
                              ValorPlan11 = tb112.VL_PLAN_MES11,
                              ValorPlan12 = tb112.VL_PLAN_MES12,
                              ValorTotalPlane = (tb112.VL_PLAN_MES1 ?? 0) + (tb112.VL_PLAN_MES2 ?? 0) + (tb112.VL_PLAN_MES3 ?? 0) + (tb112.VL_PLAN_MES4 ?? 0) +
                              (tb112.VL_PLAN_MES5 ?? 0) + (tb112.VL_PLAN_MES6 ?? 0) + (tb112.VL_PLAN_MES7 ?? 0) + (tb112.VL_PLAN_MES8 ?? 0) + (tb112.VL_PLAN_MES9 ?? 0) +
                              (tb112.VL_PLAN_MES10 ?? 0) + (tb112.VL_PLAN_MES11 ?? 0) + (tb112.VL_PLAN_MES12 ?? 0),
                              ValorReali1 = tb112.VL_REAL_MES_1,
                              ValorReali2 = tb112.VL_REAL_MES_2,
                              ValorReali3 = tb112.VL_REAL_MES_3,
                              ValorReali4 = tb112.VL_REAL_MES_4,
                              ValorReali5 = tb112.VL_REAL_MES_5,
                              ValorReali6 = tb112.VL_REAL_MES_6,
                              ValorReali7 = tb112.VL_REAL_MES_7,
                              ValorReali8 = tb112.VL_REAL_MES_8,
                              ValorReali9 = tb112.VL_REAL_MES_9,
                              ValorReali10 = tb112.VL_REAL_MES_10,
                              ValorReali11 = tb112.VL_REAL_MES_11,
                              ValorReali12 = tb112.VL_REAL_MES_12,
                              ValorTotalReali = (tb112.VL_REAL_MES_1 ?? 0) + (tb112.VL_REAL_MES_2 ?? 0) + (tb112.VL_REAL_MES_3 ?? 0) + (tb112.VL_REAL_MES_4 ?? 0) +
                              (tb112.VL_REAL_MES_5 ?? 0) + (tb112.VL_REAL_MES_6 ?? 0) + (tb112.VL_REAL_MES_7 ?? 0) + (tb112.VL_REAL_MES_8 ?? 0) + (tb112.VL_REAL_MES_9 ?? 0) +
                              (tb112.VL_REAL_MES_10 ?? 0) + (tb112.VL_REAL_MES_11 ?? 0) + (tb112.VL_REAL_MES_12 ?? 0)
                          };
                #endregion

                var res = lst.ToList();
                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta a lista no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPlanejCentroCustoDif at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }


        public class RelPlanejCentroCustoDif
        {
            public int CoGrupo { get; set; }
            public int CoSubGrupo { get; set; }
            public string SubGrupo { get; set; }
            public int CoConta { get; set; }
            public string PlanoConta { get; set; }
            public decimal? ValorPlan1 { get; set; }
            public decimal? ValorPlan2 { get; set; }
            public decimal? ValorPlan3 { get; set; }
            public decimal? ValorPlan4 { get; set; }
            public decimal? ValorPlan5 { get; set; }
            public decimal? ValorPlan6 { get; set; }
            public decimal? ValorPlan7 { get; set; }
            public decimal? ValorPlan8 { get; set; }
            public decimal? ValorPlan9 { get; set; }
            public decimal? ValorPlan10 { get; set; }
            public decimal? ValorPlan11 { get; set; }
            public decimal? ValorPlan12 { get; set; }
            public decimal? ValorReali1 { get; set; }
            public decimal? ValorReali2 { get; set; }
            public decimal? ValorReali3 { get; set; }
            public decimal? ValorReali4 { get; set; }
            public decimal? ValorReali5 { get; set; }
            public decimal? ValorReali6 { get; set; }
            public decimal? ValorReali7 { get; set; }
            public decimal? ValorReali8 { get; set; }
            public decimal? ValorReali9 { get; set; }
            public decimal? ValorReali10 { get; set; }
            public decimal? ValorReali11 { get; set; }
            public decimal? ValorReali12 { get; set; }
            public decimal? ValorTotalReali { get; set; }
            public decimal? ValorTotalPlane { get; set; }

            public string SubGrupoDesc
            {
                get
                {
                    return this.CoGrupo.ToString() + "." + this.CoSubGrupo.ToString().PadLeft(2, '0') + " - " + this.SubGrupo;
                }
            }

            public string ContaDesc
            {
                get
                {
                    return this.CoGrupo.ToString() + "." + this.CoSubGrupo.ToString().PadLeft(2, '0') + "." + this.CoConta.ToString().PadLeft(3, '0') + " - " + this.PlanoConta;
                }
            }
        }

        private void DetailContent_AfterPrint(object sender, EventArgs e)
        {
            vp1 = lblVP1.Text != "" ? vp1 + decimal.Parse(lblVP1.Text.Replace("+", "").Replace(" ", "")) : vp1;
            vp2 = lblVP2.Text != "" ? vp2 + decimal.Parse(lblVP2.Text.Replace("+", "").Replace(" ", "")) : vp2;
            vp3 = lblVP3.Text != "" ? vp3 + decimal.Parse(lblVP3.Text.Replace("+", "").Replace(" ", "")) : vp3;
            vp4 = lblVP4.Text != "" ? vp4 + decimal.Parse(lblVP4.Text.Replace("+", "").Replace(" ", "")) : vp4;
            vp5 = lblVP5.Text != "" ? vp5 + decimal.Parse(lblVP5.Text.Replace("+", "").Replace(" ", "")) : vp5;
            vp6 = lblVP6.Text != "" ? vp6 + decimal.Parse(lblVP6.Text.Replace("+", "").Replace(" ", "")) : vp6;
            vp7 = lblVP7.Text != "" ? vp7 + decimal.Parse(lblVP7.Text.Replace("+", "").Replace(" ", "")) : vp7;
            vp8 = lblVP8.Text != "" ? vp8 + decimal.Parse(lblVP8.Text.Replace("+", "").Replace(" ", "")) : vp8;
            vp9 = lblVP9.Text != "" ? vp9 + decimal.Parse(lblVP9.Text.Replace("+", "").Replace(" ", "")) : vp9;
            vp10 = lblVP10.Text != "" ? vp10 + decimal.Parse(lblVP10.Text.Replace("+", "").Replace(" ", "")) : vp10;
            vp11 = lblVP11.Text != "" ? vp11 + decimal.Parse(lblVP11.Text.Replace("+", "").Replace(" ", "")) : vp11;
            vp12 = lblVP12.Text != "" ? vp12 + decimal.Parse(lblVP12.Text.Replace("+", "").Replace(" ", "")) : vp12;
            vpTotal = lblVFTotal.Text != "" ? vpTotal + decimal.Parse(lblVFTotal.Text.Replace("+", "").Replace(" ", "")) : vpTotal;
        
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
             lblVt1.Text = String.Format("{0:#,##0.00}", vt1);
            if (vt1 < 0)
            {
                lblVt1.Text = lblVt1.Text;
                lblVt1.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt1 > 0)
            {
                lblVt1.Text = "+" + lblVt1.Text.Replace("-", "");
                lblVt1.ForeColor = System.Drawing.Color.Red;
            }

            lblVt2.Text = String.Format("{0:#,##0.00}", vt2);
            if (vt2 < 0)
            {
                lblVt2.Text = lblVt2.Text;
                lblVt2.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt2 > 0)
            {
                lblVt2.Text = "+" + lblVt2.Text.Replace("-", "");
                lblVt2.ForeColor = System.Drawing.Color.Red;
            }

            lblVt3.Text = String.Format("{0:#,##0.00}", vt3);
            if (vt3 < 0)
            {
                lblVt3.Text = lblVt3.Text;
                lblVt3.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt3 > 0)
            {
                lblVt3.Text = "+" + lblVt3.Text.Replace("-", "");
                lblVt3.ForeColor = System.Drawing.Color.Red;
            }

            lblVt4.Text = String.Format("{0:#,##0.00}", vt4);
            if (vt4 < 0)
            {
                lblVt4.Text = lblVt4.Text;
                lblVt4.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt4 > 0)
            {
                lblVt4.Text = "+" + lblVt4.Text.Replace("-", "");
                lblVt4.ForeColor = System.Drawing.Color.Red;
            }

            lblVt5.Text = String.Format("{0:#,##0.00}", vt5);
            if (vt5 < 0)
            {
                lblVt5.Text = lblVt5.Text;
                lblVt5.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt5 > 0)
            {
                lblVt5.Text = "+" + lblVt5.Text.Replace("-", "");
                lblVt5.ForeColor = System.Drawing.Color.Red;
            }

            lblVt6.Text = String.Format("{0:#,##0.00}", vt6);
            if (vt6 < 0)
            {
                lblVt6.Text = lblVt6.Text;
                lblVt6.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt6 > 0)
            {
                lblVt6.Text = "+" + lblVt6.Text.Replace("-", "");
                lblVt6.ForeColor = System.Drawing.Color.Red;
            }

            lblVt7.Text = String.Format("{0:#,##0.00}", vt7);
            if (vt7 < 0)
            {
                lblVt7.Text = lblVt7.Text;
                lblVt7.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt7 > 0)
            {
                lblVt7.Text = "+" + lblVt7.Text.Replace("-", "");
                lblVt7.ForeColor = System.Drawing.Color.Red;
            }

            lblVt8.Text = String.Format("{0:#,##0.00}", vt8);
            if (vt8 < 0)
            {
                lblVt8.Text = lblVt8.Text;
                lblVt8.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt8 > 0)
            {
                lblVt8.Text = "+" + lblVt8.Text.Replace("-", "");
                lblVt8.ForeColor = System.Drawing.Color.Red;
            }

            lblVt9.Text = String.Format("{0:#,##0.00}", vt9);
            if (vt9 < 0)
            {
                lblVt9.Text = lblVt9.Text;
                lblVt9.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt9 > 0)
            {
                lblVt9.Text = "+" + lblVt9.Text.Replace("-", "");
                lblVt9.ForeColor = System.Drawing.Color.Red;
            }

            lblVt10.Text = String.Format("{0:#,##0.00}", vt10);
            if (vt10 < 0)
            {
                lblVt10.Text = lblVt10.Text;
                lblVt10.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt10 > 0)
            {
                lblVt10.Text = "+" + lblVt10.Text.Replace("-", "");
                lblVt10.ForeColor = System.Drawing.Color.Red;
            }

            lblVt11.Text = String.Format("{0:#,##0.00}", vt11);
            if (vt11 < 0)
            {
                lblVt11.Text = lblVt11.Text;
                lblVt11.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt11 > 0)
            {
                lblVt11.Text = "+" + lblVt11.Text.Replace("-", "");
                lblVt11.ForeColor = System.Drawing.Color.Red;
            }

            lblVt12.Text = String.Format("{0:#,##0.00}", vt12);
            if (vt12 < 0)
            {
                lblVt12.Text = lblVt12.Text;
                lblVt12.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vt12 > 0)
            {
                lblVt12.Text = "+" + lblVt12.Text.Replace("-", "");
                lblVt12.ForeColor = System.Drawing.Color.Red;
            }

            lblVtTotal.Text = String.Format("{0:#,##0.00}", vtTotal);
            if (vtTotal < 0)
            {
                lblVtTotal.Text = lblVtTotal.Text;
                lblVtTotal.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vtTotal > 0)
            {
                lblVtTotal.Text = "+" + lblVtTotal.Text.Replace("-", "");
                lblVtTotal.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTot1.Text = String.Format("{0:#,##0.00}", vp1);
            if (vp1 < 0)
            {
                lblTot1.Text = lblTot1.Text;
                lblTot1.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp1 > 0)
            {
                lblTot1.Text = "+" + lblTot1.Text.Replace("-", "");
                lblTot1.ForeColor = System.Drawing.Color.Red;
            }

            lblTot2.Text = String.Format("{0:#,##0.00}", vp2);
            if (vp2 < 0)
            {
                lblTot2.Text = lblTot2.Text;
                lblTot2.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp2 > 0)
            {
                lblTot2.Text = "+" + lblTot2.Text.Replace("-", "");
                lblTot2.ForeColor = System.Drawing.Color.Red;
            }

            lblTot3.Text = String.Format("{0:#,##0.00}", vp3);
            if (vp3 < 0)
            {
                lblTot3.Text = lblTot3.Text;
                lblTot3.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp3 > 0)
            {
                lblTot3.Text = "+" + lblTot3.Text.Replace("-", "");
                lblTot3.ForeColor = System.Drawing.Color.Red;
            }

            lblTot4.Text = String.Format("{0:#,##0.00}", vp4);
            if (vp4 < 0)
            {
                lblTot4.Text = lblTot4.Text;
                lblTot4.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp4 > 0)
            {
                lblTot4.Text = "+" + lblTot4.Text.Replace("-", "");
                lblTot4.ForeColor = System.Drawing.Color.Red;
            }

            lblTot5.Text = String.Format("{0:#,##0.00}", vp5);
            if (vp5 < 0)
            {
                lblTot5.Text = lblTot5.Text;
                lblTot5.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp5 > 0)
            {
                lblTot5.Text = "+" + lblTot5.Text.Replace("-", "");
                lblTot5.ForeColor = System.Drawing.Color.Red;
            }

            lblTot6.Text = String.Format("{0:#,##0.00}", vp6);
            if (vp6 < 0)
            {
                lblTot6.Text = lblTot6.Text;
                lblTot6.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp6 > 0)
            {
                lblTot6.Text = "+" + lblTot6.Text.Replace("-", "");
                lblTot6.ForeColor = System.Drawing.Color.Red;
            }

            lblTot7.Text = String.Format("{0:#,##0.00}", vp7);
            if (vp7 < 0)
            {
                lblTot7.Text = lblTot7.Text;
                lblTot7.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp7 > 0)
            {
                lblTot7.Text = "+" + lblTot7.Text.Replace("-", "");
                lblTot7.ForeColor = System.Drawing.Color.Red;
            }

            lblTot8.Text = String.Format("{0:#,##0.00}", vp8);
            if (vp8 < 0)
            {
                lblTot8.Text = lblTot8.Text;
                lblTot8.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp8 > 0)
            {
                lblTot8.Text = "+" + lblTot8.Text.Replace("-", "");
                lblTot8.ForeColor = System.Drawing.Color.Red;
            }

            lblTot9.Text = String.Format("{0:#,##0.00}", vp9);
            if (vp9 < 0)
            {
                lblTot9.Text = lblTot9.Text;
                lblTot9.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp9 > 0)
            {
                lblTot9.Text = "+" + lblTot9.Text.Replace("-", "");
                lblTot9.ForeColor = System.Drawing.Color.Red;
            }

            lblTot10.Text = String.Format("{0:#,##0.00}", vp10);
            if (vp10 < 0)
            {
                lblTot10.Text = lblTot10.Text;
                lblTot10.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp10 > 0)
            {
                lblTot10.Text = "+" + lblTot10.Text.Replace("-", "");
                lblTot10.ForeColor = System.Drawing.Color.Red;
            }

            lblTot11.Text = String.Format("{0:#,##0.00}", vp11);
            if (vp11 < 0)
            {
                lblTot11.Text = lblTot11.Text;
                lblTot11.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp11 > 0)
            {
                lblTot11.Text = "+" + lblTot11.Text.Replace("-", "");
                lblTot11.ForeColor = System.Drawing.Color.Red;
            }

            lblTot12.Text = String.Format("{0:#,##0.00}", vp12);
            if (vp12 < 0)
            {
                lblTot12.Text = lblTot12.Text;
                lblTot12.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp12 > 0)
            {
                lblTot12.Text = "+" + lblTot12.Text.Replace("-", "");
                lblTot12.ForeColor = System.Drawing.Color.Red;
            }

            lblVPTotal.Text = String.Format("{0:#,##0.00}", vpTotal);
            if (vpTotal < 0)
            {
                lblVPTotal.Text = lblVPTotal.Text;
                lblVPTotal.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vpTotal > 0)
            {
                lblVPTotal.Text = "+" + lblVPTotal.Text.Replace("-", "");
                lblVPTotal.ForeColor = System.Drawing.Color.Red;
            }

            vt1 = vt1 + vp1;
            vt2 = vt2 + vp2;
            vt3 = vt3 + vp3;
            vt4 = vt4 + vp4;
            vt5 = vt5 + vp5;
            vt6 = vt6 + vp6;
            vt7 = vt7 + vp7;
            vt8 = vt8 + vp8;
            vt9 = vt9 + vp9;
            vt10 = vt10 + vp10;
            vt11 = vt11 + vp11;
            vt12 = vt12 + vp12;
            vtTotal = vtTotal + vpTotal;

            vp1 = vp2 = vp3 = vp4 = vp5 = vp6 = vp7 = vp8 = vp9 = vp10 = vp11 = vp12 = vpTotal = 0;
        }       

    }
}
