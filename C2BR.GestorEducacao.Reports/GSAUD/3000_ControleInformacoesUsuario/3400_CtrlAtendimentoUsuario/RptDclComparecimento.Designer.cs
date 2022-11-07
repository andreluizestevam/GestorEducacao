namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    partial class RptDclComparecimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptDclComparecimento));
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.lblCRM = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDoutor = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.lblInfosDia = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
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
            this.DetailContent.HeightF = 448.545F;
            // 
            // bsReport
            // 
            this.bsReport.DataSource = typeof(C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario.RptDclComparecimento.Atestado);
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupFooter1});
            this.DetailReport.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.DetailReport.Controls.SetChildIndex(this.DetailContent, 0);
            // 
            // xrRichText1
            // 
            this.xrRichText1.Dpi = 254F;
            this.xrRichText1.Font = new System.Drawing.Font("Arial", 10F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(252.3534F, 92.60416F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(1397.321F, 158.9616F);
            this.xrRichText1.StylePriority.UseFont = false;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCRM,
            this.lblDoutor,
            this.xrLine1,
            this.lblInfosDia});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 465.8789F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // lblCRM
            // 
            this.lblCRM.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ENT_CONCAT")});
            this.lblCRM.Dpi = 254F;
            this.lblCRM.LocationFloat = new DevExpress.Utils.PointFloat(637.1771F, 351.8963F);
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
            this.lblDoutor.LocationFloat = new DevExpress.Utils.PointFloat(509.9689F, 301.4137F);
            this.lblDoutor.Name = "lblDoutor";
            this.lblDoutor.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDoutor.SizeF = new System.Drawing.SizeF(881.0625F, 50.48248F);
            this.lblDoutor.StylePriority.UseTextAlignment = false;
            this.lblDoutor.Text = "lblDoutor";
            this.lblDoutor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(509.9689F, 296.1218F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(881.0625F, 5.291672F);
            // 
            // lblInfosDia
            // 
            this.lblInfosDia.Dpi = 254F;
            this.lblInfosDia.LocationFloat = new DevExpress.Utils.PointFloat(128.7585F, 68.79172F);
            this.lblInfosDia.Name = "lblInfosDia";
            this.lblInfosDia.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInfosDia.SizeF = new System.Drawing.SizeF(1647.483F, 45.19089F);
            this.lblInfosDia.StylePriority.UseTextAlignment = false;
            this.lblInfosDia.Text = "lblInfosDia";
            this.lblInfosDia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // RptDclComparecimento
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport});
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLabel lblInfosDia;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel lblDoutor;
        private DevExpress.XtraReports.UI.XRLabel lblCRM;
    }
}
