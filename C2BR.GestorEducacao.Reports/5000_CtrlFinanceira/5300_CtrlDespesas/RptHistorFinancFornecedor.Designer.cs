namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas
{
    partial class RptHistorFinancFornecedor
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
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorPago = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblJurosDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblMultaDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDesctoDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorTotDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblTotParcMulta = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcJuros = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcTot = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcDescto = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcPago = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcDocto = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.lblvmul = new DevExpress.XtraReports.UI.XRLabel();
            this.lblvjur = new DevExpress.XtraReports.UI.XRLabel();
            this.lblvtot = new DevExpress.XtraReports.UI.XRLabel();
            this.lblvdes = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblvpag = new DevExpress.XtraReports.UI.XRLabel();
            this.lblvp = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "HISTÓRICO DE TÍTULOS DE DESPESAS DE PAGAMENTOS POR FORNECEDOR/CREDOR";
            // 
            // lblParametros
            // 
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.DetailContent.HeightF = 49.67026F;
            this.DetailContent.AfterPrint += new System.EventHandler(this.DetailContent_AfterPrint);
            // 
            // bsReport
            // 
            this.bsReport.DataSource = typeof(C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas.RptHistorFinancFornecedor.RelHistoFinanFornec);
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeader1,
            this.ReportFooter,
            this.GroupFooter1});
            this.DetailReport.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.DetailReport.Controls.SetChildIndex(this.ReportFooter, 0);
            this.DetailReport.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel7,
            this.xrTable1});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeDesc", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 100.6771F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NomeDesc")});
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(194.0208F, 57.44134F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(1567.292F, 41.23571F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "xrLabel6";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 57.44134F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(190.5833F, 41.23568F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Fornecedor:";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrTable1.BackColor = System.Drawing.Color.Gray;
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable1.ForeColor = System.Drawing.Color.White;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(1F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1900F, 49.67027F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell7,
            this.xrTableCell1,
            this.xrTableCell15,
            this.xrTableCell11,
            this.xrTableCell3,
            this.xrTableCell8,
            this.xrTableCell4,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell12});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.CanGrow = false;
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "PA";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell10.Weight = 0.07921569911576D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.CanGrow = false;
            this.xrTableCell7.Dpi = 254F;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "VENCTO";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.20596081876356015D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Nº DOC/STATUS";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.8036431714416209D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.CanGrow = false;
            this.xrTableCell15.Dpi = 254F;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "VALOR";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell15.Weight = 0.238472522466887D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.CanGrow = false;
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(7, 0, 0, 0, 254F);
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "DT PAG";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell11.Weight = 0.20223095213556555D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.CanGrow = false;
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "R$ PAGO";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.26524032206747772D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.CanGrow = false;
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "DIAS";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell8.Weight = 0.11981432807871484D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.CanGrow = false;
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "JUROS";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell4.Weight = 0.24975996216424617D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.CanGrow = false;
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "MULTA";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell2.Weight = 0.26002573013530939D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.CanGrow = false;
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "DESCTO";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.26560370672967265D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.CanGrow = false;
            this.xrTableCell12.Dpi = 254F;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "VALOR";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 0.32181369253203818D;
            // 
            // xrTable2
            // 
            this.xrTable2.Dpi = 254F;
            this.xrTable2.EvenStyleName = "EvenStyle";
            this.xrTable2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(1F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.OddStyleName = "OddStyle";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1900F, 49.67026F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell6,
            this.xrTableCell13,
            this.lblValorDocto,
            this.xrTableCell9,
            this.lblValorPago,
            this.xrTableCell16,
            this.lblJurosDocto,
            this.lblMultaDocto,
            this.lblDesctoDocto,
            this.lblValorTotDocto});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Parcela", "{0:00}")});
            this.xrTableCell14.Dpi = 254F;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell14.StylePriority.UsePadding = false;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.Weight = 0.0792156957908866D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataVencimento", "{0:dd/MM/yy}")});
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 0.20596080742309444D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DescricaoDesc")});
            this.xrTableCell13.Dpi = 254F;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "xrTableCell13";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell13.Weight = 0.80364312439806118D;
            // 
            // lblValorDocto
            // 
            this.lblValorDocto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValorDocumento", "{0:#,##0.00}")});
            this.lblValorDocto.Dpi = 254F;
            this.lblValorDocto.Name = "lblValorDocto";
            this.lblValorDocto.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblValorDocto.StylePriority.UsePadding = false;
            this.lblValorDocto.StylePriority.UseTextAlignment = false;
            this.lblValorDocto.Text = "lblValorDocto";
            this.lblValorDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblValorDocto.Weight = 0.23847240044814932D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataPagamento", "{0:dd/MM/yy}")});
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(7, 0, 0, 0, 254F);
            this.xrTableCell9.StylePriority.UsePadding = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 0.20223103520997812D;
            // 
            // lblValorPago
            // 
            this.lblValorPago.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValorPago", "{0:#,##0.00}")});
            this.lblValorPago.Dpi = 254F;
            this.lblValorPago.Name = "lblValorPago";
            this.lblValorPago.StylePriority.UseTextAlignment = false;
            this.lblValorPago.Text = "lblValorPago";
            this.lblValorPago.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblValorPago.Weight = 0.2652402692863664D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DiasDesc")});
            this.xrTableCell16.Dpi = 254F;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "xrTableCell16";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell16.Weight = 0.11981451558859593D;
            this.xrTableCell16.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell16_BeforePrint);
            // 
            // lblJurosDocto
            // 
            this.lblJurosDocto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "JurosDesc", "{0:#,##0.00}")});
            this.lblJurosDocto.Dpi = 254F;
            this.lblJurosDocto.Name = "lblJurosDocto";
            this.lblJurosDocto.StylePriority.UseTextAlignment = false;
            this.lblJurosDocto.Text = "lblJurosDocto";
            this.lblJurosDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblJurosDocto.Weight = 0.24975953708834D;
            // 
            // lblMultaDocto
            // 
            this.lblMultaDocto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MultaDesc", "{0:#,##0.00}")});
            this.lblMultaDocto.Dpi = 254F;
            this.lblMultaDocto.Name = "lblMultaDocto";
            this.lblMultaDocto.StylePriority.UseTextAlignment = false;
            this.lblMultaDocto.Text = "lblMultaDocto";
            this.lblMultaDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblMultaDocto.Weight = 0.26002649021826224D;
            // 
            // lblDesctoDocto
            // 
            this.lblDesctoDocto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DescontoDesc", "{0:#,##0.00}")});
            this.lblDesctoDocto.Dpi = 254F;
            this.lblDesctoDocto.Name = "lblDesctoDocto";
            this.lblDesctoDocto.StylePriority.UseTextAlignment = false;
            this.lblDesctoDocto.Text = "lblDesctoDocto";
            this.lblDesctoDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblDesctoDocto.Weight = 0.26560311267601783D;
            // 
            // lblValorTotDocto
            // 
            this.lblValorTotDocto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalDesc", "{0:#,##0.00}")});
            this.lblValorTotDocto.Dpi = 254F;
            this.lblValorTotDocto.Name = "lblValorTotDocto";
            this.lblValorTotDocto.StylePriority.UseTextAlignment = false;
            this.lblValorTotDocto.Text = "lblValorTotDocto";
            this.lblValorTotDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblValorTotDocto.Weight = 0.32181372703258648D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotParcMulta,
            this.lblTotParcJuros,
            this.lblTotParcTot,
            this.lblTotParcDescto,
            this.xrLine1,
            this.xrLabel2,
            this.lblTotParcPago,
            this.lblTotParcDocto});
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 95.25F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportFooter_BeforePrint);
            // 
            // lblTotParcMulta
            // 
            this.lblTotParcMulta.Dpi = 254F;
            this.lblTotParcMulta.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcMulta.LocationFloat = new DevExpress.Utils.PointFloat(1360.23F, 7.573112F);
            this.lblTotParcMulta.Name = "lblTotParcMulta";
            this.lblTotParcMulta.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotParcMulta.SizeF = new System.Drawing.SizeF(170F, 41.23569F);
            this.lblTotParcMulta.StylePriority.UseFont = false;
            this.lblTotParcMulta.StylePriority.UsePadding = false;
            this.lblTotParcMulta.StylePriority.UseTextAlignment = false;
            this.lblTotParcMulta.Text = "Total:";
            this.lblTotParcMulta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotParcJuros
            // 
            this.lblTotParcJuros.Dpi = 254F;
            this.lblTotParcJuros.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcJuros.LocationFloat = new DevExpress.Utils.PointFloat(1186.583F, 7.573112F);
            this.lblTotParcJuros.Name = "lblTotParcJuros";
            this.lblTotParcJuros.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotParcJuros.SizeF = new System.Drawing.SizeF(173.5204F, 41.23569F);
            this.lblTotParcJuros.StylePriority.UseFont = false;
            this.lblTotParcJuros.StylePriority.UsePadding = false;
            this.lblTotParcJuros.StylePriority.UseTextAlignment = false;
            this.lblTotParcJuros.Text = "Total:";
            this.lblTotParcJuros.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotParcTot
            // 
            this.lblTotParcTot.Dpi = 254F;
            this.lblTotParcTot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcTot.LocationFloat = new DevExpress.Utils.PointFloat(1697.875F, 7.573242F);
            this.lblTotParcTot.Name = "lblTotParcTot";
            this.lblTotParcTot.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotParcTot.SizeF = new System.Drawing.SizeF(196.188F, 41.23569F);
            this.lblTotParcTot.StylePriority.UseFont = false;
            this.lblTotParcTot.StylePriority.UsePadding = false;
            this.lblTotParcTot.StylePriority.UseTextAlignment = false;
            this.lblTotParcTot.Text = "Total:";
            this.lblTotParcTot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotParcDescto
            // 
            this.lblTotParcDescto.Dpi = 254F;
            this.lblTotParcDescto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcDescto.LocationFloat = new DevExpress.Utils.PointFloat(1530.23F, 7.573112F);
            this.lblTotParcDescto.Name = "lblTotParcDescto";
            this.lblTotParcDescto.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotParcDescto.SizeF = new System.Drawing.SizeF(167.6454F, 41.23569F);
            this.lblTotParcDescto.StylePriority.UseFont = false;
            this.lblTotParcDescto.StylePriority.UsePadding = false;
            this.lblTotParcDescto.StylePriority.UseTextAlignment = false;
            this.lblTotParcDescto.Text = "Total:";
            this.lblTotParcDescto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1.191071F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(1901F, 6F);
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.6463995F, 7.191099F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(235.2078F, 41.23569F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Total Geral:";
            // 
            // lblTotParcPago
            // 
            this.lblTotParcPago.Dpi = 254F;
            this.lblTotParcPago.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcPago.LocationFloat = new DevExpress.Utils.PointFloat(940.9586F, 7.191025F);
            this.lblTotParcPago.Name = "lblTotParcPago";
            this.lblTotParcPago.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotParcPago.SizeF = new System.Drawing.SizeF(191.8748F, 41.23569F);
            this.lblTotParcPago.StylePriority.UseFont = false;
            this.lblTotParcPago.StylePriority.UsePadding = false;
            this.lblTotParcPago.StylePriority.UseTextAlignment = false;
            this.lblTotParcPago.Text = "Total:";
            this.lblTotParcPago.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotParcDocto
            // 
            this.lblTotParcDocto.Dpi = 254F;
            this.lblTotParcDocto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcDocto.LocationFloat = new DevExpress.Utils.PointFloat(573.7709F, 7.573112F);
            this.lblTotParcDocto.Name = "lblTotParcDocto";
            this.lblTotParcDocto.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotParcDocto.SizeF = new System.Drawing.SizeF(264F, 41.23569F);
            this.lblTotParcDocto.StylePriority.UseFont = false;
            this.lblTotParcDocto.StylePriority.UsePadding = false;
            this.lblTotParcDocto.StylePriority.UseTextAlignment = false;
            this.lblTotParcDocto.Text = "Total:";
            this.lblTotParcDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblvmul,
            this.lblvjur,
            this.lblvtot,
            this.lblvdes,
            this.xrLine2,
            this.xrLabel1,
            this.lblvpag,
            this.lblvp});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 95.25F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter1_BeforePrint);
            // 
            // lblvmul
            // 
            this.lblvmul.Dpi = 254F;
            this.lblvmul.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblvmul.LocationFloat = new DevExpress.Utils.PointFloat(1360.23F, 8.573112F);
            this.lblvmul.Name = "lblvmul";
            this.lblvmul.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblvmul.SizeF = new System.Drawing.SizeF(170F, 41.23569F);
            this.lblvmul.StylePriority.UseFont = false;
            this.lblvmul.StylePriority.UsePadding = false;
            this.lblvmul.StylePriority.UseTextAlignment = false;
            this.lblvmul.Text = "Total:";
            this.lblvmul.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblvjur
            // 
            this.lblvjur.Dpi = 254F;
            this.lblvjur.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblvjur.LocationFloat = new DevExpress.Utils.PointFloat(1186.583F, 8.573112F);
            this.lblvjur.Name = "lblvjur";
            this.lblvjur.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblvjur.SizeF = new System.Drawing.SizeF(173.5204F, 41.23569F);
            this.lblvjur.StylePriority.UseFont = false;
            this.lblvjur.StylePriority.UsePadding = false;
            this.lblvjur.StylePriority.UseTextAlignment = false;
            this.lblvjur.Text = "Total:";
            this.lblvjur.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblvtot
            // 
            this.lblvtot.Dpi = 254F;
            this.lblvtot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblvtot.LocationFloat = new DevExpress.Utils.PointFloat(1701.875F, 8.573242F);
            this.lblvtot.Name = "lblvtot";
            this.lblvtot.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblvtot.SizeF = new System.Drawing.SizeF(192.188F, 41.23569F);
            this.lblvtot.StylePriority.UseFont = false;
            this.lblvtot.StylePriority.UsePadding = false;
            this.lblvtot.StylePriority.UseTextAlignment = false;
            this.lblvtot.Text = "Total:";
            this.lblvtot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblvdes
            // 
            this.lblvdes.Dpi = 254F;
            this.lblvdes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblvdes.LocationFloat = new DevExpress.Utils.PointFloat(1530.23F, 8.573112F);
            this.lblvdes.Name = "lblvdes";
            this.lblvdes.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblvdes.SizeF = new System.Drawing.SizeF(167.6454F, 41.23569F);
            this.lblvdes.StylePriority.UseFont = false;
            this.lblvdes.StylePriority.UsePadding = false;
            this.lblvdes.StylePriority.UseTextAlignment = false;
            this.lblvdes.Text = "Total:";
            this.lblvdes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLine2
            // 
            this.xrLine2.Dpi = 254F;
            this.xrLine2.LineWidth = 3;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(1F, 2.191071F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(1900F, 6F);
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.6463995F, 8.191101F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(235.2078F, 41.23569F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Total:";
            // 
            // lblvpag
            // 
            this.lblvpag.Dpi = 254F;
            this.lblvpag.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblvpag.LocationFloat = new DevExpress.Utils.PointFloat(940.9586F, 8.191025F);
            this.lblvpag.Name = "lblvpag";
            this.lblvpag.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblvpag.SizeF = new System.Drawing.SizeF(191.8748F, 41.23569F);
            this.lblvpag.StylePriority.UseFont = false;
            this.lblvpag.StylePriority.UsePadding = false;
            this.lblvpag.StylePriority.UseTextAlignment = false;
            this.lblvpag.Text = "Total:";
            this.lblvpag.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblvp
            // 
            this.lblvp.Dpi = 254F;
            this.lblvp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblvp.LocationFloat = new DevExpress.Utils.PointFloat(573.7709F, 8.573112F);
            this.lblvp.Name = "lblvp";
            this.lblvp.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblvp.SizeF = new System.Drawing.SizeF(264F, 41.23569F);
            this.lblvp.StylePriority.UseFont = false;
            this.lblvp.StylePriority.UsePadding = false;
            this.lblvp.StylePriority.UseTextAlignment = false;
            this.lblvp.Text = "Total:";
            this.lblvp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // RptHistorFinancFornecedor
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport});
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell lblValorDocto;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell lblValorPago;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        private DevExpress.XtraReports.UI.XRTableCell lblJurosDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblMultaDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblDesctoDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblValorTotDocto;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcMulta;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcJuros;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcTot;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcDescto;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcPago;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcDocto;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLabel lblvmul;
        private DevExpress.XtraReports.UI.XRLabel lblvjur;
        private DevExpress.XtraReports.UI.XRLabel lblvtot;
        private DevExpress.XtraReports.UI.XRLabel lblvdes;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lblvpag;
        private DevExpress.XtraReports.UI.XRLabel lblvp;
    }
}
