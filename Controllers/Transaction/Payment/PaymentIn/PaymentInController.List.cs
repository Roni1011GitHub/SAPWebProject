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

using Models.Transaction.PaymentIn;
using Models._Utils;

namespace Controllers.Transaction.Payment
{
    public partial class PaymentInController : BaseController
    {

        public ListFindParam GetParam(HttpRequestBase Request)
        {

            var param = new ListFindParam();

            if (string.IsNullOrEmpty(Request["hidden_IsFindTransDate"]))
            {
                param.IsFindTransDate = true;
                param.TransDate_From = DateTime.Now.Date.AddDays((DateTime.Now.Day - 1) * -1);
                param.TransDate_To = DateTime.Now.Date;
            }
            else
            {
                param.IsFindTransDate = bool.Parse(Request["hidden_IsFindTransDate"]);
                if (string.IsNullOrEmpty(Request["hidden_TransDate_From"]))
                {
                    param.TransDate_From = null;
                }
                else
                {
                    param.TransDate_From = DateTime.Parse(Request["hidden_TransDate_From"]);
                }

                if (string.IsNullOrEmpty(Request["hidden_TransDate_To"]))
                {
                    param.TransDate_To = null;
                }
                else
                {
                    param.TransDate_To = DateTime.Parse(Request["hidden_TransDate_To"]);
                }

            }



            return param;
        }

        public ActionResult ListPartial()
        {
            int userId = (int)Session["userId"];
            var param = GetParam(Request);

            var viewModel = GetListModel();
            ProcessCustomBinding(userId, viewModel, param);
            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Paging
        public ActionResult ListPaging(GridViewPagerState pager)
        {
            int userId = (int)Session["userId"];
            var param = GetParam(Request);

            var viewModel = GetListModel();
            viewModel.ApplyPagingState(pager);
            ProcessCustomBinding(userId, viewModel, param);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Filtering
        public ActionResult ListFiltering(GridViewFilteringState filteringState)
        {
            int userId = (int)Session["userId"];
            var param = GetParam(Request);

            var viewModel = GetListModel();
            viewModel.ApplyFilteringState(filteringState);
            ProcessCustomBinding(userId, viewModel, param);
            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }

        // Sorting
        public ActionResult ListSorting(GridViewColumnState column, bool reset)
        {
            int userId = (int)Session["userId"];
            var param = GetParam(Request);

            var viewModel = GetListModel();
            viewModel.ApplySortingState(column, reset);
            ProcessCustomBinding(userId, viewModel, param);

            return PartialView(VIEW_LIST_PARTIAL, viewModel);
        }



        static GridViewModel GetListModel()
        {
            var viewModel = GridViewExtension.GetViewModel("gvPaymentInList");
            if (viewModel == null)
            {
                var listMode = new PaymentIn__List_Model(); 
                viewModel = listMode.CreateGridViewModel(); 
            }

            return viewModel;
        }

        static void ProcessCustomBinding(int userId, GridViewModel viewModel, ListFindParam param)
        {
            var listMode = new PaymentIn__List_Model();
            listMode.SetBindingData(viewModel, userId, param);


        }

        public ActionResult PopupListLoadOnDemandPartial()
        {
            int userId = (int)Session["userId"];
            var param = GetParam(Request);

            var viewModel = GetListModel();
            ProcessCustomBinding(userId, viewModel, param);

            return PartialView(VIEW_PANEL_LIST_PARTIAL, viewModel);
        }

    }
}