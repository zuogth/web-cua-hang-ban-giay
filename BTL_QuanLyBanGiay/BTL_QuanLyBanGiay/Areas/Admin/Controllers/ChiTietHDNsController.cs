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
    public class ChiTietHDNsController : Controller
    {
        private CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();

        // GET: Admin/ChiTietHDNs
        public ActionResult Index()
        {
            var chiTietHDNs = db.ChiTietHDNs.Include(c => c.HoaDonNhap).Include(c => c.SanPham);
            return View(chiTietHDNs.ToList());
        }

        // GET: Admin/ChiTietHDNs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(id);
            if (chiTietHDN == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHDN);
        }

        // GET: Admin/ChiTietHDNs/Create
        public ActionResult Create()
        {
            ViewBag.SoHDN = new SelectList(db.HoaDonNhaps, "SoHDN", "MaNV");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/ChiTietHDNs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoHDN,MaSP,SoLuong,DonGia,GiamGia,ThanhTien")] ChiTietHDN chiTietHDN)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHDNs.Add(chiTietHDN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoHDN = new SelectList(db.HoaDonNhaps, "SoHDN", "MaNV", chiTietHDN.SoHDN);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHDN.MaSP);
            return View(chiTietHDN);
        }

        // GET: Admin/ChiTietHDNs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(id);
            if (chiTietHDN == null)
            {
                return HttpNotFound();
            }
            ViewBag.SoHDN = new SelectList(db.HoaDonNhaps, "SoHDN", "MaNV", chiTietHDN.SoHDN);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHDN.MaSP);
            return View(chiTietHDN);
        }

        // POST: Admin/ChiTietHDNs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoHDN,MaSP,SoLuong,DonGia,GiamGia,ThanhTien")] ChiTietHDN chiTietHDN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHDN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SoHDN = new SelectList(db.HoaDonNhaps, "SoHDN", "MaNV", chiTietHDN.SoHDN);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHDN.MaSP);
            return View(chiTietHDN);
        }

        // GET: Admin/ChiTietHDNs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(id);
            if (chiTietHDN == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHDN);
        }

        // POST: Admin/ChiTietHDNs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(id);
            db.ChiTietHDNs.Remove(chiTietHDN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
