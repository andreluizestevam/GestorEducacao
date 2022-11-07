using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro
{
    public partial class RptMapaPlanejFinancContaContabil : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        decimal vp1, vp2, vp3, vp4, vp5, vp6, vp7, vp8, vp9, vp10, vp11, vp12, vpTotal = 0;
        decimal vr1, vr2, vr3, vr4, vr5, vr6, vr7, vr8, vr9, vr10, vr11, vr12, vrTotal = 0;

        public RptMapaPlanejFinancContaContabil()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
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

                #region Query

                var lst = (from tb111 in ctx.TB111_PLANEJ_FINAN
                           join tb55 in ctx.TB055_SGRP2_CTA on tb111.TB56_PLANOCTA.TB055_SGRP2_CTA.CO_SGRUP2_CTA equals tb55.CO_SGRUP2_CTA into resultado1
                           from tb55 in resultado1.DefaultIfEmpty()
                           where tb111.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && tb111.TB25_EMPRESA.CO_EMP == strP_CO_EMP_REF
                           && tb111.TB56_PLANOCTA.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == strP_TP_CONTA
                           && tb111.CO_ANO_REF >= strP_ANO_INI
                           && tb111.CO_ANO_REF <= strP_ANO_FIM
                           select new RelPlanejConta
                           {
                               CoGrupo = tb111.TB56_PLANOCTA.TB54_SGRP_CTA.CO_GRUP_CTA,
                               CoSubGrupo = tb111.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA,
                               SubGrupo = tb111.TB56_PLANOCTA.TB54_SGRP_CTA.DE_SGRUP_CTA,
                               CoSubGrupo2 = (tb111.TB56_PLANOCTA.TB055_SGRP2_CTA != null ? tb111.TB56_PLANOCTA.TB055_SGRP2_CTA.CO_SGRUP2_CTA : 0),
                               SubGrupo2 = (tb55 != null ? tb55.DE_SGRUP2_CTA : ""),
                               CoConta = tb111.TB56_PLANOCTA.CO_CONTA_PC,
                               PlanoConta = tb111.TB56_PLANOCTA.DE_CONTA_PC,
                               ValorPlan1 = tb111.VL_PLAN_MES1, ValorPlan2 = tb111.VL_PLAN_MES2, ValorPlan3 = tb111.VL_PLAN_MES3,
                               ValorPlan4 = tb111.VL_PLAN_MES4, ValorPlan5 = tb111.VL_PLAN_MES5, ValorPlan6 = tb111.VL_PLAN_MES6,
                               ValorPlan7 = tb111.VL_PLAN_MES7, ValorPlan8 = tb111.VL_PLAN_MES8, ValorPlan9 = tb111.VL_PLAN_MES9,
                               ValorPlan10 = tb111.VL_PLAN_MES10, ValorPlan11 = tb111.VL_PLAN_MES11, ValorPlan12 = tb111.VL_PLAN_MES12,
                               ValorTotalPlane = (tb111.VL_PLAN_MES1 ?? 0) + (tb111.VL_PLAN_MES2 ?? 0) + (tb111.VL_PLAN_MES3 ?? 0) + (tb111.VL_PLAN_MES4 ?? 0) +
                               (tb111.VL_PLAN_MES5 ?? 0) + (tb111.VL_PLAN_MES6 ?? 0) + (tb111.VL_PLAN_MES7 ?? 0) + (tb111.VL_PLAN_MES8 ?? 0) + (tb111.VL_PLAN_MES9 ?? 0) +
                               (tb111.VL_PLAN_MES10 ?? 0) + (tb111.VL_PLAN_MES11 ?? 0) + (tb111.VL_PLAN_MES12 ?? 0),
                               ValorReali1 = tb111.VL_REAL_MES_1, ValorReali2 = tb111.VL_REAL_MES_2, ValorReali3 = tb111.VL_REAL_MES_3,
                               ValorReali4 = tb111.VL_REAL_MES_4, ValorReali5 = tb111.VL_REAL_MES_5, ValorReali6 = tb111.VL_REAL_MES_6,
                               ValorReali7 = tb111.VL_REAL_MES_7, ValorReali8 = tb111.VL_REAL_MES_8, ValorReali9 = tb111.VL_REAL_MES_9,
                               ValorReali10 = tb111.VL_REAL_MES_10, ValorReali11 = tb111.VL_REAL_MES_11, ValorReali12 = tb111.VL_REAL_MES_12,
                               ValorTotalReali = (tb111.VL_REAL_MES_1 ?? 0) + (tb111.VL_REAL_MES_2 ?? 0) + (tb111.VL_REAL_MES_3 ?? 0) + (tb111.VL_REAL_MES_4 ?? 0) +
                               (tb111.VL_REAL_MES_5 ?? 0) + (tb111.VL_REAL_MES_6 ?? 0) + (tb111.VL_REAL_MES_7 ?? 0) + (tb111.VL_REAL_MES_8 ?? 0) + (tb111.VL_REAL_MES_9 ?? 0) +
                               (tb111.VL_REAL_MES_10 ?? 0) + (tb111.VL_REAL_MES_11 ?? 0) + (tb111.VL_REAL_MES_12 ?? 0)
                           }).OrderBy(p => p.SubGrupo).ThenBy(p => p.SubGrupo2).ThenBy(p => p.PlanoConta);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta a lista no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPlanejConta at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        private void DetailContent_AfterPrint(object sender, System.EventArgs e)
        {

            vp1 = lblVP1.Text != "" ? vp1 + decimal.Parse(lblVP1.Text) : vp1;
            vp2 = lblVP2.Text != "" ? vp2 + decimal.Parse(lblVP2.Text) : vp2;
            vp3 = lblVP3.Text != "" ? vp3 + decimal.Parse(lblVP3.Text) : vp3;
            vp4 = lblVP4.Text != "" ? vp4 + decimal.Parse(lblVP4.Text) : vp4;
            vp5 = lblVP5.Text != "" ? vp5 + decimal.Parse(lblVP5.Text) : vp5;
            vp6 = lblVP6.Text != "" ? vp6 + decimal.Parse(lblVP6.Text) : vp6;
            vp7 = lblVP7.Text != "" ? vp7 + decimal.Parse(lblVP7.Text) : vp7;
            vp8 = lblVP8.Text != "" ? vp8 + decimal.Parse(lblVP8.Text) : vp8;
            vp9 = lblVP9.Text != "" ? vp9 + decimal.Parse(lblVP9.Text) : vp9;
            vp10 = lblVP10.Text != "" ? vp10 + decimal.Parse(lblVP10.Text) : vp10;
            vp11 = lblVP11.Text != "" ? vp11 + decimal.Parse(lblVP11.Text) : vp11;
            vp12 = lblVP12.Text != "" ? vp12 + decimal.Parse(lblVP11.Text) : vp12;
            vpTotal = lblVPTotal.Text != "" ? vpTotal + decimal.Parse(lblVPTotal.Text) : vpTotal;

            vr1 = lblVR1.Text != "" ? vr1 + decimal.Parse(lblVR1.Text) : vr1;
            vr2 = lblVR2.Text != "" ? vr2 + decimal.Parse(lblVR2.Text) : vr2;
            vr3 = lblVR3.Text != "" ? vr3 + decimal.Parse(lblVR3.Text) : vr3;
            vr4 = lblVR4.Text != "" ? vr4 + decimal.Parse(lblVR4.Text) : vr4;
            vr5 = lblVR5.Text != "" ? vr5 + decimal.Parse(lblVR5.Text) : vr5;
            vr6 = lblVR6.Text != "" ? vr6 + decimal.Parse(lblVR6.Text) : vr6;
            vr7 = lblVR7.Text != "" ? vr7 + decimal.Parse(lblVR7.Text) : vr7;
            vr8 = lblVR8.Text != "" ? vr8 + decimal.Parse(lblVR8.Text) : vr8;
            vr9 = lblVR9.Text != "" ? vr9 + decimal.Parse(lblVR9.Text) : vr9;
            vr10 = lblVR10.Text != "" ? vr10 + decimal.Parse(lblVR10.Text) : vr10;
            vr11 = lblVR11.Text != "" ? vr11 + decimal.Parse(lblVR11.Text) : vr11;
            vr12 = lblVR12.Text != "" ? vr12 + decimal.Parse(lblVR11.Text) : vr12;
            vrTotal = lblVRTotal.Text != "" ? vrTotal + decimal.Parse(lblVRTotal.Text) : vrTotal;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTot1.Text = String.Format("{0:#,##0.00}", vp1 - vr1);
            if (vp1 - vr1 > 0)
            {
                lblTot1.Text = "-" + lblTot1.Text;
                lblTot1.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp1 - vr1 < 0)
            {
                lblTot1.Text = "+" + lblTot1.Text.Replace("-", "");
                lblTot1.ForeColor = System.Drawing.Color.Red;
            }

            lblTot2.Text = String.Format("{0:#,##0.00}", vp2 - vr2);
            if (vp2 - vr2 > 0)
            {
                lblTot2.Text = "-" + lblTot2.Text;
                lblTot2.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp2 - vr2 < 0)
            {
                lblTot2.Text = "+" + lblTot2.Text.Replace("-", "");
                lblTot2.ForeColor = System.Drawing.Color.Red;
            }

            lblTot3.Text = String.Format("{0:#,##0.00}", vp3 - vr3);
            if (vp3 - vr3 > 0)
            {
                lblTot3.Text = "-" + lblTot3.Text;
                lblTot3.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp3 - vr3 < 0)
            {
                lblTot3.Text = "+" + lblTot3.Text.Replace("-", "");
                lblTot3.ForeColor = System.Drawing.Color.Red;
            }

            lblTot4.Text = String.Format("{0:#,##0.00}", vp4 - vr4);
            if (vp4 - vr4 > 0)
            {
                lblTot4.Text = "-" + lblTot4.Text;
                lblTot4.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp4 - vr4 < 0)
            {
                lblTot4.Text = "+" + lblTot4.Text.Replace("-", "");
                lblTot4.ForeColor = System.Drawing.Color.Red;
            }

            lblTot5.Text = String.Format("{0:#,##0.00}", vp5 - vr5);
            if (vp5 - vr5 > 0)
            {
                lblTot5.Text = "-" + lblTot5.Text;
                lblTot5.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp5 - vr5 < 0)
            {
                lblTot5.Text = "+" + lblTot5.Text.Replace("-", "");
                lblTot5.ForeColor = System.Drawing.Color.Red;
            }

            lblTot6.Text = String.Format("{0:#,##0.00}", vp6 - vr6);
            if (vp6 - vr6 > 0)
            {
                lblTot6.Text = "-" + lblTot6.Text;
                lblTot6.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp6 - vr6 < 0)
            {
                lblTot6.Text = "+" + lblTot6.Text.Replace("-", "");
                lblTot6.ForeColor = System.Drawing.Color.Red;
            }

            lblTot7.Text = String.Format("{0:#,##0.00}", vp7 - vr7);
            if (vp7 - vr7 > 0)
            {
                lblTot7.Text = "-" + lblTot7.Text;
                lblTot7.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp7 - vr7 < 0)
            {
                lblTot7.Text = "+" + lblTot7.Text.Replace("-", "");
                lblTot7.ForeColor = System.Drawing.Color.Red;
            }

            lblTot8.Text = String.Format("{0:#,##0.00}", vp8 - vr8);
            if (vp8 - vr8 > 0)
            {
                lblTot8.Text = "-" + lblTot8.Text;
                lblTot8.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp8 - vr8 < 0)
            {
                lblTot8.Text = "+" + lblTot8.Text.Replace("-", "");
                lblTot8.ForeColor = System.Drawing.Color.Red;
            }

            lblTot9.Text = String.Format("{0:#,##0.00}", vp9 - vr9);
            if (vp9 - vr9 > 0)
            {
                lblTot9.Text = "-" + lblTot9.Text;
                lblTot9.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp9 - vr9 < 0)
            {
                lblTot9.Text = "+" + lblTot9.Text.Replace("-", "");
                lblTot9.ForeColor = System.Drawing.Color.Red;
            }

            lblTot10.Text = String.Format("{0:#,##0.00}", vp10 - vr10);
            if (vp10 - vr10 > 0)
            {
                lblTot10.Text = "-" + lblTot10.Text;
                lblTot10.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp10 - vr10 < 0)
            {
                lblTot10.Text = "+" + lblTot10.Text.Replace("-", "");
                lblTot10.ForeColor = System.Drawing.Color.Red;
            }

            lblTot11.Text = String.Format("{0:#,##0.00}", vp11 - vr11);
            if (vp11 - vr11 > 0)
            {
                lblTot11.Text = "-" + lblTot11.Text;
                lblTot11.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp11 - vr11 < 0)
            {
                lblTot11.Text = "+" + lblTot11.Text.Replace("-", "");
                lblTot11.ForeColor = System.Drawing.Color.Red;
            }

            lblTot12.Text = String.Format("{0:#,##0.00}", vp12 - vr12);
            if (vp12 - vr12 > 0)
            {
                lblTot12.Text = "-" + lblTot12.Text;
                lblTot12.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vp12 - vr12 < 0)
            {
                lblTot12.Text = "+" + lblTot12.Text.Replace("-","");
                lblTot12.ForeColor = System.Drawing.Color.Red;
            }

            lblTotal.Text = String.Format("{0:#,##0.00}", vpTotal - vrTotal);
            if (vpTotal - vrTotal > 0)
            {
                lblTotal.Text = "-" + lblTotal.Text;
                lblTotal.ForeColor = System.Drawing.Color.Blue;
            }
            else if (vpTotal - vrTotal < 0)
            {
                lblTotal.Text = "+" + lblTotal.Text.Replace("-", "");
                lblTotal.ForeColor = System.Drawing.Color.Red;
            }

            vp1 = vp2= vp3= vp4= vp5= vp6= vp7= vp8= vp9= vp10= vp11= vp12= vpTotal = 0;
            vr1 = vr2= vr3= vr4= vr5= vr6= vr7= vr8= vr9= vr10= vr11= vr12= vrTotal = 0;
        }
    }

    public class RelPlanejConta
    {
        public int CoGrupo { get; set; }
        public int CoSubGrupo { get; set; }
        public string SubGrupo { get; set; }
        public int CoSubGrupo2 { get; set; }
        public string SubGrupo2 { get; set; }
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
                return this.CoGrupo.ToString() + "." + this.CoSubGrupo.ToString().PadLeft(3, '0') + "." + this.CoSubGrupo2.ToString().PadLeft(3, '0') + " - " + this.SubGrupo2;
            }
        }

        public string ContaDesc
        {
            get
            {
                return this.CoGrupo.ToString() + "." + this.CoSubGrupo.ToString().PadLeft(3, '0') + "." + this.CoSubGrupo2.ToString().PadLeft(3, '0') + "." + this.CoConta.ToString().PadLeft(4, '0') + " - " + this.PlanoConta;
            }
        }
    }
}
