namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    partial class RptDiarioClasse
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
            this.Frente = new DevExpress.XtraReports.UI.XRSubreport();
            this.detailReportBand1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.Verso = new DevExpress.XtraReports.UI.XRSubreport();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.detailReportBand2 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.LocationFloat = new DevExpress.Utils.PointFloat(6.999997F, 0F);
            this.lblTitulo.SizeF = new System.Drawing.SizeF(1883.063F, 63.32979F);
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "DIÁRIO DE CLASSE";
            // 
            // lblParametros
            // 
            this.lblParametros.LocationFloat = new DevExpress.Utils.PointFloat(8.00034F, 63.32979F);
            this.lblParametros.SizeF = new System.Drawing.SizeF(1883.063F, 23.04404F);
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            // 
            // GroupHeaderTitulo
            // 
            this.GroupHeaderTitulo.HeightF = 86.37383F;
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Frente});
            this.DetailContent.HeightF = 63.5F;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupFooter1});
            this.DetailReport.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // Frente
            // 
            this.Frente.Dpi = 254F;
            this.Frente.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.Frente.Name = "Frente";
            this.Frente.ReportSource = new C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar.RptDiarioClasseFrente();
            this.Frente.SizeF = new System.Drawing.SizeF(1899F, 58.42F);
            // 
            // detailReportBand1
            // 
            this.detailReportBand1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.detailReportBand1.DataSource = this.bsReport;
            this.detailReportBand1.Dpi = 254F;
            this.detailReportBand1.Level = 1;
            this.detailReportBand1.Name = "detailReportBand1";
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
            this.Verso.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.Verso.Name = "Verso";
            this.Verso.ReportSource = new C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar.RptDiarioClasseVerso();
            this.Verso.SizeF = new System.Drawing.SizeF(1899F, 58.42F);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 52.91667F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            this.GroupFooter1.RepeatEveryPage = true;
            // 
            // detailReportBand2
            // 
            this.detailReportBand2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupFooter2});
            this.detailReportBand2.Dpi = 254F;
            this.detailReportBand2.Level = 2;
            this.detailReportBand2.Name = "detailReportBand2";
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
            // RptDiarioClasse
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport,
            this.detailReportBand1,
            this.detailReportBand2});
            this.Margins = new System.Drawing.Printing.Margins(94, 104, 100, 100);
            this.Version = "12.1";
            this.Controls.SetChildIndex(this.detailReportBand2, 0);
            this.Controls.SetChildIndex(this.detailReportBand1, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.GroupHeaderTitulo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRSubreport Frente;
        private DevExpress.XtraReports.UI.DetailReportBand detailReportBand1;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.XRSubreport Verso;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.DetailReportBand detailReportBand2;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;


    }
}
