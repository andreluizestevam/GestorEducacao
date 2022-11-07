namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    partial class RptDiarioClasseBimestral
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.Verso = new DevExpress.XtraReports.UI.XRSubreport();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.Frente = new DevExpress.XtraReports.UI.XRSubreport();
            this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.FrenteNotas = new DevExpress.XtraReports.UI.XRSubreport();
            this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
            this.Frente2Pagina = new DevExpress.XtraReports.UI.XRSubreport();
            this.GroupFooter4 = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "DIÁRIO DE CLASSE";
            // 
            // lblParametros
            // 
            this.lblParametros.LocationFloat = new DevExpress.Utils.PointFloat(7.999996F, 63.32979F);
            this.lblParametros.SizeF = new System.Drawing.SizeF(2752F, 28.00657F);
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            // 
            // GroupHeaderTitulo
            // 
            this.GroupHeaderTitulo.HeightF = 91.33636F;
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Frente});
            this.DetailContent.HeightF = 58.42F;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupFooter1});
            this.DetailReport.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport1.Dpi = 254F;
            this.DetailReport1.Level = 3;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Verso});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 60.85417F;
            this.Detail1.Name = "Detail1";
            // 
            // Verso
            // 
            this.Verso.Dpi = 254F;
            this.Verso.LocationFloat = new DevExpress.Utils.PointFloat(7.999996F, 0F);
            this.Verso.Name = "Verso";
            this.Verso.ReportSource = new C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar.RptDiarioClasseVerso();
            this.Verso.SizeF = new System.Drawing.SizeF(2752F, 58.42F);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 0F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // Frente
            // 
            this.Frente.Dpi = 254F;
            this.Frente.LocationFloat = new DevExpress.Utils.PointFloat(7.999996F, 0F);
            this.Frente.Name = "Frente";
            this.Frente.ReportSource = new C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar.RptDiarioClasseFrente();
            this.Frente.SizeF = new System.Drawing.SizeF(2752F, 58.42F);
            // 
            // DetailReport2
            // 
            this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupFooter2});
            this.DetailReport2.Dpi = 254F;
            this.DetailReport2.Level = 4;
            this.DetailReport2.Name = "DetailReport2";
            // 
            // Detail2
            // 
            this.Detail2.Dpi = 254F;
            this.Detail2.HeightF = 254F;
            this.Detail2.Name = "Detail2";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Dpi = 254F;
            this.GroupFooter2.HeightF = 254F;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // DetailReport3
            // 
            this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupFooter3});
            this.DetailReport3.Dpi = 254F;
            this.DetailReport3.Level = 2;
            this.DetailReport3.Name = "DetailReport3";
            // 
            // Detail3
            // 
            this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.FrenteNotas});
            this.Detail3.Dpi = 254F;
            this.Detail3.HeightF = 58.42F;
            this.Detail3.Name = "Detail3";
            // 
            // FrenteNotas
            // 
            this.FrenteNotas.Dpi = 254F;
            this.FrenteNotas.LocationFloat = new DevExpress.Utils.PointFloat(7.999673F, 0F);
            this.FrenteNotas.Name = "FrenteNotas";
            this.FrenteNotas.ReportSource = new C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar.RptDiarioClasseFrente();
            this.FrenteNotas.SizeF = new System.Drawing.SizeF(2752F, 58.42F);
            // 
            // GroupFooter3
            // 
            this.GroupFooter3.Dpi = 254F;
            this.GroupFooter3.HeightF = 0F;
            this.GroupFooter3.Name = "GroupFooter3";
            this.GroupFooter3.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // DetailReport4
            // 
            this.DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupFooter4});
            this.DetailReport4.Dpi = 254F;
            this.DetailReport4.Level = 1;
            this.DetailReport4.Name = "DetailReport4";
            // 
            // Detail4
            // 
            this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Frente2Pagina});
            this.Detail4.Dpi = 254F;
            this.Detail4.HeightF = 58.42F;
            this.Detail4.Name = "Detail4";
            // 
            // Frente2Pagina
            // 
            this.Frente2Pagina.Dpi = 254F;
            this.Frente2Pagina.LocationFloat = new DevExpress.Utils.PointFloat(8.000319F, 0F);
            this.Frente2Pagina.Name = "Frente2Pagina";
            this.Frente2Pagina.ReportSource = new C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar.RptDiarioClasseFrente();
            this.Frente2Pagina.SizeF = new System.Drawing.SizeF(2752F, 58.42F);
            // 
            // GroupFooter4
            // 
            this.GroupFooter4.Dpi = 254F;
            this.GroupFooter4.HeightF = 0F;
            this.GroupFooter4.Name = "GroupFooter4";
            this.GroupFooter4.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // RptDiarioClasseBimestral
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport,
            this.DetailReport1,
            this.DetailReport2,
            this.DetailReport3,
            this.DetailReport4});
            this.Version = "12.1";
            this.Controls.SetChildIndex(this.DetailReport4, 0);
            this.Controls.SetChildIndex(this.DetailReport3, 0);
            this.Controls.SetChildIndex(this.DetailReport2, 0);
            this.Controls.SetChildIndex(this.DetailReport1, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.GroupHeaderTitulo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRSubreport Frente;
        private DevExpress.XtraReports.UI.XRSubreport Verso;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport2;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport3;
        private DevExpress.XtraReports.UI.DetailBand Detail3;
        private DevExpress.XtraReports.UI.XRSubreport FrenteNotas;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter3;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport4;
        private DevExpress.XtraReports.UI.DetailBand Detail4;
        private DevExpress.XtraReports.UI.XRSubreport Frente2Pagina;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter4;
    }
}
