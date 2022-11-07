namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores._1240_ContrDeDeclaracoes
{
    partial class RptDeclaracoesFuncionais
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
            this.GroupHeaderTitle = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.lblParametro = new DevExpress.XtraReports.UI.XRLabel();
            this.lbltitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailContent = new DevExpress.XtraReports.UI.DetailBand();
            this.richPagina1 = new DevExpress.XtraReports.UI.XRRichText();
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // GroupHeaderTitle
            // 
            this.GroupHeaderTitle.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblParametro,
            this.lbltitulo});
            this.GroupHeaderTitle.Dpi = 254F;
            this.GroupHeaderTitle.HeightF = 254F;
            this.GroupHeaderTitle.Name = "GroupHeaderTitle";
            // 
            // lblParametro
            // 
            this.lblParametro.Dpi = 254F;
            this.lblParametro.LocationFloat = new DevExpress.Utils.PointFloat(25.40002F, 0F);
            this.lblParametro.Name = "lblParametro";
            this.lblParametro.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblParametro.SizeF = new System.Drawing.SizeF(1847.85F, 58.41996F);
            // 
            // lbltitulo
            // 
            this.lbltitulo.Dpi = 254F;
            this.lbltitulo.Font = new System.Drawing.Font("Times New Roman", 15.5F, System.Drawing.FontStyle.Bold);
            this.lbltitulo.LocationFloat = new DevExpress.Utils.PointFloat(25.40002F, 58.41996F);
            this.lbltitulo.Name = "lbltitulo";
            this.lbltitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbltitulo.SizeF = new System.Drawing.SizeF(1847.85F, 58.41996F);
            this.lbltitulo.StylePriority.UseFont = false;
            this.lbltitulo.StylePriority.UseTextAlignment = false;
            this.lbltitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
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
            this.DetailContent.HeightF = 60.85417F;
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
            this.richPagina1.SizeF = new System.Drawing.SizeF(1841.5F, 58.41996F);
            this.richPagina1.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 5, 254F);
            this.richPagina1.StylePriority.UseFont = false;
            this.richPagina1.Visible = false;
            // 
            // RptDeclaracaoDeFrequencia
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitle,
            this.DetailReport});
            this.Margins = new System.Drawing.Printing.Margins(99, 99, 100, 100);
            this.Version = "12.1";
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.GroupHeaderTitle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderTitle;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand DetailContent;
        private DevExpress.XtraReports.UI.XRRichText richPagina1;
        private DevExpress.XtraReports.UI.XRLabel lbltitulo;
        private DevExpress.XtraReports.UI.XRLabel lblParametro;
    }
}
