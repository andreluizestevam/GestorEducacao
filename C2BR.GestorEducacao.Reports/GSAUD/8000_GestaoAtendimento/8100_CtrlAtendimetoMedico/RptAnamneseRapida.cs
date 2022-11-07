using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico
{
    public partial class RptAnamneseRapida : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptAnamneseRapida()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(
                        int coEmp
                        )
        {

            try
            {
                #region Setar o Header e as Labels
                
                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion


                return 1;
            }
            catch { return 0; }
        }

        #endregion

    }
}

