using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Threading;
using Models;
using PetaPoco;

using System.Net;
using Models._Alert;
using Models._PetaPoco;


using Models._Utils;
 



namespace Controllers._GetData
{
    public partial class _GetDataController : Controller
    {
        public JsonResult GetCurRate(string CurCode, [ModelBinder(typeof(DevExpressEditorsBinder))] DateTime RateDate)
        {
            int userId = (int)Session["userId"];

            var curRate = DbProvider.dbApp.ExecuteScalar<decimal?>("SELECT ISNULL(T0.Rate,0) AS Rate FROM [" + DbProvider.dbSap_Name + "].DBO.ORTT T0 (NOLOCK) WHERE T0.Currency=@0 AND T0.RateDate=@1 ", CurCode, RateDate.Date);
            if (curRate.HasValue)
            {
                if (curRate.Value == 0)
                {
                    curRate = null;
                }
            }

            if (curRate == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(curRate, JsonRequestBehavior.AllowGet);
            }
            

        }

     



        public JsonResult GetCheckFormApproval(string formCode, long? Id)
        {
            var model = SpFormApproval.SpSysFormApproval(formCode, Id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

       




    }
}