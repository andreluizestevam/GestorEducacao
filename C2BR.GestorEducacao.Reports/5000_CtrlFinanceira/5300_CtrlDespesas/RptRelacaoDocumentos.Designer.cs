namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas
{
    partial class RptRelacaoDocumentos
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
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNome = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTM = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblJurosDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblMultaDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDesctoDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblValorTotDocto = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblTotGeralMulta = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotDesctoGeral = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotGeralTot = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotGeralJuros = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
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
            this.lblTitulo.Text = "POSIÇÃO DE TÍTULOS DE DESPESAS/COMPROMISSOS DE PAGAMENTO - GERAL";
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
            this.bsReport.DataSource = typeof(C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas.RptRelacaoDocumentos.RelTitulosPagar);
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeader1,
            this.ReportFooter});
            this.DetailReport.Controls.SetChildIndex(this.ReportFooter, 0);
            this.DetailReport.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 49.67027F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // xrTable1
            // 
            this.xrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrTable1.BackColor = System.Drawing.Color.Gray;
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable1.ForeColor = System.Drawing.Color.White;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(8F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(2759F, 49.67027F);
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
            this.xrTableCell2,
            this.xrTableCell9,
            this.xrTableCell1,
            this.xrTableCell3,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell8,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell4});
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
            this.xrTableCell7.Text = "DT REF";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.18156944972527433D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.CanGrow = false;
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = " DIAS";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 0.10381227645241418D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.CanGrow = false;
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = " CNPJ / CPF";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell9.Weight = 0.31919220709511303D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = " NºDOC / PA";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.27132242910918514D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.CanGrow = false;
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "VALOR DOC";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.24547129873420148D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.CanGrow = false;
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "JUROS";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell6.Weight = 0.1907988300019344D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.CanGrow = false;
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = " MULTA";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.187918749232294D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.CanGrow = false;
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = " DESCONT";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell8.Weight = 0.1911163790510014D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.CanGrow = false;
            this.xrTableCell12.Dpi = 254F;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "R$ TOTAL";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 0.23259109255925237D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.CanGrow = false;
            this.xrTableCell13.Dpi = 254F;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(12, 0, 0, 0, 254F);
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "HISTÓRICO";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell13.Weight = 0.66753537557581033D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.CanGrow = false;
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "STATUS";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.41174510315217877D;
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
            this.xrTable2.SizeF = new System.Drawing.SizeF(2767F, 49.67026F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell14,
            this.lblNome,
            this.lblTM,
            this.lblValorDocto,
            this.lblJurosDocto,
            this.lblMultaDocto,
            this.lblDesctoDocto,
            this.lblValorTotDocto,
            this.xrTableCell11,
            this.xrTableCell15});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Data", "{0:dd/MM/yy}")});
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.Weight = 0.15739567758878867D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Dias")});
            this.xrTableCell14.Dpi = 254F;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.Weight = 0.085872643747546912D;
            this.xrTableCell14.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell14_BeforePrint);
            // 
            // lblNome
            // 
            this.lblNome.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CNPJCPFDesc")});
            this.lblNome.Dpi = 254F;
            this.lblNome.Name = "lblNome";
            this.lblNome.StylePriority.UseTextAlignment = false;
            this.lblNome.Text = "lblNome";
            this.lblNome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblNome.Weight = 0.26403313851911087D;
            // 
            // lblTM
            // 
            this.lblTM.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DocParc", "{0:00}")});
            this.lblTM.Dpi = 254F;
            this.lblTM.Name = "lblTM";
            this.lblTM.StylePriority.UseTextAlignment = false;
            this.lblTM.Text = "lblTM";
            this.lblTM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblTM.Weight = 0.22443567579626239D;
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
            this.lblValorDocto.Weight = 0.203051809956187D;
            this.lblValorDocto.AfterPrint += new System.EventHandler(this.lblValorDocto_AfterPrint);
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
            this.lblJurosDocto.Weight = 0.15782707630349876D;
            this.lblJurosDocto.AfterPrint += new System.EventHandler(this.lblJurosDocto_AfterPrint);
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
            this.lblMultaDocto.Weight = 0.15544492750721875D;
            this.lblMultaDocto.AfterPrint += new System.EventHandler(this.lblMultaDocto_AfterPrint);
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
            this.lblDesctoDocto.Weight = 0.15808987881060063D;
            this.lblDesctoDocto.AfterPrint += new System.EventHandler(this.lblDesctoDocto_AfterPrint);
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
            this.lblValorTotDocto.Weight = 0.19239742333606702D;
            this.lblValorTotDocto.AfterPrint += new System.EventHandler(this.lblValorTotDocto_AfterPrint);
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Historico")});
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(13, 0, 0, 0, 254F);
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.Text = "xrTableCell11";
            this.xrTableCell11.Weight = 0.55217956008273716D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusDesc")});
            this.xrTableCell15.Dpi = 254F;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "xrTableCell15";
            this.xrTableCell15.Weight = 0.34059221527523553D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotGeralMulta,
            this.lblTotDesctoGeral,
            this.lblTotGeralTot,
            this.lblTotGeralJuros,
            this.xrLabel2,
            this.xrLine1,
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
            this.lblTotGeralMulta.LocationFloat = new DevExpress.Utils.PointFloat(1216.167F, 5.999957F);
            this.lblTotGeralMulta.Name = "lblTotGeralMulta";
            this.lblTotGeralMulta.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotGeralMulta.SizeF = new System.Drawing.SizeF(170F, 41.23569F);
            this.lblTotGeralMulta.StylePriority.UseFont = false;
            this.lblTotGeralMulta.StylePriority.UsePadding = false;
            this.lblTotGeralMulta.StylePriority.UseTextAlignment = false;
            this.lblTotGeralMulta.Text = "Total:";
            this.lblTotGeralMulta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotDesctoGeral
            // 
            this.lblTotDesctoGeral.Dpi = 254F;
            this.lblTotDesctoGeral.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotDesctoGeral.LocationFloat = new DevExpress.Utils.PointFloat(1396.75F, 5.882231F);
            this.lblTotDesctoGeral.Name = "lblTotDesctoGeral";
            this.lblTotDesctoGeral.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotDesctoGeral.SizeF = new System.Drawing.SizeF(165F, 41.23569F);
            this.lblTotDesctoGeral.StylePriority.UseFont = false;
            this.lblTotDesctoGeral.StylePriority.UsePadding = false;
            this.lblTotDesctoGeral.StylePriority.UseTextAlignment = false;
            this.lblTotDesctoGeral.Text = "Total:";
            this.lblTotDesctoGeral.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotGeralTot
            // 
            this.lblTotGeralTot.Dpi = 254F;
            this.lblTotGeralTot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralTot.LocationFloat = new DevExpress.Utils.PointFloat(1561.75F, 5.88207F);
            this.lblTotGeralTot.Name = "lblTotGeralTot";
            this.lblTotGeralTot.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotGeralTot.SizeF = new System.Drawing.SizeF(213.6875F, 41.23569F);
            this.lblTotGeralTot.StylePriority.UseFont = false;
            this.lblTotGeralTot.StylePriority.UsePadding = false;
            this.lblTotGeralTot.StylePriority.UseTextAlignment = false;
            this.lblTotGeralTot.Text = "Total:";
            this.lblTotGeralTot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblTotGeralJuros
            // 
            this.lblTotGeralJuros.Dpi = 254F;
            this.lblTotGeralJuros.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralJuros.LocationFloat = new DevExpress.Utils.PointFloat(1043.521F, 5.88207F);
            this.lblTotGeralJuros.Name = "lblTotGeralJuros";
            this.lblTotGeralJuros.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotGeralJuros.SizeF = new System.Drawing.SizeF(170F, 41.23569F);
            this.lblTotGeralJuros.StylePriority.UseFont = false;
            this.lblTotGeralJuros.StylePriority.UsePadding = false;
            this.lblTotGeralJuros.StylePriority.UseTextAlignment = false;
            this.lblTotGeralJuros.Text = "Total:";
            this.lblTotGeralJuros.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.0002422333F, 5.882231F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(203.5413F, 41.23569F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Total Geral:";
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0.0002441406F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(2767F, 5F);
            // 
            // lblTotGeralDocto
            // 
            this.lblTotGeralDocto.Dpi = 254F;
            this.lblTotGeralDocto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotGeralDocto.LocationFloat = new DevExpress.Utils.PointFloat(786.4791F, 5.999957F);
            this.lblTotGeralDocto.Name = "lblTotGeralDocto";
            this.lblTotGeralDocto.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.lblTotGeralDocto.SizeF = new System.Drawing.SizeF(251.7499F, 41.23569F);
            this.lblTotGeralDocto.StylePriority.UseFont = false;
            this.lblTotGeralDocto.StylePriority.UsePadding = false;
            this.lblTotGeralDocto.StylePriority.UseTextAlignment = false;
            this.lblTotGeralDocto.Text = "Total:";
            this.lblTotGeralDocto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // RptRelacaoDocumentos
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
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell lblNome;
        private DevExpress.XtraReports.UI.XRTableCell lblTM;
        private DevExpress.XtraReports.UI.XRTableCell lblValorDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblJurosDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblMultaDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblDesctoDocto;
        private DevExpress.XtraReports.UI.XRTableCell lblValorTotDocto;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralMulta;
        private DevExpress.XtraReports.UI.XRLabel lblTotDesctoGeral;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralTot;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralJuros;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel lblTotGeralDocto;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
    }
}
