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
    public class HoaDonNhapsController : Controller
    {
        private CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();

        // GET: Admin/HoaDonNhaps
        public ActionResult Index()
        {
            var hoaDonNhaps = db.HoaDonNhaps.Include(h => h.NhaCungCap).Include(h => h.NhanVien);
            return View(hoaDonNhaps.ToList());
        }

        // GET: Admin/HoaDonNhaps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV");
            return View();
        }

        // POST: Admin/HoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoHDN,MaNV,NgayNhap,MaNCC,TongTien")] HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonNhaps.Add(hoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", hoaDonNhap.MaNCC);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", hoaDonNhap.MaNV);
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", hoaDonNhap.MaNCC);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", hoaDonNhap.MaNV);
            return View(hoaDonNhap);
        }

        // POST: Admin/HoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoHDN,MaNV,NgayNhap,MaNCC,TongTien")] HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", hoaDonNhap.MaNCC);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", hoaDonNhap.MaNV);
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonNhap);
        }

        // POST: Admin/HoaDonNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            db.HoaDonNhaps.Remove(hoaDonNhap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
