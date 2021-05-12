using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;

namespace BTL_QuanLyBanGiay.Controllers
{
    public class ContactController : Controller
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        // GET: Contact
        public ActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddContact(Contact ct)
        {
            if(ModelState.IsValid)
            {
                Contact contact = new Contact()
                {
                    DienThoai = ct.DienThoai,
                    Email = ct.Email,
                    HoTen = ct.HoTen,
                    Subject = ct.Subject,
                    NoiDung = ct.NoiDung
                };
                db.Contacts.Add(contact);
                db.SaveChanges();
            }    
            return Json(new { Success=true},JsonRequestBehavior.AllowGet);
        }
    }
}