namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar
{
    partial class RptDeclaracaoAdvertencia
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptDeclaracaoAdvertencia));
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.lblConteudo = new DevExpress.XtraReports.UI.XRRichText();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.lblTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblParametro = new DevExpress.XtraReports.UI.XRRichText();
            this.bsReport = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConteudo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblParametro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.ReportFooter});
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblConteudo});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 63.5F;
            this.Detail1.Name = "Detail1";
            // 
            // lblConteudo
            // 
            this.lblConteudo.Dpi = 254F;
            this.lblConteudo.Font = new System.Drawing.Font("Arial", 9F);
            this.lblConteudo.LocationFloat = new DevExpress.Utils.PointFloat(31.75F, 0F);
            this.lblConteudo.Name = "lblConteudo";
            this.lblConteudo.SerializableRtfString = resources.GetString("lblConteudo.SerializableRtfString");
            this.lblConteudo.SizeF = new System.Drawing.SizeF(1841.5F, 63.5F);
            this.lblConteudo.StylePriority.UseFont = false;
            this.lblConteudo.Visible = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 44.97917F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTitulo,
            this.lblParametro});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 254F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dpi = 254F;
            this.lblTitulo.Font = new System.Drawing.Font("Times New Roman", 15.5F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.LocationFloat = new DevExpress.Utils.PointFloat(31.75F, 63.5F);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTitulo.SizeF = new System.Drawing.SizeF(1841.5F, 63.5F);
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblParametro
            // 
            this.lblParametro.Dpi = 254F;
            this.lblParametro.Font = new System.Drawing.Font("Arial", 9F);
            this.lblParametro.LocationFloat = new DevExpress.Utils.PointFloat(31.75F, 0F);
            this.lblParametro.Name = "lblParametro";
            this.lblParametro.SerializableRtfString = resources.GetString("lblParametro.SerializableRtfString");
            this.lblParametro.SizeF = new System.Drawing.SizeF(1841.5F, 63.5F);
            // 
            // RptDeclaracaoAdvertencia
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailReport,
            this.GroupHeader1});
            this.Version = "12.1";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConteudo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblParametro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.XRRichText lblConteudo;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRRichText lblParametro;
        private System.Windows.Forms.BindingSource bsReport;
        private DevExpress.XtraReports.UI.XRLabel lblTitulo;
    }
}
