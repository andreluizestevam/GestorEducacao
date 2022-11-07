namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5500_GeraFluxoCaixa
{
    partial class RptFluxoCaixaAnalitico
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblHader = new DevExpress.XtraReports.UI.XRTable();
            this.rowHeader = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.celDescricaoHeader = new DevExpress.XtraReports.UI.XRTableCell();
            this.celOrigemHeader = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tblReport = new DevExpress.XtraReports.UI.XRTable();
            this.rowReport = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.celDescricao = new DevExpress.XtraReports.UI.XRTableCell();
            this.celOrigem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.tblFooter = new DevExpress.XtraReports.UI.XRTable();
            this.rowFooter = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.celOrigemFooter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.celSaldoFinal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblHader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "EXTRATO DE MOVIMENTAÇÃO DE FLUXO DE CAIXA DA INSTITUIÇÃO - ANALÍTICO";
            // 
            // lblParametros
            // 
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            // 
            // GroupHeaderTitulo
            // 
            this.GroupHeaderTitulo.HeightF = 128.378F;
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblReport});
            this.DetailContent.HeightF = 50F;
            // 
            // bsReport
            // 
            this.bsReport.DataSource = typeof(C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5500_GeraFluxoCaixa.RptFluxoCaixaAnalitico.FluxoCaixaRelatorio);
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeader1,
            this.GroupFooter1});
            this.DetailReport.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.DetailReport.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblHader});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 49.67027F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // tblHader
            // 
            this.tblHader.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.tblHader.BackColor = System.Drawing.Color.Gray;
            this.tblHader.Dpi = 254F;
            this.tblHader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.tblHader.ForeColor = System.Drawing.Color.White;
            this.tblHader.LocationFloat = new DevExpress.Utils.PointFloat(7F, 0F);
            this.tblHader.Name = "tblHader";
            this.tblHader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.rowHeader});
            this.tblHader.SizeF = new System.Drawing.SizeF(1883.063F, 49.67027F);
            this.tblHader.StylePriority.UseBackColor = false;
            this.tblHader.StylePriority.UseFont = false;
            this.tblHader.StylePriority.UseForeColor = false;
            this.tblHader.StylePriority.UseTextAlignment = false;
            this.tblHader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rowHeader
            // 
            this.rowHeader.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.celDescricaoHeader,
            this.celOrigemHeader,
            this.xrTableCell1,
            this.xrTableCell11,
            this.xrTableCell21});
            this.rowHeader.Dpi = 254F;
            this.rowHeader.Name = "rowHeader";
            this.rowHeader.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.CanGrow = false;
            this.xrTableCell7.Dpi = 254F;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(20, 0, 0, 0, 254F);
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "DATA";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.31185757982434437D;
            // 
            // celDescricaoHeader
            // 
            this.celDescricaoHeader.CanGrow = false;
            this.celDescricaoHeader.Dpi = 254F;
            this.celDescricaoHeader.Name = "celDescricaoHeader";
            this.celDescricaoHeader.StylePriority.UseTextAlignment = false;
            this.celDescricaoHeader.Text = "DESCRIÇÃO (CNPJ/CPF - Nº DOC - HISTÓRICO)";
            this.celDescricaoHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.celDescricaoHeader.Weight = 1.3583638730172003D;
            // 
            // celOrigemHeader
            // 
            this.celOrigemHeader.CanGrow = false;
            this.celOrigemHeader.Dpi = 254F;
            this.celOrigemHeader.Name = "celOrigemHeader";
            this.celOrigemHeader.Text = "ORIGEM";
            this.celOrigemHeader.Weight = 0.23271523736485891D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "R$ RECEITA";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell1.Weight = 0.36907994839986291D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.CanGrow = false;
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBackColor = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "R$ DESPESA ";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell11.Weight = 0.35228754813900587D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.CanGrow = false;
            this.xrTableCell21.Dpi = 254F;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.xrTableCell21.StylePriority.UsePadding = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "R$ SALDO";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell21.Weight = 0.38747671888558D;
            // 
            // tblReport
            // 
            this.tblReport.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.tblReport.Dpi = 254F;
            this.tblReport.EvenStyleName = "EvenStyle";
            this.tblReport.Font = new System.Drawing.Font("Arial", 8F);
            this.tblReport.LocationFloat = new DevExpress.Utils.PointFloat(6.999634F, 0F);
            this.tblReport.Name = "tblReport";
            this.tblReport.OddStyleName = "OddStyle";
            this.tblReport.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.rowReport});
            this.tblReport.SizeF = new System.Drawing.SizeF(1883.063F, 50F);
            this.tblReport.StylePriority.UseBackColor = false;
            this.tblReport.StylePriority.UseFont = false;
            this.tblReport.StylePriority.UseForeColor = false;
            this.tblReport.StylePriority.UseTextAlignment = false;
            this.tblReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rowReport
            // 
            this.rowReport.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.celDescricao,
            this.celOrigem,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.rowReport.Dpi = 254F;
            this.rowReport.Name = "rowReport";
            this.rowReport.StylePriority.UseForeColor = false;
            this.rowReport.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.CanGrow = false;
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DtMovimento", "{0:dd/MM/yy}")});
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(20, 0, 0, 0, 254F);
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "DATA";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 0.31185816554405521D;
            this.xrTableCell2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell2_BeforePrint);
            // 
            // celDescricao
            // 
            this.celDescricao.CanGrow = false;
            this.celDescricao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Descricao")});
            this.celDescricao.Dpi = 254F;
            this.celDescricao.Multiline = true;
            this.celDescricao.Name = "celDescricao";
            this.celDescricao.StylePriority.UseTextAlignment = false;
            this.celDescricao.Text = "DESCRIÇÃO";
            this.celDescricao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.celDescricao.Weight = 1.3583639706371526D;
            // 
            // celOrigem
            // 
            this.celOrigem.CanGrow = false;
            this.celOrigem.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OrigemPgto")});
            this.celOrigem.Dpi = 254F;
            this.celOrigem.Name = "celOrigem";
            this.celOrigem.Weight = 0.23271504212495531D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.CanGrow = false;
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receita", "{0:n}")});
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.NullValueText = "0,00";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "R$ RECEITA";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell4.Weight = 0.369079948399863D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.CanGrow = false;
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Despesa", "{0:n}")});
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.NullValueText = "0,00";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "R$ DESPESA ";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.35228757254399373D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.CanGrow = false;
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Saldo", "{0:\'+\'#,##0.00;\'-\'#,##0.00}")});
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "R$ SALDO";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell6.Weight = 0.387476206380833D;
            this.xrTableCell6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell6_BeforePrint);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblFooter,
            this.xrLine1});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 71.4375F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // tblFooter
            // 
            this.tblFooter.Dpi = 254F;
            this.tblFooter.EvenStyleName = "EvenStyle";
            this.tblFooter.Font = new System.Drawing.Font("Arial", 8F);
            this.tblFooter.LocationFloat = new DevExpress.Utils.PointFloat(6.999634F, 5.291667F);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.OddStyleName = "OddStyle";
            this.tblFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.rowFooter});
            this.tblFooter.SizeF = new System.Drawing.SizeF(1883.063F, 50F);
            this.tblFooter.StylePriority.UseBackColor = false;
            this.tblFooter.StylePriority.UseFont = false;
            this.tblFooter.StylePriority.UseForeColor = false;
            this.tblFooter.StylePriority.UseTextAlignment = false;
            this.tblFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rowFooter
            // 
            this.rowFooter.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell8,
            this.celOrigemFooter,
            this.xrTableCell10,
            this.xrTableCell12,
            this.celSaldoFinal});
            this.rowFooter.Dpi = 254F;
            this.rowFooter.Name = "rowFooter";
            this.rowFooter.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Weight = 0.31185807154895318D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.CanGrow = false;
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "TOTAIS";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell8.Weight = 1.3583640646322548D;
            // 
            // celOrigemFooter
            // 
            this.celOrigemFooter.CanGrow = false;
            this.celOrigemFooter.Dpi = 254F;
            this.celOrigemFooter.Name = "celOrigemFooter";
            this.celOrigemFooter.Weight = 0.23271504212495531D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.CanGrow = false;
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receita")});
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.NullValueText = "0,00";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell10.Summary = xrSummary1;
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell10.Weight = 0.369079948399863D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.CanGrow = false;
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Despesa")});
            this.xrTableCell12.Dpi = 254F;
            this.xrTableCell12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.NullValueText = "0,00";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell12.Summary = xrSummary2;
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 0.35228757254399373D;
            // 
            // celSaldoFinal
            // 
            this.celSaldoFinal.CanGrow = false;
            this.celSaldoFinal.Dpi = 254F;
            this.celSaldoFinal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.celSaldoFinal.Name = "celSaldoFinal";
            this.celSaldoFinal.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.celSaldoFinal.StylePriority.UseFont = false;
            this.celSaldoFinal.StylePriority.UsePadding = false;
            this.celSaldoFinal.StylePriority.UseTextAlignment = false;
            this.celSaldoFinal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.celSaldoFinal.Weight = 0.387476206380833D;
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(6.999634F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(1883.063F, 5.291667F);
            // 
            // RptFluxoCaixaAnalitico
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport});
            this.Margins = new System.Drawing.Printing.Margins(101, 101, 100, 100);
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblHader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable tblReport;
        private DevExpress.XtraReports.UI.XRTableRow rowReport;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell celDescricao;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTable tblHader;
        private DevExpress.XtraReports.UI.XRTableRow rowHeader;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell celDescricaoHeader;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRTableCell celOrigem;
        private DevExpress.XtraReports.UI.XRTableCell celOrigemHeader;
        private DevExpress.XtraReports.UI.XRTable tblFooter;
        private DevExpress.XtraReports.UI.XRTableRow rowFooter;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell celOrigemFooter;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell celSaldoFinal;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
    }
}
