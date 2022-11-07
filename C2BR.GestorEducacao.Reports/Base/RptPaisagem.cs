using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace C2BR.GestorEducacao.Reports
{
    public partial class RptPaisagem : XtraReport
    {
        private DateTime dtAtual;

        public RptPaisagem()
        {
            InitializeComponent();
            this.dtAtual = DateTime.Now;
        }

        public string Parametros { get; set; }
        public string Titulo { get; set; }
        public string InfosRodape { get; set; }

        /// <summary>
        /// Personaliza o rodapé de acordo com o tipo da unidade logada
        /// </summary>
        /// <param name="NomeRodape"></param>
        public void MostrarRodapePortal(string TipoUnidade)
        {
            switch (TipoUnidade)
            {
                case "PGE":
                    lblinfoSite.Text = "www.gestoreducacao.com.br";
                    break;
                case "PGS":
                    lblinfoSite.Text = "www.portalgestorsaude.com.br";
                    break;
                default:
                    lblinfoSite.Text = "";
                    break;
            }
        }

        public virtual void BaseInit(ReportHeader header)
        {
            this.bsHeader.Clear();
            this.bsHeader.Add(header);

            MostrarRodapePortal(header.TipoUnidade);
        }

        public override void CreateDocument(bool buildPagesInBackground)
        {
            if (!string.IsNullOrEmpty(this.Titulo))
                this.lblTitulo.Text = Titulo;

            base.CreateDocument(buildPagesInBackground);
        }

        private void lblParametros_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = this.Parametros;
        }

        private void lblInfos_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = this.InfosRodape;
        }

        private void lblData_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text =
                string.Format((sender as XRLabel).Text, this.dtAtual.ToString("dd/MM/yyyy"));
        }

        private void lblHora_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text =
                string.Format((sender as XRLabel).Text, this.dtAtual.ToString("HH:mm"));
        }

        private void lblDescHora_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = (sender as XRLabel).Text;
        }

        private void lblDescData_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = (sender as XRLabel).Text;
        }

        private void lblDescPagina_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = (sender as XRLabel).Text;
        }
    }
}
