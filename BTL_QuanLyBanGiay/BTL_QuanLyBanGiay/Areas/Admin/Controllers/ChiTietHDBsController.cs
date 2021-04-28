using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;

namespace BTL_QuanLyBanGiay.Areas.Admin.Controllers
{
    public class ChiTietHDBsController : Controller
    {
        private CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();

        // GET: Admin/ChiTietHDBs
        public PartialViewResult Index(int id)
        {
            List<ChiTietHDB> lst = db.ChiTietHDBs.Where(x => x.SoHDB == id).OrderBy(x=>x.MaSP).ToList();
            return PartialView(lst);
        }

        // GET: Admin/ChiTietHDBs/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHDB chiTietHDB = db.ChiTietHDBs.Find(id);
            if (chiTietHDB == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHDB);
        }

        // GET: Admin/ChiTietHDBs/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/ChiTietHDBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoHDB,MaSP,GiamGia,SoLuong")] ChiTietHDB chiTietHDB)
        {
            if (ModelState.IsValid)
            {
                if(db.ChiTietHDBs.Find(chiTietHDB.SoHDB,chiTietHDB.MaSP)!=null)
                {
                    ViewBag.Err = "Sản phẩm đã tồn tại";
                }
                else
                {
                    db.ChiTietHDBs.Add(chiTietHDB);
                    db.SaveChanges();
                    return RedirectToAction("Details", "HoaDonBans",new {id=chiTietHDB.SoHDB });
                }                    
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHDB.MaSP);
            return View(chiTietHDB);
        }

        // GET: Admin/ChiTietHDBs/Delete/5
        public ActionResult Delete(int SoHDB,string MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHDB chiTietHDB = db.ChiTietHDBs.Find(SoHDB,MaSP);
            if (chiTietHDB == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHDB);
        }

        // POST: Admin/ChiTietHDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int SoHDB, string MaSP)
        {
            ChiTietHDB chiTietHDB = db.ChiTietHDBs.Find(SoHDB, MaSP);
            db.ChiTietHDBs.Remove(chiTietHDB);
            db.SaveChanges();
            return RedirectToAction("Details", "HoaDonBans", new { id = chiTietHDB.SoHDB });
        }
    }
}
