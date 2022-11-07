namespace C2BR.GestorEducacao.Reports
{
    partial class RptRetratoOdonto
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblParametros = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.OddStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.EvenStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.pnlPadrao = new DevExpress.XtraReports.UI.XRPanel();
            this.imgLogomarca = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lblUnidade = new DevExpress.XtraReports.UI.XRLabel();
            this.lblEndereco = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCidade = new DevExpress.XtraReports.UI.XRLabel();
            this.lblContato = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInfos = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeaderTitulo = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailContent = new DevExpress.XtraReports.UI.DetailBand();
            this.bsReport = new System.Windows.Forms.BindingSource(this.components);
            this.bsHeader = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 2.645833F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblParametros
            // 
            this.lblParametros.Dpi = 254F;
            this.lblParametros.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParametros.LocationFloat = new DevExpress.Utils.PointFloat(7F, 63.32979F);
            this.lblParametros.Name = "lblParametros";
            this.lblParametros.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblParametros.SizeF = new System.Drawing.SizeF(1883.063F, 41.23573F);
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            this.lblParametros.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblParametros.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblParametros_BeforePrint);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dpi = 254F;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.LocationFloat = new DevExpress.Utils.PointFloat(7F, 0F);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTitulo.SizeF = new System.Drawing.SizeF(1883.063F, 63.32968F);
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "TÍTULO DO RELATÓRIO";
            this.lblTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // OddStyle
            // 
            this.OddStyle.BackColor = System.Drawing.Color.LightGray;
            this.OddStyle.Name = "OddStyle";
            // 
            // EvenStyle
            // 
            this.EvenStyle.BackColor = System.Drawing.Color.White;
            this.EvenStyle.Name = "EvenStyle";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlPadrao});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 510.6667F;
            this.PageHeader.Name = "PageHeader";
            // 
            // pnlPadrao
            // 
            this.pnlPadrao.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgLogomarca,
            this.lblUnidade,
            this.lblEndereco,
            this.lblCidade,
            this.lblContato});
            this.pnlPadrao.Dpi = 254F;
            this.pnlPadrao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.pnlPadrao.Name = "pnlPadrao";
            this.pnlPadrao.SizeF = new System.Drawing.SizeF(1902F, 510.6667F);
            // 
            // imgLogomarca
            // 
            this.imgLogomarca.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Logo")});
            this.imgLogomarca.Dpi = 254F;
            this.imgLogomarca.LocationFloat = new DevExpress.Utils.PointFloat(758.4166F, 25.00001F);
            this.imgLogomarca.Name = "imgLogomarca";
            this.imgLogomarca.SizeF = new System.Drawing.SizeF(425.979F, 249.0211F);
            this.imgLogomarca.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // lblUnidade
            // 
            this.lblUnidade.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Unidade")});
            this.lblUnidade.Dpi = 254F;
            this.lblUnidade.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblUnidade.LocationFloat = new DevExpress.Utils.PointFloat(151.8123F, 274.0211F);
            this.lblUnidade.Name = "lblUnidade";
            this.lblUnidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblUnidade.SizeF = new System.Drawing.SizeF(1592.73F, 46.14552F);
            this.lblUnidade.StylePriority.UseFont = false;
            this.lblUnidade.StylePriority.UseTextAlignment = false;
            this.lblUnidade.Text = "Unidade";
            this.lblUnidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblEndereco
            // 
            this.lblEndereco.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Endereco")});
            this.lblEndereco.Dpi = 254F;
            this.lblEndereco.Font = new System.Drawing.Font("Arial", 8F);
            this.lblEndereco.LocationFloat = new DevExpress.Utils.PointFloat(297.333F, 320.1667F);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblEndereco.SizeF = new System.Drawing.SizeF(1335.467F, 41.23573F);
            this.lblEndereco.StylePriority.UseFont = false;
            this.lblEndereco.StylePriority.UseTextAlignment = false;
            this.lblEndereco.Text = "Endereco";
            this.lblEndereco.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCidade
            // 
            this.lblCidade.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CidadeEstado")});
            this.lblCidade.Dpi = 254F;
            this.lblCidade.Font = new System.Drawing.Font("Arial", 8F);
            this.lblCidade.LocationFloat = new DevExpress.Utils.PointFloat(297.333F, 361.4024F);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCidade.SizeF = new System.Drawing.SizeF(1335.467F, 41.23578F);
            this.lblCidade.StylePriority.UseFont = false;
            this.lblCidade.StylePriority.UseTextAlignment = false;
            this.lblCidade.Text = "Endereco";
            this.lblCidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblContato
            // 
            this.lblContato.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Contato")});
            this.lblContato.Dpi = 254F;
            this.lblContato.Font = new System.Drawing.Font("Arial", 8F);
            this.lblContato.LocationFloat = new DevExpress.Utils.PointFloat(297.333F, 402.6382F);
            this.lblContato.Name = "lblContato";
            this.lblContato.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblContato.SizeF = new System.Drawing.SizeF(1335.467F, 41.23578F);
            this.lblContato.StylePriority.UseFont = false;
            this.lblContato.StylePriority.UseTextAlignment = false;
            this.lblContato.Text = "Endereco";
            this.lblContato.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.lblInfos});
            this.PageFooter.Dpi = 254F;
            this.PageFooter.HeightF = 65.80879F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel13.ForeColor = System.Drawing.Color.DarkGray;
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(1473.713F, 17.00009F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(416.3501F, 39.80877F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseForeColor = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "www.portalgestoreducacao .com.br";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblInfos
            // 
            this.lblInfos.Dpi = 254F;
            this.lblInfos.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfos.ForeColor = System.Drawing.Color.DarkGray;
            this.lblInfos.LocationFloat = new DevExpress.Utils.PointFloat(6.999997F, 16.99993F);
            this.lblInfos.Name = "lblInfos";
            this.lblInfos.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInfos.SizeF = new System.Drawing.SizeF(1441.838F, 39.80878F);
            this.lblInfos.StylePriority.UseFont = false;
            this.lblInfos.StylePriority.UseForeColor = false;
            this.lblInfos.StylePriority.UseTextAlignment = false;
            this.lblInfos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblInfos.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblInfos_BeforePrint);
            // 
            // GroupHeaderTitulo
            // 
            this.GroupHeaderTitulo.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTitulo,
            this.lblParametros});
            this.GroupHeaderTitulo.Dpi = 254F;
            this.GroupHeaderTitulo.HeightF = 119.0833F;
            this.GroupHeaderTitulo.Name = "GroupHeaderTitulo";
            this.GroupHeaderTitulo.RepeatEveryPage = true;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailContent});
            this.DetailReport.DataSource = this.bsReport;
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // DetailContent
            // 
            this.DetailContent.Dpi = 254F;
            this.DetailContent.HeightF = 254F;
            this.DetailContent.Name = "DetailContent";
            // 
            // bsHeader
            // 
            this.bsHeader.DataSource = typeof(C2BR.GestorEducacao.Reports.ReportHeader);
            // 
            // RptRetratoOdonto
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.GroupHeaderTitulo,
            this.DetailReport});
            this.DataSource = this.bsHeader;
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(101, 98, 100, 100);
            this.PageHeight = 2969;
            this.PageWidth = 2101;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.OddStyle,
            this.EvenStyle});
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRControlStyle OddStyle;
        private DevExpress.XtraReports.UI.XRControlStyle EvenStyle;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel lblEndereco;
        private DevExpress.XtraReports.UI.XRLabel lblUnidade;
        public DevExpress.XtraReports.UI.XRLabel lblTitulo;
        public DevExpress.XtraReports.UI.XRLabel lblParametros;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel lblInfos;
        private System.Windows.Forms.BindingSource bsHeader;
        public DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderTitulo;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.DetailBand DetailContent;
        public System.Windows.Forms.BindingSource bsReport;
        public DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.XRLabel lblContato;
        private DevExpress.XtraReports.UI.XRLabel lblCidade;
        private DevExpress.XtraReports.UI.XRPanel pnlPadrao;
        private DevExpress.XtraReports.UI.XRPictureBox imgLogomarca;
    }
}
