using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;

using System.Web.Mvc;
using System.Web.Script.Serialization;

using DevExpress.Web.Mvc;
using Models._CrystalReport;
using Models._Utils;

namespace Reports
{
    public partial class Rpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ReportDocument rptDoc;
                rptDoc = Models._Utils.Rpt.GetRptDoc(Request);

                if (rptDoc != null)
                {

                    CrystalReport.ReportSource = rptDoc;
                    Session["ReportDocument"] = rptDoc;
                }
            }
            else
            {
                ReportDocument rptDoc = (ReportDocument)Session["ReportDocument"];
                CrystalReport.ReportSource = rptDoc;
            }

        }

        //protected void Page_Load(object sender, EventArgs e)
        //{


        //    ReportDocument rptDoc;

        //    String ReportFile = HttpUtility.UrlDecode(Request["ReportFile"]);

        //    String ReportParam = HttpUtility.UrlDecode(Request["ReportParam"]);

        //    JavaScriptSerializer serializer = new JavaScriptSerializer();

        //    List<CrystalReportParam> reportParams;
        //    reportParams = serializer.Deserialize<List<CrystalReportParam>>(ReportParam);

        //    String FileName = Server.MapPath(ReportFile);

        //    if (System.IO.File.Exists(FileName))
        //    {
        //        rptDoc = Models._Utils.Rpt.Report (FileName, reportParams );
        //        CrystalReport.ReportSource = rptDoc;
        //        //CrystalReport.HasPrintButton = false;
        //        //CrystalReport.HasExportButton = false;
        //        //CrystalReport.HasDrillUpButton = false;
        //        //CrystalReport.HasDrilldownTabs = false;

        //    }


        //}
    }
}