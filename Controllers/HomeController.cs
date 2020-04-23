using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace  Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //Session["userId"] = "suhut";

            return View();    
        }
        
     
    }
}