﻿using System;
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


namespace Controllers._Cfl
{
    public partial class _CflContactPersonController : BaseController
    {
        string VIEW_LIST_PARTIAL = "Partial/_CflContactPerson_List_Partial";
        string VIEW_PANEL_LIST_PARTIAL = "Partial/_CflContactPerson_Panel_List_Partial";

        public CflContactPerson_ParamModel GetParam(HttpRequestBase Request)
        {
            var cflParam = new CflContactPerson_ParamModel();
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

            var cflContactPersonParam = GetParam(Request); 

            var viewModel = GetListModel(cflContactPersonParam.Name);
            ProcessCustomBinding(userId, cflContactPersonParam, viewModel);
            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Paging
        public ActionResult ListPaging(GridViewPagerState pager)
        {
            int userId = (int)Session["userId"];

            var cflContactPersonParam = GetParam(Request); 

            var viewModel = GetListModel(cflContactPersonParam.Name);
            viewModel.ApplyPagingState(pager);
            ProcessCustomBinding(userId, cflContactPersonParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Filtering

        public ActionResult ListFiltering(GridViewFilteringState filteringState)
        {
            int userId = (int)Session["userId"];

            var cflContactPersonParam = GetParam(Request); 

            var viewModel = GetListModel(cflContactPersonParam.Name);
            viewModel.ApplyFilteringState(filteringState);
            ProcessCustomBinding(userId, cflContactPersonParam, viewModel);
            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Sorting
        public ActionResult ListSorting(GridViewColumnState column, bool reset)
        {
            int userId = (int)Session["userId"];

            var cflContactPersonParam = GetParam(Request); 

            var viewModel = GetListModel(cflContactPersonParam.Name);
            viewModel.ApplySortingState(column, reset);
            ProcessCustomBinding(userId, cflContactPersonParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }



        static GridViewModel GetListModel(string name)
        {
            var viewModel = GridViewExtension.GetViewModel("gvCflContactPersonList" + name);
            if (viewModel == null)
            {
                viewModel = CflContactPerson_Model.CreateGridViewModel();
            }

            return viewModel;
        }

        static void ProcessCustomBinding(int userId, CflContactPerson_ParamModel cflParam, GridViewModel viewModel)
        {

            CflContactPerson_Model.SetBindingData(viewModel, userId, cflParam);  

           

        }

        public ActionResult PopupListLoadOnDemandPartial()
        {
            int userId = (int)Session["userId"];

            var cflContactPersonParam = GetParam(Request); 

            var viewModel = GetListModel(cflContactPersonParam.Name);
            ProcessCustomBinding(userId, cflContactPersonParam, viewModel);

            ViewBag.viewModel = viewModel; 

            return PartialView(VIEW_PANEL_LIST_PARTIAL, cflContactPersonParam);
        }

    }
}