namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    partial class RptAtestadoMedico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptAtestadoMedico));
            this.richPagina1 = new DevExpress.XtraReports.UI.XRRichText();
            this.bsHeader = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "";
            // 
            // lblParametros
            // 
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.richPagina1});
            this.DetailContent.HeightF = 69.00333F;
            // 
            // richPagina1
            // 
            this.richPagina1.Dpi = 254F;
            this.richPagina1.Font = new System.Drawing.Font("Arial", 9F);
            this.richPagina1.LocationFloat = new DevExpress.Utils.PointFloat(6.999997F, 0F);
            this.richPagina1.Name = "richPagina1";
            this.richPagina1.SerializableRtfString = resources.GetString("richPagina1.SerializableRtfString");
            this.richPagina1.SizeF = new System.Drawing.SizeF(1883.063F, 69.00333F);
            // 
            // bsHeader
            // 
            this.bsHeader.DataSource = typeof(C2BR.GestorEducacao.Reports.ReportHeader);
            // 
            // RptAtestadoMedico
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport});
            this.Margins = new System.Drawing.Printing.Margins(101, 101, 100, 100);
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRRichText richPagina1;
        private System.Windows.Forms.BindingSource bsHeader;
    }
}
