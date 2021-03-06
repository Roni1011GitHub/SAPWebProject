﻿using System;
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


 
namespace Controllers.Authentication
{

    public partial class LogoutController : Controller
    { 
      
        public ActionResult Logout()
        {
            Session.Remove("userId");
            Session.Remove("userName");
            Session.Remove("roleName");
            Session.Remove("isAdmin");
            Session.Remove("branchCode");
            Session.Remove("branchName");
            Session.Remove("IsPassExpired");
            Session.Remove("lastController");

            Session.Remove("MenuNodes"); 

            

            return RedirectToAction("Index","Home"); 
        }

        

    }
}