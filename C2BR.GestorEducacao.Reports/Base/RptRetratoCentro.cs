using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace C2BR.GestorEducacao.Reports
{
    public partial class RptRetratoCentro : XtraReport
    {
        private DateTime dtAtual;
        
        public RptRetratoCentro()
        {
            InitializeComponent();
            this.dtAtual = DateTime.Now;
            this.VisibleNumeroPage = true;
            this.VisibleDataHeader = true;
            this.VisibleHoraHeader = true;
            this.VisiblePageHeader = true;
        }

        /// <summary>
        /// Oculta o cabeçalho do relatório
        /// </summary>
        public void MostrarCabecalho(bool mostrar)
        {
            PageHeader.Visible = mostrar;
        }

        public string Parametros { get; set; }
        public string Titulo { get; set; }
        public string InfosRodape { get; set; }
        public bool VisibleNumeroPage { get; set; }
        public bool VisiblePageHeader { get; set; }
        public bool VisibleDataHeader { get; set; }
        public bool VisibleHoraHeader { get; set; }

        /// <summary>
        /// Personaliza o rodapé de acordo com o tipo da unidade logada
        /// </summary>
        /// <param name="NomeRodape"></param>
        public void MostrarRodapePortal(string TipoUnidade)
        {
            switch (TipoUnidade)
            {
                case "PGE":
                    xrLabel13.Text = "www.gestoreducacao.com.br";
                    break;
                case "PGS":
                    xrLabel13.Text = "www.portalgestorsaude.com.br";
                    break;
                default:
                    xrLabel13.Text = "";
                    break;
            }
        }

        public virtual void BaseInit(ReportHeader header)
        {
            header.Unidade = header.Unidade.ToUpper();
            this.bsHeader.Clear();
            this.bsHeader.Add(header);
            this.PageHeader.Visible = this.VisiblePageHeader;

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

        private void lblDescPage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = (sender as XRLabel).Text;
        }

        private void lblDescData_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = (sender as XRLabel).Text;
        }

        private void lblDescHora_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = (sender as XRLabel).Text;
        }
    }
}
