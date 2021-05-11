using BTL_QuanLyBanGiay.Areas.Admin.Data;
using BTL_QuanLyBanGiay.Code;
using ModelWeb.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_QuanLyBanGiay.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            using(BanGiayDbContext context=new BanGiayDbContext())
            {
                var user = context.TaiKhoans.Where(x => x.UserName == loginModel.UserName).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.Err = "Tài khoản không tồn tại";
                    return View();
                }
                if (user.PassWord.Trim() != Encryptor.MD5Hash(loginModel.PassWord))
                {
                    ViewBag.Err = "Sai mật khẩu";
                    return View();
                }
                Session["Username"] = user.UserName;
                return RedirectToAction("Products", "Products");
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(LoginModel loginModel,string reg)
        {
            using(BanGiayDbContext context=new BanGiayDbContext())
            {
                try
                {
                    var user = context.TaiKhoans.Add(new TaiKhoan
                    {
                        UserName = loginModel.UserName,
                        PassWord =Encryptor.MD5Hash(loginModel.PassWord)
                    });
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.Err = ex.Message;
                    return View();
                }
            }          
        }
        public ActionResult Logout()
        {
            Session.Remove("UserName");
            return RedirectToAction("Login");
        }
    }
}