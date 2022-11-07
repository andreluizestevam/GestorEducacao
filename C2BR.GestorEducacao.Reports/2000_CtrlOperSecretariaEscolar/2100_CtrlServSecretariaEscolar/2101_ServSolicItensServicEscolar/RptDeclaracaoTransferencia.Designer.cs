namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar
{
    partial class RptDeclaracaoTransferencia
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
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailContent = new DevExpress.XtraReports.UI.DetailBand();
            this.richPagina1 = new DevExpress.XtraReports.UI.XRRichText();
            this.GroupHeaderTitulo = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.lblParametro = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTitulo = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailContent});
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.richPagina1});
            this.DetailContent.Dpi = 254F;
            this.DetailContent.HeightF = 58.42F;
            this.DetailContent.Name = "DetailContent";
            // 
            // richPagina1
            // 
            this.richPagina1.Dpi = 254F;
            this.richPagina1.Font = new System.Drawing.Font("Arial", 9F);
            this.richPagina1.KeepTogether = true;
            this.richPagina1.LocationFloat = new DevExpress.Utils.PointFloat(31.75F, 0F);
            this.richPagina1.Name = "richPagina1";
            this.richPagina1.SerializableRtfString = "ewBcAHIAdABmADEAXABhAG4AcwBpAFwAYQBuAHMAaQBjAHAAZwAxADIANQAyAFwAZABlAGYAZgAwAHsAX" +
                "ABmAG8AbgB0AHQAYgBsAHsAXABmADAAXABmAG4AaQBsACAAVABpAG0AZQBzACAATgBlAHcAIABSAG8Ab" +
                "QBhAG4AOwB9AH0A";
            this.richPagina1.SizeF = new System.Drawing.SizeF(1841.5F, 58.42F);
            this.richPagina1.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 5, 254F);
            this.richPagina1.StylePriority.UseFont = false;
            this.richPagina1.Visible = false;
            // 
            // GroupHeaderTitulo
            // 
            this.GroupHeaderTitulo.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblParametro,
            this.lblTitulo});
            this.GroupHeaderTitulo.Dpi = 254F;
            this.GroupHeaderTitulo.HeightF = 217.3125F;
            this.GroupHeaderTitulo.Name = "GroupHeaderTitulo";
            // 
            // lblParametro
            // 
            this.lblParametro.Dpi = 254F;
            this.lblParametro.LocationFloat = new DevExpress.Utils.PointFloat(31.75F, 0F);
            this.lblParametro.Name = "lblParametro";
            this.lblParametro.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblParametro.SizeF = new System.Drawing.SizeF(1841.5F, 58.41996F);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dpi = 254F;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 15.5F);
            this.lblTitulo.LocationFloat = new DevExpress.Utils.PointFloat(31.75F, 58.42004F);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTitulo.SizeF = new System.Drawing.SizeF(1841.5F, 58.41997F);
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // RptDeclaracaoTransferencia
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailReport,
            this.GroupHeaderTitulo});
            this.Margins = new System.Drawing.Printing.Margins(99, 99, 100, 100);
            this.Version = "12.1";
            this.Controls.SetChildIndex(this.GroupHeaderTitulo, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand DetailContent;
        private DevExpress.XtraReports.UI.XRRichText richPagina1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderTitulo;
        private DevExpress.XtraReports.UI.XRLabel lblTitulo;
        private DevExpress.XtraReports.UI.XRLabel lblParametro;
    }
}
