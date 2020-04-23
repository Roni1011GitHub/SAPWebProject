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
    public partial class _CflBpController : BaseController
    {
        string VIEW_LIST_PARTIAL = "Partial/_CflBp_List_Partial";
        string VIEW_PANEL_LIST_PARTIAL = "Partial/_CflBp_Panel_List_Partial";

        public CflBp_ParamModel GetParam(HttpRequestBase Request, string branchCode)
        {
            var cflParam = new CflBp_ParamModel();
            cflParam.Type = Request["hidden_CflType"];
            cflParam.Name = Request["hidden_CflName"];
            cflParam.Header = Request["hidden_CflHeader"];
            cflParam.SqlWhere = Request["hidden_CflSqlWhere"];
            cflParam.IsMulti = Request["hidden_CflIsMulti"];
            cflParam.BranchCode = branchCode;
            
           

            return cflParam;
        }


        public ActionResult ListPartial()
        {
            int userId = (int)Session["userId"];
            string branchCode = (string)Session["branchCode"];

            var cflBpParam = GetParam(Request, branchCode);  

            var viewModel = GetListModel(cflBpParam.Name);
            ProcessCustomBinding(userId, cflBpParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Paging
        public ActionResult ListPaging(GridViewPagerState pager)
        {
            int userId = (int)Session["userId"];
            string branchCode = (string)Session["branchCode"];

            var cflBpParam = GetParam(Request, branchCode);  

            var viewModel = GetListModel(cflBpParam.Name);
            viewModel.ApplyPagingState(pager);
            ProcessCustomBinding(userId, cflBpParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Filtering 
        public ActionResult ListFiltering(GridViewFilteringState filteringState)
        {
            int userId = (int)Session["userId"];
            string branchCode = (string)Session["branchCode"];

            var cflBpParam = GetParam(Request, branchCode);  

            var viewModel = GetListModel(cflBpParam.Name);
            viewModel.ApplyFilteringState(filteringState);
            ProcessCustomBinding(userId, cflBpParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Sorting
        public ActionResult ListSorting(GridViewColumnState column, bool reset)
        {
            int userId = (int)Session["userId"];
            string branchCode = (string)Session["branchCode"];

            var cflBpParam = GetParam(Request, branchCode);  

            var viewModel = GetListModel(cflBpParam.Name);
            viewModel.ApplySortingState(column, reset);
            ProcessCustomBinding(userId, cflBpParam, viewModel);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }



        static GridViewModel GetListModel(string name)
        {
            var viewModel = GridViewExtension.GetViewModel("gvCflBpList" + name);
            if (viewModel == null)
            {
                viewModel = CflBp_Model.CreateGridViewModel();
            }

            return viewModel;
        }

        static void ProcessCustomBinding(int userId, CflBp_ParamModel cflBpParam, GridViewModel viewModel)
        { 

            CflBp_Model.SetBindingData(viewModel, userId, cflBpParam ); 

        }

        public ActionResult PopupListLoadOnDemandPartial()
        {
            int userId = (int)Session["userId"];
            string branchCode = (string)Session["branchCode"];

            var cflBpParam = GetParam(Request, branchCode);  

            var viewModel = GetListModel(cflBpParam.Name);
            ProcessCustomBinding(userId, cflBpParam, viewModel);

            ViewBag.viewModel = viewModel; 

            return PartialView(VIEW_PANEL_LIST_PARTIAL, cflBpParam);
        }

    }
}