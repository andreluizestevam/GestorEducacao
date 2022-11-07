using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1120_PlanejEscolarAlunos
{
    public partial class RptMapaPlanejMatricula : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        decimal vp1, vp2, vp3, vp4, vp5, vp6, vp7, vp8, vp9, vp10, vp11, vp12, vpTotal = 0;
        decimal vr1, vr2, vr3, vr4, vr5, vr6, vr7, vr8, vr9, vr10, vr11, vr12, vrTotal = 0;

        public RptMapaPlanejMatricula()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_MODU_CUR,
                              int strP_CO_DPTO_CUR,
                              string strP_CO_ANO_REF,
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

                var lst = (from tb155 in ctx.tb155_plan_matr
                           join tb01 in ctx.TB01_CURSO on tb155.co_cur equals tb01.CO_CUR
                           where tb01.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && tb01.TB25_EMPRESA.CO_EMP == strP_CO_EMP_REF
                           && tb01.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb01.CO_DPTO_CUR == strP_CO_DPTO_CUR
                           && tb155.co_ano_ref == strP_CO_ANO_REF
                           select new RelPlanejMatric
                           {
                               NomeSerie = tb01.NO_CUR,
                               ValorPlan1 = tb155.vl_plan_mes1,
                               ValorPlan2 = tb155.vl_plan_mes2,
                               ValorPlan3 = tb155.vl_plan_mes3,
                               ValorPlan4 = tb155.vl_plan_mes4,
                               ValorPlan5 = tb155.vl_plan_mes5,
                               ValorPlan6 = tb155.vl_plan_mes6,
                               ValorPlan7 = tb155.vl_plan_mes7,
                               ValorPlan8 = tb155.vl_plan_mes8,
                               ValorPlan9 = tb155.vl_plan_mes9,
                               ValorPlan10 = tb155.vl_plan_mes10,
                               ValorPlan11 = tb155.vl_plan_mes11,
                               ValorPlan12 = tb155.vl_plan_mes12,
                               ValorTotalPlane = (tb155.vl_plan_mes1 ?? 0) + (tb155.vl_plan_mes2 ?? 0) + (tb155.vl_plan_mes3 ?? 0) + (tb155.vl_plan_mes4 ?? 0) +
                               (tb155.vl_plan_mes5 ?? 0) + (tb155.vl_plan_mes6 ?? 0) + (tb155.vl_plan_mes7 ?? 0) + (tb155.vl_plan_mes8 ?? 0) + (tb155.vl_plan_mes9 ?? 0) +
                               (tb155.vl_plan_mes10 ?? 0) + (tb155.vl_plan_mes11 ?? 0) + (tb155.vl_plan_mes12 ?? 0),
                               ValorReali1 = tb155.vl_real_mes1,
                               ValorReali2 = tb155.vl_real_mes2,
                               ValorReali3 = tb155.vl_real_mes3,
                               ValorReali4 = tb155.vl_real_mes4,
                               ValorReali5 = tb155.vl_real_mes5,
                               ValorReali6 = tb155.vl_real_mes6,
                               ValorReali7 = tb155.vl_real_mes7,
                               ValorReali8 = tb155.vl_real_mes8,
                               ValorReali9 = tb155.vl_real_mes9,
                               ValorReali10 = tb155.vl_real_mes10,
                               ValorReali11 = tb155.vl_real_mes11,
                               ValorReali12 = tb155.vl_real_mes12,
                               ValorTotalReali = (tb155.vl_real_mes1 ?? 0) + (tb155.vl_real_mes2 ?? 0) + (tb155.vl_real_mes3 ?? 0) + (tb155.vl_real_mes4 ?? 0) +
                               (tb155.vl_real_mes5 ?? 0) + (tb155.vl_real_mes6 ?? 0) + (tb155.vl_real_mes7 ?? 0) + (tb155.vl_real_mes8 ?? 0) + (tb155.vl_real_mes9 ?? 0) +
                               (tb155.vl_real_mes10 ?? 0) + (tb155.vl_real_mes11 ?? 0) + (tb155.vl_real_mes12 ?? 0)
                           }).OrderBy(p => p.NomeSerie);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta a lista no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPlanejMatric at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Planejamento Matrícula

        public class RelPlanejMatric
        {
            public string NomeSerie { get; set; }
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
        }

        #endregion

        private void DetailContent_AfterPrint(object sender, EventArgs e)
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

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblVP1.Text = vp1.ToString();
            lblVP2.Text = vp2.ToString();
            lblVP3.Text = vp3.ToString();
            lblVP4.Text = vp4.ToString();
            lblVP5.Text = vp5.ToString();
            lblVP6.Text = vp6.ToString();
            lblVP7.Text = vp7.ToString();
            lblVP8.Text = vp8.ToString();
            lblVP9.Text = vp9.ToString();
            lblVP10.Text = vp10.ToString();
            lblVP11.Text = vp11.ToString();
            lblVP12.Text = vp12.ToString();
            lblVPTotal.Text = vpTotal.ToString();

            lblVR1.Text = vr1.ToString();
            lblVR2.Text = vr2.ToString();
            lblVR3.Text = vr3.ToString();
            lblVR4.Text = vr4.ToString();
            lblVR5.Text = vr5.ToString();
            lblVR6.Text = vr6.ToString();
            lblVR7.Text = vr7.ToString();
            lblVR8.Text = vr8.ToString();
            lblVR9.Text = vr9.ToString();
            lblVR10.Text = vr10.ToString();
            lblVR11.Text = vr11.ToString();
            lblVR12.Text = vr12.ToString();
            lblVRTotal.Text = vrTotal.ToString();
        }
    }
}
