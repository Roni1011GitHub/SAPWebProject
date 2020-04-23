using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Threading;
using PetaPoco;

using System.Net;

using System.Web.Mvc.Razor;


using Models._LoginApproval;

using Models._Utils;

namespace Controllers._LoginApproval
{

    public partial class _LoginApprovalController : BaseController
    {

        string VIEW_DETAIL = "LoginApproval";
        //string VIEW_FORM_PARTIAL = "Partial/LoginApproval_Form_Partial";
        string VIEW_PANEL_PARTIAL = "Partial/LoginApproval_Panel_Partial";

        LoginApprovalService loginApprovalService;

        public ActionResult Index()
        {
            if (Session["userId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Detail");
        }


        public ActionResult Detail(int UserId = 0)
        {
            if (Session["userId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            LoginApprovalModel loginApprovalModel = new LoginApprovalModel();
            return View(VIEW_DETAIL, loginApprovalModel);
        }


       


        [HttpPost, ValidateInput(false)]
        public ActionResult LoginApproval(string FormCode, string UserName, string Pwd, string Notes)
        {
            Session["Approve_UserId"] = null;
            Session["Approve_Notes"] = null;

            var loginApprovalModel = new LoginApprovalModel();

            var loginApprovalService = new LoginApprovalService();

          

            if (!loginApprovalService.GetCheckFormApproval(UserName, Pwd, FormCode))
            {
                throw new Exception(string.Format("[VALIDATION] {0}", "LoginApproval fail"));
            } 
            else
            {
                var loginService = new Models.Authentication.Login.LoginService();
                var model = loginService.GetLoginInfo(UserName);

                Session["Approve_UserId"] = model.UserId;
                Session["Approve_Notes"] = Notes;

            } 
             
            return Content("");
        }

        /// <summary>
        /// this is my comments zul
        /// </summary>
        /// <returns></returns>

        public ActionResult PopupLoadOnDemandPartial()
        {
            LoginApprovalModel loginApprovalModel = new LoginApprovalModel();
            return PartialView(VIEW_PANEL_PARTIAL, loginApprovalModel);
        }


    }
}