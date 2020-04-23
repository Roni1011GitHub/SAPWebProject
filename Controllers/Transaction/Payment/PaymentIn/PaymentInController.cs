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

        string VIEW_DETAIL = "PaymentIn";
        string VIEW_FORM_PARTIAL = "Partial/PaymentIn_Form_Partial";
        string VIEW_LIST_PARTIAL = "Partial/PaymentIn_List_Partial";
        string VIEW_PANEL_LIST_PARTIAL = "Partial/PaymentIn_Panel_List_Partial";

        PaymentInService paymentInService;

        public ActionResult Index()
        {
            return RedirectToAction("Detail");
        }

        public ActionResult Detail(long Id = 0)
        {
            int userId = (int)Session["userId"];


            paymentInService = new PaymentInService();
            PaymentInModel paymentInModel;
            if (Id == 0)
            {
                paymentInModel = paymentInService.GetNewModel(userId);
                paymentInModel._FormMode = FormModeEnum.New;
            }
            else
            {
                paymentInService = new PaymentInService();
                paymentInModel = paymentInService.GetById(userId, Id);
                paymentInModel._FormMode = FormModeEnum.Edit;
            }

            return View(VIEW_DETAIL, paymentInModel);
        }


        public ActionResult DetailPartial(long Id = 0)
        {
            int userId = (int)Session["userId"];


            PaymentInModel paymentInModel;

            paymentInService = new PaymentInService();

            if (Id == 0)
            {
                paymentInModel = paymentInService.GetNewModel(userId);
                paymentInModel._FormMode = FormModeEnum.New;
            }
            else
            {
                paymentInModel = paymentInService.GetById(userId, Id);
                if (paymentInModel != null)
                {
                    paymentInModel._FormMode = FormModeEnum.Edit;
                }
                else
                {
                    paymentInModel = paymentInService.GetNewModel(userId);
                    paymentInModel._FormMode = FormModeEnum.New;
                }
            }

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }



        [HttpPost, ValidateInput(false)]
        public ActionResult Add([ModelBinder(typeof(DevExpressEditorsBinder))]  PaymentInModel paymentInModel)
        {
            int userId = (int)Session["userId"];

            paymentInModel._UserId = (int)Session["userId"];
            paymentInService = new PaymentInService();


            //if (ModelState.IsValid)
            //{
                long Id = 0;
                PaymentInNumbering_Mutex.wait();
                try
                {
                    Id = paymentInService.Add(paymentInModel);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    PaymentInNumbering_Mutex.release();
                }
                paymentInModel = paymentInService.GetById(userId, Id);
                paymentInModel._FormMode = Models.FormModeEnum.Edit;
                // paymentInModel = paymentInService.GetNewModel();
            //}
            //else
            //{
            //    string message = GetErrorModel();

            //    throw new Exception(string.Format("[VALIDATION] {0}", message));
            //}

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }




        [HttpPost, ValidateInput(false)]
        public ActionResult Update([ModelBinder(typeof(DevExpressEditorsBinder))]  PaymentInModel paymentInModel)
        {
            int userId = (int)Session["userId"];

            paymentInModel._UserId = (int)Session["userId"];
            paymentInService = new PaymentInService();
            paymentInModel._FormMode = FormModeEnum.Edit;



            //if (ModelState.IsValid)
            //{
                paymentInService.Update(paymentInModel);
                paymentInModel = paymentInService.GetById(userId, paymentInModel.Id);
            //}
            //else
            //{
            //    string message = GetErrorModel();

            //    throw new Exception(string.Format("[VALIDATION] {0}", message));
            //}

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }




        [HttpPost, ValidateInput(false)]
        public ActionResult Post(long Id)
        {
            int userId = (int)Session["userId"];

            PaymentInModel paymentInModel;

            paymentInService = new PaymentInService();
            paymentInService.Post(userId, Id);

            paymentInModel = paymentInService.GetById(userId, Id);
            if (paymentInModel != null)
            {
                paymentInModel._FormMode = FormModeEnum.Edit;
            }
            else
            {
                paymentInModel = paymentInService.GetNewModel(userId);
                paymentInModel._FormMode = FormModeEnum.New;
            }

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Cancel(long Id)
        {
            int userId = (int)Session["userId"];

            PaymentInModel paymentInModel;

            paymentInService = new PaymentInService();
            paymentInService.Cancel(userId, Id);

            paymentInModel = paymentInService.GetById(userId, Id);
            if (paymentInModel != null)
            {
                paymentInModel._FormMode = FormModeEnum.Edit;
            }
            else
            {
                paymentInModel = paymentInService.GetNewModel(userId);
                paymentInModel._FormMode = FormModeEnum.New;
            }

            return PartialView(VIEW_FORM_PARTIAL, paymentInModel);
        }
      

       

        public ContentResult ChooseArCorporate(long Id, String[] Data)
        {
            int userId = (int)Session["userId"];

            paymentInService = new PaymentInService();
            var result = paymentInService.ChooseArCorporate(userId, Id, Data);


            return Content(result.ToString());
        }

        public ActionResult RefreshOutsandingArCorporate(long Id)
        {
            int userId = (int)Session["userId"];

            paymentInService = new PaymentInService();
            var result = paymentInService.RefreshOutsandingArCorporate(userId, Id);

            return Content(result.ToString());

        }




    }
}