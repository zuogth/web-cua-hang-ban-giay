using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;
using PagedList;

namespace BTL_QuanLyBanGiay.Areas.Admin.Controllers
{
    public class ContactsController : BaseController
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        // GET: Admin/Contacts
        public ActionResult Contacts(int?page)
        {
            int pageSize = 12;
            int pageNumber = page ?? 1;
            List<Contact> lct = db.Contacts.OrderBy(x => x.MaContact).ToList();
            if(lct.Count==0)
            {
                ViewBag.Err = "Không có contact nào từ khách hàng";
            }    
            return View(lct.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Index(int MaContact)
        {
            Contact ct = db.Contacts.SingleOrDefault(x => x.MaContact == MaContact);
            Session["MaContact"] = MaContact;
            return View(ct);
        }
        public class MailInfo
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
        [HttpPost]
        public ActionResult SendMail()
        {
            
            string From = Request["From"];
            string Password = Request["Password"];
            string To = Request["To"];
            string Subject = Request["Subject"];
            string Body = Request["Body"];

            var mail = new SmtpClient("smtp.gmail.com", 587);
            {
                mail.Credentials = new NetworkCredential(From, Password);
                mail.EnableSsl = true;
            };
            var message = new MailMessage();
            message.From = new MailAddress(From);
            message.ReplyToList.Add(From);
            message.To.Add(new MailAddress(To));

            message.Subject = Subject;
            message.Body = Body;
            mail.Send(message);
            int mact =(int)Session["MaContact"];
            Contact ct = db.Contacts.SingleOrDefault(x => x.MaContact == mact);
            ct.Comfirm = 1;
            db.SaveChanges();
            return RedirectToAction("Contacts");
        }
    }
}