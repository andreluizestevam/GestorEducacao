namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas
{
    partial class RptRelacaoTituloFornecDespesas
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
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblSexo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDeficiencia = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblJurosDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblMultaDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDesctoDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorTotDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.lblTotParcMulta = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcDescto = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcTot = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcJuros = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotParcDocto = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblTotGeralMulta = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotGeralDescto = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotGeralTot = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotGeralJuros = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotGeralDocto = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "POSIÇÃO DE TÍTULOS DE DESPESAS - DE FORNECEDORES";
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
            this.bsReport.DataSource = typeof(C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas.RptRelacaoTituloFornecDespesas.RelTitulosFornecedor);
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeader1,
            this.GroupFooter1,
            this.ReportFooter});
            this.DetailReport.Controls.SetChildIndex(this.ReportFooter, 0);
            this.DetailReport.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.DetailReport.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrLabel6,
            this.xrLabel7});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Nome", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 108.4792F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrTable1
            // 
            this.xrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrTable1.BackColor = System.Drawing.Color.Gray;
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable1.ForeColor = System.Drawing.Color.White;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 56.54166F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1901F, 49.67027F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell1,
            this.xrTableCell9,
            this.xrTableCell11,
            this.xrTableCell10,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell12});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.CanGrow = false;
            this.xrTableCell7.Dpi = 254F;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "DT MOV";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.21388243642685872D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Nº DOC";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.49394283213507251D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.CanGrow = false;
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "DT VENC";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell9.Weight = 0.23008868534990298D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.CanGrow = false;
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "DT PAG";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell11.Weight = 0.2091294465322508D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.CanGrow = false;
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "STATUS";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell10.Weight = 0.26299614527288251D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.CanGrow = false;
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "VALOR DOC";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.41825888466884542D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.CanGrow = false;
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "JUROS";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell4.Weight = 0.28992946513142315D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.CanGrow = false;
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "MULTA";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell2.Weight = 0.28517652141771521D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.CanGrow = false;
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "DESCTO";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.2865627961638631D;
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
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NomeDesc")});
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(201.0829F, 3.404427F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(1376.792F, 41.23571F);
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
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.404427F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(196.1458F, 41.23568F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Fornecedor:";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Dpi = 254F;
            this.xrTable2.EvenStyleName = "EvenStyle";
            this.xrTable2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.OddStyleName = "OddStyle";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1901F, 49.67026F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCell13,
            this.lblSexo,
            this.xrTableCell8,
            this.lblDeficiencia,
            this.lblValorDocto,
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
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataMov", "{0:dd/MM/yy}")});
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 0.21388237792086412D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Documento")});
            this.xrTableCell13.Dpi = 254F;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "xrTableCell13";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell13.Weight = 0.49394278050281515D;
            // 
            // lblSexo
            // 
            this.lblSexo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataVencimento", "{0:dd/MM/yy}")});
            this.lblSexo.Dpi = 254F;
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.lblSexo.StylePriority.UsePadding = false;
            this.lblSexo.StylePriority.UseTextAlignment = false;
            this.lblSexo.Text = "lblSexo";
            this.lblSexo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblSexo.Weight = 0.23008866201255043D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataPagamento", "{0:dd/MM/yy}")});
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell8.Weight = 0.20912943101653131D;
            // 
            // lblDeficiencia
            // 
            this.lblDeficiencia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusDesc")});
            this.lblDeficiencia.Dpi = 254F;
            this.lblDeficiencia.Name = "lblDeficiencia";
            this.lblDeficiencia.StylePriority.UseTextAlignment = false;
            this.lblDeficiencia.Text = "lblDeficiencia";
            this.lblDeficiencia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblDeficiencia.Weight = 0.26299613266583621D;
            // 
            // lblValorDocto
            // 
            this.lblValorDocto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValorDocumento", "{0:#,##0.00}")});
            this.lblValorDocto.Dpi = 254F;
            this.lblValorDocto.Name = "lblValorDocto";
            this.lblValorDocto.StylePriority.UseTextAlignment = false;
            this.lblValorDocto.Text = "lblValorDocto";
            this.lblValorDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblValorDocto.Weight = 0.41825886653735789D;
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
            this.lblJurosDocto.Weight = 0.28992945017468585D;
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
            this.lblMultaDocto.Weight = 0.2851765063193169D;
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
            this.lblDesctoDocto.Weight = 0.2865627809777942D;
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
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotParcMulta,
            this.lblTotParcDescto,
            this.lblTotParcTot,
            this.lblTotParcJuros,
            this.xrLine1,
            this.xrLabel2,
            this.lblTotParcDocto});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 63.5F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter1_BeforePrint);
            // 
            // lblTotParcMulta
            // 
            this.lblTotParcMulta.Dpi = 254F;
            this.lblTotParcMulta.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcMulta.LocationFloat = new DevExpress.Utils.PointFloat(1337F, 7.382088F);
            this.lblTotParcMulta.Name = "lblTotParcMulta";
            this.lblTotParcMulta.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotParcMulta.SizeF = new System.Drawing.SizeF(180F, 41.23569F);
            this.lblTotParcMulta.StylePriority.UseFont = false;
            this.lblTotParcMulta.StylePriority.UseTextAlignment = false;
            this.lblTotParcMulta.Text = "Total:";
            this.lblTotParcMulta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotParcDescto
            // 
            this.lblTotParcDescto.Dpi = 254F;
            this.lblTotParcDescto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcDescto.LocationFloat = new DevExpress.Utils.PointFloat(1517F, 7.382088F);
            this.lblTotParcDescto.Name = "lblTotParcDescto";
            this.lblTotParcDescto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotParcDescto.SizeF = new System.Drawing.SizeF(180.875F, 41.23569F);
            this.lblTotParcDescto.StylePriority.UseFont = false;
            this.lblTotParcDescto.StylePriority.UseTextAlignment = false;
            this.lblTotParcDescto.Text = "Total:";
            this.lblTotParcDescto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotParcTot
            // 
            this.lblTotParcTot.Dpi = 254F;
            this.lblTotParcTot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcTot.LocationFloat = new DevExpress.Utils.PointFloat(1697.875F, 7.382172F);
            this.lblTotParcTot.Name = "lblTotParcTot";
            this.lblTotParcTot.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotParcTot.SizeF = new System.Drawing.SizeF(203.12F, 41.23569F);
            this.lblTotParcTot.StylePriority.UseFont = false;
            this.lblTotParcTot.StylePriority.UseTextAlignment = false;
            this.lblTotParcTot.Text = "Total:";
            this.lblTotParcTot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotParcJuros
            // 
            this.lblTotParcJuros.Dpi = 254F;
            this.lblTotParcJuros.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcJuros.LocationFloat = new DevExpress.Utils.PointFloat(1154F, 7.382088F);
            this.lblTotParcJuros.Name = "lblTotParcJuros";
            this.lblTotParcJuros.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotParcJuros.SizeF = new System.Drawing.SizeF(183.0001F, 41.23569F);
            this.lblTotParcJuros.StylePriority.UseFont = false;
            this.lblTotParcJuros.StylePriority.UseTextAlignment = false;
            this.lblTotParcJuros.Text = "Total:";
            this.lblTotParcJuros.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(1901F, 6F);
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(474.5928F, 7.382088F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(174.3536F, 41.23569F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Total:";
            // 
            // lblTotParcDocto
            // 
            this.lblTotParcDocto.Dpi = 254F;
            this.lblTotParcDocto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotParcDocto.LocationFloat = new DevExpress.Utils.PointFloat(889.9999F, 7.382088F);
            this.lblTotParcDocto.Name = "lblTotParcDocto";
            this.lblTotParcDocto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotParcDocto.SizeF = new System.Drawing.SizeF(264F, 41.23569F);
            this.lblTotParcDocto.StylePriority.UseFont = false;
            this.lblTotParcDocto.StylePriority.UseTextAlignment = false;
            this.lblTotParcDocto.Text = "Total:";
            this.lblTotParcDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotGeralMulta,
            this.lblTotGeralDescto,
            this.lblTotGeralTot,
            this.lblTotGeralJuros,
            this.xrLine2,
            this.xrLabel1,
            this.lblTotGeralDocto});
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 95.25F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportFooter_BeforePrint);
            // 
            // lblTotGeralMulta
            // 
            this.lblTotGeralMulta.Dpi = 254F;
            this.lblTotGeralMulta.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralMulta.LocationFloat = new DevExpress.Utils.PointFloat(1335.5F, 7.191025F);
            this.lblTotGeralMulta.Name = "lblTotGeralMulta";
            this.lblTotGeralMulta.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotGeralMulta.SizeF = new System.Drawing.SizeF(180F, 41.23569F);
            this.lblTotGeralMulta.StylePriority.UseFont = false;
            this.lblTotGeralMulta.StylePriority.UseTextAlignment = false;
            this.lblTotGeralMulta.Text = "Total:";
            this.lblTotGeralMulta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotGeralDescto
            // 
            this.lblTotGeralDescto.Dpi = 254F;
            this.lblTotGeralDescto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralDescto.LocationFloat = new DevExpress.Utils.PointFloat(1516.495F, 7.191025F);
            this.lblTotGeralDescto.Name = "lblTotGeralDescto";
            this.lblTotGeralDescto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotGeralDescto.SizeF = new System.Drawing.SizeF(180.88F, 41.23569F);
            this.lblTotGeralDescto.StylePriority.UseFont = false;
            this.lblTotGeralDescto.StylePriority.UseTextAlignment = false;
            this.lblTotGeralDescto.Text = "Total:";
            this.lblTotGeralDescto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotGeralTot
            // 
            this.lblTotGeralTot.Dpi = 254F;
            this.lblTotGeralTot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralTot.LocationFloat = new DevExpress.Utils.PointFloat(1697.38F, 7.573273F);
            this.lblTotGeralTot.Name = "lblTotGeralTot";
            this.lblTotGeralTot.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotGeralTot.SizeF = new System.Drawing.SizeF(203.12F, 41.23569F);
            this.lblTotGeralTot.StylePriority.UseFont = false;
            this.lblTotGeralTot.StylePriority.UseTextAlignment = false;
            this.lblTotGeralTot.Text = "Total:";
            this.lblTotGeralTot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotGeralJuros
            // 
            this.lblTotGeralJuros.Dpi = 254F;
            this.lblTotGeralJuros.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralJuros.LocationFloat = new DevExpress.Utils.PointFloat(1152.5F, 7.191025F);
            this.lblTotGeralJuros.Name = "lblTotGeralJuros";
            this.lblTotGeralJuros.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotGeralJuros.SizeF = new System.Drawing.SizeF(183F, 41.23569F);
            this.lblTotGeralJuros.StylePriority.UseFont = false;
            this.lblTotGeralJuros.StylePriority.UseTextAlignment = false;
            this.lblTotGeralJuros.Text = "Total:";
            this.lblTotGeralJuros.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLine2
            // 
            this.xrLine2.Dpi = 254F;
            this.xrLine2.LineWidth = 3;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(1901F, 6F);
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(474.0928F, 7.573273F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(192.8744F, 41.23569F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Total Geral:";
            // 
            // lblTotGeralDocto
            // 
            this.lblTotGeralDocto.Dpi = 254F;
            this.lblTotGeralDocto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralDocto.LocationFloat = new DevExpress.Utils.PointFloat(888.5002F, 7.191025F);
            this.lblTotGeralDocto.Name = "lblTotGeralDocto";
            this.lblTotGeralDocto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotGeralDocto.SizeF = new System.Drawing.SizeF(264F, 41.23569F);
            this.lblTotGeralDocto.StylePriority.UseFont = false;
            this.lblTotGeralDocto.StylePriority.UseTextAlignment = false;
            this.lblTotGeralDocto.Text = "Total:";
            this.lblTotGeralDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // RptRelacaoTituloFornecDespesas
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
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell lblSexo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell lblDeficiencia;
        private DevExpress.XtraReports.UI.XRTableCell lblValorDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblJurosDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblMultaDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblDesctoDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblValorTotDocto;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcMulta;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcDescto;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcTot;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcJuros;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel lblTotParcDocto;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralMulta;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralDescto;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralTot;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralJuros;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralDocto;
    }
}
