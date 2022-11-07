using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;
using System.Data.SqlClient;
using System.IO;


namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptCarteiraEstudantilClass : DevExpress.XtraReports.UI.XtraReport
    {
        public RptCarteiraEstudantilClass()
        {
            InitializeComponent();
        }

        #region InitReport
        public XtraReport InitReport(
                                string infos,
                                string parametros,
                                string ano,
                                int coEmp,
                                int coModu,
                                int coCur,
                                int coTur,
                                int coAlu
                             )
        {

                return this;
        }
        #endregion
    }
}
