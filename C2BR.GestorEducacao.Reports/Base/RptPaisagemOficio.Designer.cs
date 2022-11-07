namespace C2BR.GestorEducacao.Reports
{
    partial class RptPaisagemOficio
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
            this.lblDescPagina = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDescData = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDescHora = new DevExpress.XtraReports.UI.XRLabel();
            this.lblData = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHora = new DevExpress.XtraReports.UI.XRLabel();
            this.lblContato = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCidade = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblUnidade = new DevExpress.XtraReports.UI.XRLabel();
            this.imgLogomarca = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lblInstituicao = new DevExpress.XtraReports.UI.XRLabel();
            this.lblEndereco = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.lblinfoSite = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInfos = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeaderTitulo = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailContent = new DevExpress.XtraReports.UI.DetailBand();
            this.bsReport = new System.Windows.Forms.BindingSource(this.components);
            this.bsHeader = new System.Windows.Forms.BindingSource(this.components);
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblParametros
            // 
            this.lblParametros.Dpi = 254F;
            this.lblParametros.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParametros.LocationFloat = new DevExpress.Utils.PointFloat(7.999996F, 63.32979F);
            this.lblParametros.Name = "lblParametros";
            this.lblParametros.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblParametros.SizeF = new System.Drawing.SizeF(3350F, 41.23573F);
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            this.lblParametros.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblParametros.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblParametros_BeforePrint);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dpi = 254F;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.LocationFloat = new DevExpress.Utils.PointFloat(7.999996F, 0F);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTitulo.SizeF = new System.Drawing.SizeF(3350F, 63.32968F);
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "TÍTULO DO RELATÓRIO";
            this.lblTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 99F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 99F;
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
            this.xrLabel1,
            this.lblDescPagina,
            this.lblDescData,
            this.lblDescHora,
            this.lblData,
            this.lblHora,
            this.lblContato,
            this.lblCidade,
            this.xrPageInfo1,
            this.lblUnidade,
            this.imgLogomarca,
            this.lblInstituicao,
            this.lblEndereco});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 263.0498F;
            this.PageHeader.Name = "PageHeader";
            // 
            // lblDescPagina
            // 
            this.lblDescPagina.Dpi = 254F;
            this.lblDescPagina.Font = new System.Drawing.Font("Arial", 8F);
            this.lblDescPagina.LocationFloat = new DevExpress.Utils.PointFloat(3106F, 136.591F);
            this.lblDescPagina.Name = "lblDescPagina";
            this.lblDescPagina.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDescPagina.SizeF = new System.Drawing.SizeF(89.73999F, 41.2357F);
            this.lblDescPagina.StylePriority.UseFont = false;
            this.lblDescPagina.StylePriority.UseTextAlignment = false;
            this.lblDescPagina.Text = "Pág.:";
            this.lblDescPagina.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblDescPagina.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblDescPagina_BeforePrint);
            // 
            // lblDescData
            // 
            this.lblDescData.Dpi = 254F;
            this.lblDescData.Font = new System.Drawing.Font("Arial", 8F);
            this.lblDescData.LocationFloat = new DevExpress.Utils.PointFloat(3106F, 177.8267F);
            this.lblDescData.Name = "lblDescData";
            this.lblDescData.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDescData.SizeF = new System.Drawing.SizeF(89.73999F, 41.2357F);
            this.lblDescData.StylePriority.UseFont = false;
            this.lblDescData.StylePriority.UseTextAlignment = false;
            this.lblDescData.Text = "Data:";
            this.lblDescData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblDescData.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblDescData_BeforePrint);
            // 
            // lblDescHora
            // 
            this.lblDescHora.Dpi = 254F;
            this.lblDescHora.Font = new System.Drawing.Font("Arial", 8F);
            this.lblDescHora.LocationFloat = new DevExpress.Utils.PointFloat(3106F, 219.814F);
            this.lblDescHora.Name = "lblDescHora";
            this.lblDescHora.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDescHora.SizeF = new System.Drawing.SizeF(89.73999F, 41.2357F);
            this.lblDescHora.StylePriority.UseFont = false;
            this.lblDescHora.StylePriority.UseTextAlignment = false;
            this.lblDescHora.Text = "Hora:";
            this.lblDescHora.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblDescHora.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblDescHora_BeforePrint);
            // 
            // lblData
            // 
            this.lblData.Dpi = 254F;
            this.lblData.Font = new System.Drawing.Font("Arial", 8F);
            this.lblData.LocationFloat = new DevExpress.Utils.PointFloat(3195.74F, 177.8268F);
            this.lblData.Name = "lblData";
            this.lblData.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblData.SizeF = new System.Drawing.SizeF(162.2598F, 41.2357F);
            this.lblData.StylePriority.UseFont = false;
            this.lblData.StylePriority.UseTextAlignment = false;
            this.lblData.Text = "{0}";
            this.lblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblData.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblData_BeforePrint);
            // 
            // lblHora
            // 
            this.lblHora.Dpi = 254F;
            this.lblHora.Font = new System.Drawing.Font("Arial", 8F);
            this.lblHora.LocationFloat = new DevExpress.Utils.PointFloat(3195.74F, 219.814F);
            this.lblHora.Name = "lblHora";
            this.lblHora.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblHora.SizeF = new System.Drawing.SizeF(162.2598F, 41.2357F);
            this.lblHora.StylePriority.UseFont = false;
            this.lblHora.StylePriority.UseTextAlignment = false;
            this.lblHora.Text = "{0}";
            this.lblHora.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblHora.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblHora_BeforePrint);
            // 
            // lblContato
            // 
            this.lblContato.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Contato")});
            this.lblContato.Dpi = 254F;
            this.lblContato.Font = new System.Drawing.Font("Arial", 8F);
            this.lblContato.LocationFloat = new DevExpress.Utils.PointFloat(293.6872F, 221.814F);
            this.lblContato.Name = "lblContato";
            this.lblContato.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblContato.SizeF = new System.Drawing.SizeF(2754.688F, 41.23579F);
            this.lblContato.StylePriority.UseFont = false;
            this.lblContato.StylePriority.UseTextAlignment = false;
            this.lblContato.Text = "Endereco";
            this.lblContato.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblCidade
            // 
            this.lblCidade.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CidadeEstado")});
            this.lblCidade.Dpi = 254F;
            this.lblCidade.Font = new System.Drawing.Font("Arial", 8F);
            this.lblCidade.LocationFloat = new DevExpress.Utils.PointFloat(293.6872F, 179.8267F);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCidade.SizeF = new System.Drawing.SizeF(2754.688F, 41.23578F);
            this.lblCidade.StylePriority.UseFont = false;
            this.lblCidade.StylePriority.UseTextAlignment = false;
            this.lblCidade.Text = "Endereco";
            this.lblCidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Dpi = 254F;
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 8F);
            this.xrPageInfo1.Format = "{0} / {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(3195.74F, 136.5911F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(162.2598F, 41.23572F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblUnidade
            // 
            this.lblUnidade.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Unidade")});
            this.lblUnidade.Dpi = 254F;
            this.lblUnidade.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lblUnidade.LocationFloat = new DevExpress.Utils.PointFloat(294.687F, 47.00004F);
            this.lblUnidade.Name = "lblUnidade";
            this.lblUnidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblUnidade.SizeF = new System.Drawing.SizeF(3063.313F, 46.14552F);
            this.lblUnidade.StylePriority.UseFont = false;
            this.lblUnidade.StylePriority.UseTextAlignment = false;
            this.lblUnidade.Text = "Unidade";
            this.lblUnidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // imgLogomarca
            // 
            this.imgLogomarca.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Logo")});
            this.imgLogomarca.Dpi = 254F;
            this.imgLogomarca.LocationFloat = new DevExpress.Utils.PointFloat(8F, 7.629395E-05F);
            this.imgLogomarca.Name = "imgLogomarca";
            this.imgLogomarca.SizeF = new System.Drawing.SizeF(280.4581F, 233.6042F);
            this.imgLogomarca.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // lblInstituicao
            // 
            this.lblInstituicao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Instituicao")});
            this.lblInstituicao.Dpi = 254F;
            this.lblInstituicao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstituicao.LocationFloat = new DevExpress.Utils.PointFloat(293.6872F, 5.000018F);
            this.lblInstituicao.Name = "lblInstituicao";
            this.lblInstituicao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInstituicao.SizeF = new System.Drawing.SizeF(3052.542F, 41.23569F);
            this.lblInstituicao.StylePriority.UseFont = false;
            this.lblInstituicao.StylePriority.UseTextAlignment = false;
            this.lblInstituicao.Text = "Instituicao";
            this.lblInstituicao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblEndereco
            // 
            this.lblEndereco.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Endereco")});
            this.lblEndereco.Dpi = 254F;
            this.lblEndereco.Font = new System.Drawing.Font("Arial", 8F);
            this.lblEndereco.LocationFloat = new DevExpress.Utils.PointFloat(293.6872F, 137.591F);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblEndereco.SizeF = new System.Drawing.SizeF(2754.688F, 41.23573F);
            this.lblEndereco.StylePriority.UseFont = false;
            this.lblEndereco.StylePriority.UseTextAlignment = false;
            this.lblEndereco.Text = "lblEndereco";
            this.lblEndereco.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblinfoSite,
            this.lblInfos});
            this.PageFooter.Dpi = 254F;
            this.PageFooter.HeightF = 65.80879F;
            this.PageFooter.Name = "PageFooter";
            // 
            // lblinfoSite
            // 
            this.lblinfoSite.Dpi = 254F;
            this.lblinfoSite.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinfoSite.ForeColor = System.Drawing.Color.DarkGray;
            this.lblinfoSite.LocationFloat = new DevExpress.Utils.PointFloat(2267.629F, 18.99997F);
            this.lblinfoSite.Name = "lblinfoSite";
            this.lblinfoSite.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblinfoSite.SizeF = new System.Drawing.SizeF(1090.371F, 36.80877F);
            this.lblinfoSite.StylePriority.UseFont = false;
            this.lblinfoSite.StylePriority.UseForeColor = false;
            this.lblinfoSite.StylePriority.UseTextAlignment = false;
            this.lblinfoSite.Text = "www.portalgestoreducacao.com.br";
            this.lblinfoSite.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblInfos
            // 
            this.lblInfos.Dpi = 254F;
            this.lblInfos.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfos.ForeColor = System.Drawing.Color.DarkGray;
            this.lblInfos.LocationFloat = new DevExpress.Utils.PointFloat(7.999996F, 18.99997F);
            this.lblInfos.Name = "lblInfos";
            this.lblInfos.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInfos.SizeF = new System.Drawing.SizeF(2259.629F, 36.80878F);
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
            this.GroupHeaderTitulo.HeightF = 116.4167F;
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
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(294.687F, 93.14556F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(2465.313F, 37.25331F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "[Descricao]";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // RptPaisagemOficio
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
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(99, 99, 99, 99);
            this.PageHeight = 2159;
            this.PageWidth = 3556;
            this.PaperKind = System.Drawing.Printing.PaperKind.Legal;
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
        public DevExpress.XtraReports.UI.XRLabel lblTitulo;
        public DevExpress.XtraReports.UI.XRLabel lblParametros;
        private DevExpress.XtraReports.UI.XRLabel lblinfoSite;
        private DevExpress.XtraReports.UI.XRLabel lblInfos;
        private System.Windows.Forms.BindingSource bsHeader;
        public DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderTitulo;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.DetailBand DetailContent;
        public System.Windows.Forms.BindingSource bsReport;
        public DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.XRLabel lblData;
        private DevExpress.XtraReports.UI.XRLabel lblHora;
        private DevExpress.XtraReports.UI.XRLabel lblContato;
        private DevExpress.XtraReports.UI.XRLabel lblCidade;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel lblUnidade;
        private DevExpress.XtraReports.UI.XRPictureBox imgLogomarca;
        private DevExpress.XtraReports.UI.XRLabel lblInstituicao;
        private DevExpress.XtraReports.UI.XRLabel lblEndereco;
        private DevExpress.XtraReports.UI.XRLabel lblDescHora;
        private DevExpress.XtraReports.UI.XRLabel lblDescData;
        private DevExpress.XtraReports.UI.XRLabel lblDescPagina;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
    }
}
