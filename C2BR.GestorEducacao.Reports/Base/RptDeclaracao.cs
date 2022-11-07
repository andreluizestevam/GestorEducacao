using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace C2BR.GestorEducacao.Reports.Base
{
    public partial class RptDeclaracao : DevExpress.XtraReports.UI.XtraReport
    {
        public RptDeclaracao()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Oculta o cabeçalho do relatório
        /// </summary>
        public void MostrarCabecalho(bool mostrar)
        {
            PageHeader.Visible = mostrar;
        }
    }
}
