using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.Reports;
using DevExpress.XtraReports.Web;
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.Web.ASPxClasses;

namespace C2BR.GestorEducacao.UI
{
    public partial class GeducReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XtraReport rpt = this.GetReport();
            this.rpvMain.Report = rpt;
            HttpResponse path = Page.Response;

            if (Session["SalvaReport"] == "S")
            {
                this.rpvMain.Report.ExportToPdf(@"" + HttpRuntime.AppDomainAppPath + "ArquivosBoletim\\" + Session["NomeReport"] + ".pdf");
                this.rpvMain.Report.ExportToImage(@"" + HttpRuntime.AppDomainAppPath + "ArquivosBoletim\\" + Session["NomeReport"] + ".jpg");
                //this.rpvMain.Report.
            }
        }

        protected void rpvMain_Unload(object sender, EventArgs e)
        {
            (sender as ReportViewer).Report = null;
        }

        protected void rpvMain_CacheReportDocument(object sender, CacheReportDocumentEventArgs e)
        {
            try
            {
                e.Key = Guid.NewGuid().ToString();
                Page.Session[e.Key] = e.SaveDocumentToMemoryStream();
            }
            catch { }
        }

        protected void rpvMain_RestoreReportDocument(object sender, RestoreReportDocumentFromCacheEventArgs e)
        {
            Stream s = Page.Session[e.Key] as Stream;
            if (s != null)
                e.RestoreDocumentFromStream(s);
        }

        private XtraReport GetReport()
        {
            if (Session["Report"] != null)
                return Session["Report"] as XtraReport;

            return null;
        }
    }
}