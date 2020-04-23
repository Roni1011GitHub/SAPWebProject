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
using Models._Cfl;
using Models._Utils;


namespace Controllers._Cfl
{
    public partial class _CflCoaController : BaseController
    {
        string VIEW_LIST_PARTIAL = "Partial/_CflCoa_List_Partial";
        string VIEW_PANEL_LIST_PARTIAL = "Partial/_CflCoa_Panel_List_Partial";

        public CflCoa_ParamModel GetParam(HttpRequestBase Request)
        {
            var cflParam = new CflCoa_ParamModel();
            cflParam.Type = Request["hidden_CflType"];
            cflParam.Name = Request["hidden_CflName"];
            cflParam.Header = Request["hidden_CflHeader"];
            cflParam.SqlWhere = Request["hidden_CflSqlWhere"];
            cflParam.IsMulti = Request["hidden_CflIsMulti"];

             

            return cflParam;
        }

        public ActionResult ListPartial()
        {
            int userId = (int)Session["userId"];

            var cflCoaParam = GetParam(Request);

            var viewModel = GetListModel(cflCoaParam.Name);
            ProcessCustomBinding(userId, cflCoaParam, viewModel);
            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Paging
        public ActionResult ListPaging(GridViewPagerState pager)
        {
            int userId = (int)Session["userId"];

            var cflCoaParam = GetParam(Request);

            var viewModel = GetListModel(cflCoaParam.Name);
            viewModel.ApplyPagingState(pager);
            ProcessCustomBinding(userId, cflCoaParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Filtering

        public ActionResult ListFiltering(GridViewFilteringState filteringState)
        {
            int userId = (int)Session["userId"];

            var cflCoaParam = GetParam(Request);

            var viewModel = GetListModel(cflCoaParam.Name);
            viewModel.ApplyFilteringState(filteringState);
            ProcessCustomBinding(userId, cflCoaParam, viewModel);
            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Sorting
        public ActionResult ListSorting(GridViewColumnState column, bool reset)
        {
            int userId = (int)Session["userId"];

            var cflCoaParam = GetParam(Request);

            var viewModel = GetListModel(cflCoaParam.Name);
            viewModel.ApplySortingState(column, reset);
            ProcessCustomBinding(userId, cflCoaParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }



        static GridViewModel GetListModel(string name)
        {
            var viewModel = GridViewExtension.GetViewModel("gvCflCoaList" + name);
            if (viewModel == null)
            {
                viewModel = CflCoa_Model.CreateGridViewModel();
            }

            return viewModel;
        }

        static void ProcessCustomBinding(int userId, CflCoa_ParamModel cflCoaParam, GridViewModel viewModel)
        {

            CflCoa_Model.SetBindingData(viewModel, userId, cflCoaParam);


        }

        public ActionResult PopupListLoadOnDemandPartial()
        {
            int userId = (int)Session["userId"];

            var cflCoaParam = GetParam(Request);

            var viewModel = GetListModel(cflCoaParam.Name);
            ProcessCustomBinding(userId, cflCoaParam, viewModel);

            ViewBag.viewModel = viewModel;


            return PartialView(VIEW_PANEL_LIST_PARTIAL, cflCoaParam);
        }

    }
}