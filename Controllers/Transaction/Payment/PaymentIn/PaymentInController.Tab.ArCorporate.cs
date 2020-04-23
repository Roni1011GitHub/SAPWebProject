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

using Models.Transaction.PaymentIn;
using Models._Utils;

namespace Controllers.Transaction.Payment
{
    public partial class PaymentInController : BaseController
    {

        string VIEW_TAB_ARCORPORATE = "Partial/PaymentIn_Form_TabArCorporate_List_Partial";
        
        public ActionResult TabArCorporateListPartial()
        {
            int userId = (int)Session["userId"];

            paymentInService = new PaymentInService();

            var Id = Convert.ToInt64(Request["cbId"]);


            var modelList = paymentInService.GetPaymentIn_ArCorporates(Id);

            return PartialView(VIEW_TAB_ARCORPORATE, modelList);
        }

      



    }
}