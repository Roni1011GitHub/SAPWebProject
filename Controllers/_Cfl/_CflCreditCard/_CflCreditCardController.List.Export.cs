﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxGridView;
using Models;
using PetaPoco;
using Models._Cfl;

namespace Controllers._Cfl
{
    public partial class _CflCreditCardController : BaseController
    {
        public ActionResult ExportTo()
        {
            int userId = (int)Session["userId"];

            var filterExpression = Request["hidden_CpGvFind_FilterExpression"];
            var sortExpression = Request["hidden_CpGvFind_SortExpression"];
            var pageIndex = Request["hidden_CpGvFind_PageIndex"];
            var pageSize = Request["hidden_CpGvFind_PageSize"];

            var cflCreditCardParam = GetParam(Request);

            var listmodel = new CflCreditCard_Model();
            List<CflCreditCard_View__> items = listmodel.GetDataList(userId, cflCreditCardParam, filterExpression, sortExpression, Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            return GridViewExportHelper.ExportTypes["XLS"].Method(GridViewExportHelper.ExportGridViewSettings(cflCreditCardParam), items);

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
            public static GridViewSettings ExportGridViewSettings(CflCreditCard_ParamModel cflCreditCardParam)
            {
                //get
                //{
                if (exportGridViewSettings == null)
                    exportGridViewSettings = CreateExportGridViewSettings(cflCreditCardParam);
                return exportGridViewSettings;
                //}
            }
            static GridViewSettings CreateExportGridViewSettings(CflCreditCard_ParamModel cflCreditCardParam)
            {
                var listmodel = new CflCreditCard_Model();
                GridViewSettings settings = listmodel.CreateExportGridViewSettings(cflCreditCardParam);


                return settings;
            }
        }

    }
}