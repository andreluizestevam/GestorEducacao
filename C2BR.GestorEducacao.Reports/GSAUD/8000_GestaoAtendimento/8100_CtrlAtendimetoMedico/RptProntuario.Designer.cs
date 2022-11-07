namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico
{
    partial class RptProntuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptProntuario));
            this.richPagina1 = new DevExpress.XtraReports.UI.XRRichText();
            this.richPagina2 = new DevExpress.XtraReports.UI.XRRichText();
            this.richPagina3 = new DevExpress.XtraReports.UI.XRRichText();
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // lblTitulo
            // 
            this.lblTitulo.StylePriority.UseFont = false;
            this.lblTitulo.StylePriority.UseTextAlignment = false;
            this.lblTitulo.Text = "PRONTUÁRIO";
            // 
            // lblParametros
            // 
            this.lblParametros.StylePriority.UseFont = false;
            this.lblParametros.StylePriority.UseTextAlignment = false;
            // 
            // DetailContent
            // 
            this.DetailContent.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.richPagina3,
            this.richPagina2,
            this.richPagina1});
            this.DetailContent.HeightF = 207.0099F;
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
            // richPagina2
            // 
            this.richPagina2.Dpi = 254F;
            this.richPagina2.Font = new System.Drawing.Font("Arial", 9F);
            this.richPagina2.LocationFloat = new DevExpress.Utils.PointFloat(6.999997F, 69.0033F);
            this.richPagina2.Name = "richPagina2";
            this.richPagina2.SerializableRtfString = resources.GetString("richPagina2.SerializableRtfString");
            this.richPagina2.SizeF = new System.Drawing.SizeF(1883.063F, 69.00333F);
            // 
            // richPagina3
            // 
            this.richPagina3.Dpi = 254F;
            this.richPagina3.Font = new System.Drawing.Font("Arial", 9F);
            this.richPagina3.LocationFloat = new DevExpress.Utils.PointFloat(6.999997F, 138.0066F);
            this.richPagina3.Name = "richPagina3";
            this.richPagina3.SerializableRtfString = resources.GetString("richPagina3.SerializableRtfString");
            this.richPagina3.SizeF = new System.Drawing.SizeF(1883.063F, 69.00333F);
            // 
            // RptProntuario
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GroupHeaderTitulo,
            this.DetailReport});
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.bsReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richPagina3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRRichText richPagina1;
        private DevExpress.XtraReports.UI.XRRichText richPagina3;
        private DevExpress.XtraReports.UI.XRRichText richPagina2;
    }
}
