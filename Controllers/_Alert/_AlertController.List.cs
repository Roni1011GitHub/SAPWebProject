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



namespace Controllers._Alert
{
    public partial class _AlertController : BaseController
    {
        string VIEW_LIST_PARTIAL = "Partial/_Alert_List_Partial";
        string VIEW_PANEL_LIST_PARTIAL = "Partial/_Alert_Panel_List_Partial";

        public ActionResult ListPartial()
        {
            int userId = (int)Session["userId"];


            var viewModel = GetListModel();
            ProcessCustomBinding(userId, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Paging
        public ActionResult ListPaging(GridViewPagerState pager)
        {
            int userId = (int)Session["userId"];



            var viewModel = GetListModel();
            viewModel.ApplyPagingState(pager);
            ProcessCustomBinding(userId, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Filtering 
        public ActionResult ListFiltering(GridViewFilteringState filteringState)
        {
            int userId = (int)Session["userId"];



            var viewModel = GetListModel();
            viewModel.ApplyFilteringState(filteringState);
            ProcessCustomBinding(userId, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Sorting
        public ActionResult ListSorting(GridViewColumnState column, bool reset)
        {
            int userId = (int)Session["userId"];



            var viewModel = GetListModel();
            viewModel.ApplySortingState(column, reset);
            ProcessCustomBinding(userId, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }



        static GridViewModel GetListModel()
        {
            var viewModel = GridViewExtension.GetViewModel("gvUserAlertList");
            if (viewModel == null)
            {
                viewModel = Alert_Model.CreateGridViewModel();
            }

            return viewModel;
        }

        static void ProcessCustomBinding(int userId, GridViewModel viewModel)
        {


            viewModel.ProcessCustomBinding(
               new GridViewCustomBindingGetDataRowCountHandler(args =>
               {
                   Alert_Model.GetDataRowCount(args, userId);
               }),
               new GridViewCustomBindingGetDataHandler(args =>
               {
                   Alert_Model.GetData(args, userId);
               })
           );

        }

        public ActionResult PopupListLoadOnDemandPartial()
        {
            int userId = (int)Session["userId"];



            var viewModel = GetListModel();
            ProcessCustomBinding(userId, viewModel);

            ViewBag.viewModel = viewModel;

            return PartialView(VIEW_PANEL_LIST_PARTIAL, viewModel);
        }

        //public ActionResult AjaxAlert()
        //{
        //    int userId = (int)Session["userId"];

        //    string str = GetAjaxAlert(userId);
        //    return Content(str);

        //}


        //karena alert akan di pisah agar tidak nnumpuk di satu request
        public ActionResult AjaxAlert(int userId)
        {
            //int userId = (int)Session["userId"];

            string str = GetAjaxAlert(userId);
            return Content(str);

        }


        public ActionResult AjaxAlertNonActive()
        {
            int userId = (int)Session["userId"];

            Alert_Model.AjaxAlertNonActive(userId);
            return Content("Y");

        }

        public static string GetAjaxAlert(int userId)
        {
            string str = Alert_Model.AjaxAlert(userId);
            return str;

        }


    }
}