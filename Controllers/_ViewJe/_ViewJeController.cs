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
using Models._PetaPoco;


using System.Net;
using Models._ViewJe;



namespace Controllers._ViewJe
{
    //public partial class _ViewJeController : BaseController
    //{

    //    string VIEW_PANEL_PARTIAL = "Partial/_ViewJePanel";
    //    string VIEW_MASTER_LIST_PARTIAL = "Partial/_ViewJeGvMasterPartial";

    //    public ActionResult getResult(string viewName)
    //    {

    //        string code = Request["hidden_Code"];
    //        long id = long.Parse(Request["hidden_Id"]);

    //        var viewModel = ViewJeModel.GetMasterList(code, id);


    //        var ssqlDataTable = PetaPoco.Sql.Builder
    //               .Append("SELECT * FROM [" + DbProvider.dbSap_Name + "].DBO.ODIM T0 ORDER BY T0.DimCode"
    //           );
    //        var dtDim = PetaPocoIduSqlRsExtensionsApp.IduGetDataTable(ssqlDataTable);
    //        ViewBag.DtDim = dtDim;

    //        return PartialView(viewName, viewModel);
    //    }

    //    public ActionResult PopupListLoadOnDemandPartial()
    //    {

    //        return getResult(VIEW_PANEL_PARTIAL);
    //    }


    //    public ActionResult MasterDetailMasterPartial()
    //    {
    //        return getResult(VIEW_MASTER_LIST_PARTIAL);
    //    }



    //}
}