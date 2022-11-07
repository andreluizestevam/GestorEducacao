namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5700_CtrlDesepenhoFinanceiro
{
    partial class RptCurvaABCDespesaComprom
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
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblTotValorTitulos = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotTitulos = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblNumSeq = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblSexo = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDeficiencia = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorTotalTit = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "CURVA ABC - DESPESAS / COMPROMISSO";
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
            // 
            // bsReport
            // 
            this.bsReport.DataSource = typeof(C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5700_CtrlDesepenhoFinanceiro.RptCurvaABCDespesaComprom.TitulosFornecedor);
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.ReportFooter,
            this.GroupHeader1});
            this.DetailReport.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.DetailReport.Controls.SetChildIndex(this.ReportFooter, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotValorTitulos,
            this.lblTotTitulos,
            this.xrLabel1,
            this.xrLine1});
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 66.14584F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportFooter_BeforePrint);
            // 
            // lblTotValorTitulos
            // 
            this.lblTotValorTitulos.Dpi = 254F;
            this.lblTotValorTitulos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotValorTitulos.LocationFloat = new DevExpress.Utils.PointFloat(1562F, 5.000018F);
            this.lblTotValorTitulos.Name = "lblTotValorTitulos";
            this.lblTotValorTitulos.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotValorTitulos.SizeF = new System.Drawing.SizeF(335.9999F, 41.23569F);
            this.lblTotValorTitulos.StylePriority.UseFont = false;
            this.lblTotValorTitulos.StylePriority.UseTextAlignment = false;
            this.lblTotValorTitulos.Text = "Total:";
            this.lblTotValorTitulos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotTitulos
            // 
            this.lblTotTitulos.Dpi = 254F;
            this.lblTotTitulos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotTitulos.LocationFloat = new DevExpress.Utils.PointFloat(1330F, 5.000018F);
            this.lblTotTitulos.Name = "lblTotTitulos";
            this.lblTotTitulos.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTotTitulos.SizeF = new System.Drawing.SizeF(232.0001F, 41.23569F);
            this.lblTotTitulos.StylePriority.UseFont = false;
            this.lblTotTitulos.StylePriority.UseTextAlignment = false;
            this.lblTotTitulos.Text = "Total:";
            this.lblTotTitulos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1036.313F, 5.000018F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(100.2703F, 41.23569F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Total:";
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(1898F, 5F);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 49.67028F;
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
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1898F, 49.67028F);
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
            this.xrTableCell6});
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
            this.xrTableCell7.Text = "SEQ.";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.15074773173691691D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "FORNECEDOR";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 1.9597205474865207D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.CanGrow = false;
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "QTDE TITULOS";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell9.Weight = 0.3681418238865744D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.CanGrow = false;
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "VALOR TOTAL";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell6.Weight = 0.53317080252084037D;
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
            this.xrTable2.SizeF = new System.Drawing.SizeF(1898F, 49.67026F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblNumSeq,
            this.lblSexo,
            this.lblDeficiencia,
            this.lblValorTotalTit});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow2.Weight = 1D;
            // 
            // lblNumSeq
            // 
            this.lblNumSeq.Dpi = 254F;
            this.lblNumSeq.Multiline = true;
            this.lblNumSeq.Name = "lblNumSeq";
            this.lblNumSeq.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.lblNumSeq.StylePriority.UsePadding = false;
            this.lblNumSeq.StylePriority.UseTextAlignment = false;
            this.lblNumSeq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblNumSeq.Weight = 0.15074771809179866D;
            this.lblNumSeq.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblNumSeq_BeforePrint);
            // 
            // lblSexo
            // 
            this.lblSexo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Fornecedor")});
            this.lblSexo.Dpi = 254F;
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblSexo.StylePriority.UsePadding = false;
            this.lblSexo.StylePriority.UseTextAlignment = false;
            this.lblSexo.Text = "lblSexo";
            this.lblSexo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblSexo.Weight = 1.9597203302395743D;
            // 
            // lblDeficiencia
            // 
            this.lblDeficiencia.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NumeroTitulos")});
            this.lblDeficiencia.Dpi = 254F;
            this.lblDeficiencia.Name = "lblDeficiencia";
            this.lblDeficiencia.StylePriority.UseTextAlignment = false;
            this.lblDeficiencia.Text = "lblDeficiencia";
            this.lblDeficiencia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblDeficiencia.Weight = 0.36814179168985628D;
            this.lblDeficiencia.AfterPrint += new System.EventHandler(this.lblDeficiencia_AfterPrint);
            // 
            // lblValorTotalTit
            // 
            this.lblValorTotalTit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalValorTitulo", "{0:#,##0.00}")});
            this.lblValorTotalTit.Dpi = 254F;
            this.lblValorTotalTit.Name = "lblValorTotalTit";
            this.lblValorTotalTit.StylePriority.UseTextAlignment = false;
            this.lblValorTotalTit.Text = "lblValorTotalTit";
            this.lblValorTotalTit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblValorTotalTit.Weight = 0.53317087513910955D;
            this.lblValorTotalTit.AfterPrint += new System.EventHandler(this.lblValorTotalTit_AfterPrint);
            // 
            // RptCurvaABCDespesaComprom
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

        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel lblTotValorTitulos;
        private DevExpress.XtraReports.UI.XRLabel lblTotTitulos;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell lblNumSeq;
        private DevExpress.XtraReports.UI.XRTableCell lblSexo;
        private DevExpress.XtraReports.UI.XRTableCell lblDeficiencia;
        private DevExpress.XtraReports.UI.XRTableCell lblValorTotalTit;
    }
}
