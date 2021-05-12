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
    public class ChiTietHDNsController : BaseController
    {
        private CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();

        // GET: Admin/ChiTietHDNs
        public PartialViewResult Index(string id)
        {
            List<ChiTietHDN> lst = db.ChiTietHDNs.Where(x => x.SoHDN == id).OrderBy(x => x.MaSP).ToList();
            return PartialView(lst);
        }

        // GET: Admin/ChiTietHDBs/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoHDN,MaSP,SoLuong,DonGia,GiamGia,ThanhTien")] ChiTietHDN chiTietHDN)
        {
            if (ModelState.IsValid)
            {
                if (db.ChiTietHDNs.Find(chiTietHDN.SoHDN, chiTietHDN.MaSP) != null)
                {
                    ViewBag.Err = "Sản phẩm đã tồn tại";
                }
                else
                {
                    db.ChiTietHDNs.Add(chiTietHDN);
                    db.SaveChanges();
                    return RedirectToAction("Details", "HoaDonNhaps", new { id = chiTietHDN.SoHDN });
                }
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHDN.MaSP);
            return View(chiTietHDN);
        }

        // GET: Admin/ChiTietHDBs/Delete/5
        public ActionResult Delete(string SoHDN, string MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(SoHDN, MaSP);
            if (chiTietHDN == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHDN);
        }

        // POST: Admin/ChiTietHDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string SoHDN, string MaSP)
        {
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(SoHDN, MaSP);
            db.ChiTietHDNs.Remove(chiTietHDN);
            db.SaveChanges();
            return RedirectToAction("Details", "HoaDonNhaps", new { id = chiTietHDN.SoHDN });
        }
    }
}
