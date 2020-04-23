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

        [HttpPost, ValidateInput(false)]
        public ActionResult NavFirst()
        {
            int userId = (int)Session["userId"];

            PaymentInModel paymentInModel;
            paymentInService = new PaymentInService();

            paymentInModel = paymentInService.NavFirst(userId);
            if (paymentInModel != null)
            {
                paymentInModel._FormMode = FormModeEnum.Edit;
            }

            if (paymentInModel == null)
            {
                //paymentInModel = paymentInService.GetNewModel(); 
                throw new Exception("[VALIDATION]-Data not exists");
            }

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult NavPrevious(long Id = 0)
        {
            int userId = (int)Session["userId"];


            PaymentInModel paymentInModel;
            paymentInService = new PaymentInService();

            paymentInModel = paymentInService.NavPrevious(userId,Id);
            if (paymentInModel != null)
            {
                paymentInModel._FormMode = FormModeEnum.Edit;
            }

            if (paymentInModel == null)
            {
                //paymentInModel = paymentInService.GetNewModel(); 
                throw new Exception("[VALIDATION]-Data not exists");
            }

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult NavNext(long Id = 0)
        {
            int userId = (int)Session["userId"];



            PaymentInModel paymentInModel;
            paymentInService = new PaymentInService();

            paymentInModel = paymentInService.NavNext(userId,Id);
            if (paymentInModel != null)
            {

                paymentInModel._FormMode = FormModeEnum.Edit;

            }

            if (paymentInModel == null)
            {
                //paymentInModel = paymentInService.GetNewModel(); 
                throw new Exception("[VALIDATION]-Data not exists");
            }

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult NavLast()
        {
            int userId = (int)Session["userId"];

            PaymentInModel paymentInModel;
            paymentInService = new PaymentInService();

            paymentInModel = paymentInService.NavLast(userId);
            if (paymentInModel != null)
            {
                paymentInModel._FormMode = FormModeEnum.Edit; 
            }

            if (paymentInModel == null)
            {
                //paymentInModel = paymentInService.GetNewModel(); 
                throw new Exception("[VALIDATION]-Data not exists");
            }

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }



    }
}