using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_QuanLyBanGiay.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["Username"]==null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/admin/user/login");
            }
        }
    }
}