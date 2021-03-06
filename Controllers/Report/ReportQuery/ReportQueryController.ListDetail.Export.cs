﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxGridView;
using PetaPoco;
using System.Collections;
using System.Collections.Generic;


using Models._PetaPoco;
using Models._Utils;
using Models;
using Models.Report.ReportQuery;


using Models._CrystalReport;
using System.Web.Script.Serialization;

namespace Controllers.Report
{
    public partial class ReportQueryController : BaseController
    {
        public ActionResult ExportTo()
        {
            int userId = (int)Session["userId"];

            var hidden_CpGvFind_FilterExpression = Request["hidden_CpGvFind_FilterExpression"];
            var hidden_CpGvFind_SortExpression = Request["hidden_CpGvFind_SortExpression"];
            var hidden_CpGvFind_PageIndex = Request["hidden_CpGvFind_PageIndex"];
            var hidden_CpGvFind_PageSize = Request["hidden_CpGvFind_PageSize"];


            int Query_Id = int.Parse(Request["Query_Id"]);



            var param = GetParam(Query_Id);

            //-------------------------------------
            string cpParams = "";


            cpParams = Request["Params"];


            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<CrystalReportParam> reportParams;
            reportParams = serializer.Deserialize<List<CrystalReportParam>>(cpParams);
            param.crtParams = reportParams;
            //--------------------------------------


            IEnumerable items = ReportQueryDetail__List_Model.GetDataList(userId, param, hidden_CpGvFind_FilterExpression, hidden_CpGvFind_SortExpression, Convert.ToInt32(hidden_CpGvFind_PageIndex), Convert.ToInt32(hidden_CpGvFind_PageSize));
            return GridViewExportHelper.ExportTypes["XLS"].Method(GridViewExportHelper.ExportGridViewSettings(userId, param), items);

        }

        public enum GridViewExportType { None, Pdf, Xls, Xlsx, Rtf, Csv }

        public delegate ActionResult ExportMethod(GridViewSettings settings, object dataObject);

        public class ExportType
        {
            public string Title { get; set; }
            public ExportMethod Method { get; set; }
        }

        public partial class GridViewExportHelper
        {
            static Dictionary<string, ExportType> exportTypes;
            public static Dictionary<string, ExportType> ExportTypes
            {
                get
                {
                    if (exportTypes == null)
                        exportTypes = CreateExportTypes();
                    return exportTypes;
                }
            }
            static Dictionary<string, ExportType> CreateExportTypes()
            {
                Dictionary<string, ExportType> types = new Dictionary<string, ExportType>();
                types.Add("PDF", new ExportType { Title = "Export to PDF", Method = GridViewExtension.ExportToPdf });
                types.Add("XLS", new ExportType { Title = "Export to XLS", Method = GridViewExtension.ExportToXls });
                types.Add("XLSX", new ExportType { Title = "Export to XLSX", Method = GridViewExtension.ExportToXlsx });
                types.Add("RTF", new ExportType { Title = "Export to RTF", Method = GridViewExtension.ExportToRtf });
                types.Add("CSV", new ExportType { Title = "Export to CSV", Method = GridViewExtension.ExportToCsv });
                return types;
            }
        }


        //----------------------------

        public partial class GridViewExportHelper
        {
            static GridViewSettings exportGridViewSettings;
            public static GridViewSettings ExportGridViewSettings(int userId, ReportQuery_ParamModel param)
            {
                //get
                //{
                //if (exportGridViewSettings == null)
                exportGridViewSettings = CreateExportGridViewSettings(userId, param);
                return exportGridViewSettings;
                //}
            }


            static GridViewSettings CreateExportGridViewSettings(int userId, ReportQuery_ParamModel param)
            {


                GridViewSettings settings = ReportQueryDetail__List_Model.CreateExportGridViewSettings(userId, param);

                return settings;
            }
        }

    }
}