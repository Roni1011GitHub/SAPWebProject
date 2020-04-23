using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.IO;
using System.Threading; 
using PetaPoco;

using DevExpress.Web.ASPxUploadControl;

using System.Net;

using Models;


using System.Web.Script.Serialization;

using Models._CrystalReport;

using Models._PetaPoco;


namespace Controllers.Report
{
    public partial class ReportCustomController : BaseController
    {

        public ActionResult Print([ModelBinder(typeof(DevExpressEditorsBinder))] List<CrystalReportParam> Params)
        {
            int Id = int.Parse(Request["Report_Id"]); 

            List<CrystalReportParam> crtParams = new List<CrystalReportParam>(); 

            ViewBag.Title = "Preview - Report";  

            var tm_Report = DbProvider.dbApp.SingleOrDefault<Tm_Report>((object)Id);

            string fileName = Request.MapPath("~/Content/Temp/" + tm_Report.Uid.ToString() + ".rpt");
            if (!System.IO.File.Exists(fileName))
            {
                System.IO.File.WriteAllBytes(fileName, tm_Report.Data);
            }

            //CrystalReportParam param = new CrystalReportParam(); 

            //param.ParamName = "@CustomerCode";
            //param.ParamValue = tm_CustomReportRptPerCustomer.CustomerCode.ToString();
            //crtParams.Add(param);

            //param = new CrystalReportParam();
            //param.ParamName = "@DateFrom";
            //param.ParamValue = DateFrom;
            //crtParams.Add(param);

            //param = new CrystalReportParam();
            //param.ParamName = "@DateTo";
            //param.ParamValue = DateTo;
            //crtParams.Add(param);


            JavaScriptSerializer serializer = new JavaScriptSerializer();
            String param1 = MvcHtmlString.Create(serializer.Serialize(Params)).ToString();

            string outputType = Request["OutputType"];
            if (outputType == "Excel")
            { 
                return base.DisplayExcelParam("~/Content/Temp/" + tm_Report.Uid.ToString() + ".rpt", param1);
            }
            else if (outputType == "Csv")
            {
                return base.DisplayCsvParam("~/Content/Temp/" + tm_Report.Uid.ToString() + ".rpt", param1);
            }
            else if (outputType == "Text")
            {
                return base.DisplayTextParam("~/Content/Temp/" + tm_Report.Uid.ToString() + ".rpt", param1);
            }
            else
            {

                ViewBag.CrtFile = HttpUtility.UrlEncode("~/Content/Temp/" + tm_Report.Uid.ToString() + ".rpt");

                ViewBag.CrtParam = HttpUtility.UrlEncode(param1);
                ViewBag.OutputType = outputType;

                return View("~/Views/_CrystalReport/Rpt.cshtml");

            }
        }


    }
}