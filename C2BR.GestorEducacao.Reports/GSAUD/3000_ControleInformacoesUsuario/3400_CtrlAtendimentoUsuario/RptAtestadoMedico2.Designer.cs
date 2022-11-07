namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    partial class RptAtestadoMedico2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptAtestadoMedico2));
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.lblInfosDia = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCRM = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDoutor = new DevExpress.XtraReports.UI.XRLabel();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrRichText4 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrRichText3 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
            this.Visibilidade = new DevExpress.XtraReports.UI.FormattingRule();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            // 
            // lblParametros
            // 
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1});
            this.DetailContent.HeightF = 592.6667F;
            // 
            // bsReport
            // 
            this.bsReport.DataSource = typeof(C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario.RptAtestadoMedico2.Atestado);
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupFooter1,
            this.ReportFooter});
            this.DetailReport.Controls.SetChildIndex(this.ReportFooter, 0);
            this.DetailReport.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.lblInfosDia,
            this.lblCRM,
            this.lblDoutor});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 439.4205F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(509.9689F, 227.3301F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(881.0625F, 5.291672F);
            // 
            // lblInfosDia
            // 
            this.lblInfosDia.Dpi = 254F;
            this.lblInfosDia.LocationFloat = new DevExpress.Utils.PointFloat(128.7585F, 0F);
            this.lblInfosDia.Name = "lblInfosDia";
            this.lblInfosDia.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInfosDia.SizeF = new System.Drawing.SizeF(1647.483F, 45.19089F);
            this.lblInfosDia.StylePriority.UseTextAlignment = false;
            this.lblInfosDia.Text = "lblInfosDia";
            this.lblInfosDia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCRM
            // 
            this.lblCRM.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ENT_CONCAT")});
            this.lblCRM.Dpi = 254F;
            this.lblCRM.LocationFloat = new DevExpress.Utils.PointFloat(637.1771F, 283.1046F);
            this.lblCRM.Name = "lblCRM";
            this.lblCRM.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCRM.SizeF = new System.Drawing.SizeF(633.9211F, 45.19086F);
            this.lblCRM.StylePriority.UseTextAlignment = false;
            this.lblCRM.Text = "lblCRM";
            this.lblCRM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDoutor
            // 
            this.lblDoutor.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MedicoValid")});
            this.lblDoutor.Dpi = 254F;
            this.lblDoutor.LocationFloat = new DevExpress.Utils.PointFloat(509.9689F, 232.622F);
            this.lblDoutor.Name = "lblDoutor";
            this.lblDoutor.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDoutor.SizeF = new System.Drawing.SizeF(881.0625F, 50.48248F);
            this.lblDoutor.StylePriority.UseTextAlignment = false;
            this.lblDoutor.Text = "lblDoutor";
            this.lblDoutor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrRichText1
            // 
            this.xrRichText1.Dpi = 254F;
            this.xrRichText1.Font = new System.Drawing.Font("Arial", 9F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(254F, 66.14584F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(1394.675F, 373.274F);
            this.xrRichText1.StylePriority.UseFont = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText4,
            this.xrRichText3,
            this.xrRichText2});
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.FormattingRules.Add(this.Visibilidade);
            this.ReportFooter.HeightF = 733.1074F;
            this.ReportFooter.KeepTogether = true;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrRichText4
            // 
            this.xrRichText4.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrRichText4.Dpi = 254F;
            this.xrRichText4.Font = new System.Drawing.Font("Arial", 9F);
            this.xrRichText4.ForeColor = System.Drawing.Color.Black;
            this.xrRichText4.LocationFloat = new DevExpress.Utils.PointFloat(509.9689F, 602.3959F);
            this.xrRichText4.Name = "xrRichText4";
            this.xrRichText4.SerializableRtfString = resources.GetString("xrRichText4.SerializableRtfString");
            this.xrRichText4.SizeF = new System.Drawing.SizeF(881.0625F, 63.71155F);
            this.xrRichText4.StylePriority.UseBorders = false;
            this.xrRichText4.StylePriority.UseFont = false;
            this.xrRichText4.StylePriority.UseForeColor = false;
            // 
            // xrRichText3
            // 
            this.xrRichText3.Dpi = 254F;
            this.xrRichText3.Font = new System.Drawing.Font("Arial", 9F);
            this.xrRichText3.LocationFloat = new DevExpress.Utils.PointFloat(254F, 59F);
            this.xrRichText3.Name = "xrRichText3";
            this.xrRichText3.SerializableRtfString = resources.GetString("xrRichText3.SerializableRtfString");
            this.xrRichText3.SizeF = new System.Drawing.SizeF(1394.675F, 63.71156F);
            this.xrRichText3.StylePriority.UseFont = false;
            // 
            // xrRichText2
            // 
            this.xrRichText2.Dpi = 254F;
            this.xrRichText2.Font = new System.Drawing.Font("Arial", 9F);
            this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(254F, 254F);
            this.xrRichText2.Name = "xrRichText2";
            this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
            this.xrRichText2.SizeF = new System.Drawing.SizeF(1394.675F, 262.149F);
            this.xrRichText2.StylePriority.UseFont = false;
            // 
            // Visibilidade
            // 
            this.Visibilidade.Condition = "[ImpCid]  == False";
            this.Visibilidade.DataMember = null;
            this.Visibilidade.DataSource = this.bsReport;
            // 
            // 
            // 
            this.Visibilidade.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.Visibilidade.Name = "Visibilidade";
            // 
            // RptAtestadoMedico2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport});
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.Visibilidade});
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel lblInfosDia;
        private DevExpress.XtraReports.UI.XRLabel lblCRM;
        private DevExpress.XtraReports.UI.XRLabel lblDoutor;
        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRRichText xrRichText4;
        private DevExpress.XtraReports.UI.XRRichText xrRichText3;
        private DevExpress.XtraReports.UI.XRRichText xrRichText2;
        private DevExpress.XtraReports.UI.FormattingRule Visibilidade;
    }
}
