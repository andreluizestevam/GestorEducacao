namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8221_AtendimentoOdonto
{
    partial class RptReceituarioMedicamentos
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lblContato = new DevExpress.XtraReports.UI.XRLabel();
            this.imgLogomarca = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 30.20833F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblContato,
            this.imgLogomarca});
            this.PageHeader.HeightF = 142.7083F;
            this.PageHeader.Name = "PageHeader";
            // 
            // lblContato
            // 
            this.lblContato.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Contato")});
            this.lblContato.Font = new System.Drawing.Font("Arial", 8F);
            this.lblContato.LocationFloat = new DevExpress.Utils.PointFloat(75.91437F, 102.9321F);
            this.lblContato.Name = "lblContato";
            this.lblContato.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblContato.SizeF = new System.Drawing.SizeF(525.7744F, 16.23456F);
            this.lblContato.StylePriority.UseFont = false;
            this.lblContato.StylePriority.UseTextAlignment = false;
            this.lblContato.Text = "Endereco";
            this.lblContato.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // imgLogomarca
            // 
            this.imgLogomarca.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Logo")});
            this.imgLogomarca.LocationFloat = new DevExpress.Utils.PointFloat(248.9583F, 0F);
            this.imgLogomarca.Name = "imgLogomarca";
            this.imgLogomarca.SizeF = new System.Drawing.SizeF(110.4166F, 91.97016F);
            this.imgLogomarca.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // RptReceituarioMedicamentos
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 30, 100);
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel lblContato;
        private DevExpress.XtraReports.UI.XRPictureBox imgLogomarca;
    }
}
