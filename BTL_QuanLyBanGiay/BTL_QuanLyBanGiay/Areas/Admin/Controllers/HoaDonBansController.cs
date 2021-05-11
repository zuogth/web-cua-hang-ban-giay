using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;
using PagedList;

namespace BTL_QuanLyBanGiay.Areas.Admin.Controllers
{
    public class HoaDonBansController : Controller
    {
        private CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();

        // GET: Admin/HoaDonBans
        public ActionResult Index(int?page)
        {
            int pageSize = 6;
            int pageNumber = (page??1);
            var hoaDonBans = db.HoaDonBans.Include(h => h.KhachHang);
            return View(hoaDonBans.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HoaDonBans/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonBan);
        }

        // GET: Admin/HoaDonBans/Create
        public ActionResult Create()
        {
            ViewBag.MaKhach = new SelectList(db.KhachHangs, "MaKhach", "TenKhach");
            return View();
        }

        // POST: Admin/HoaDonBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoHDB,NgayBan,MaKhach,TongTien")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonBans.Add(hoaDonBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhach = new SelectList(db.KhachHangs, "MaKhach", "TenKhach", hoaDonBan.MaKhach);
            return View(hoaDonBan);
        }

        // GET: Admin/HoaDonBans/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhach = new SelectList(db.KhachHangs, "MaKhach", "TenKhach", hoaDonBan.MaKhach);
            return View(hoaDonBan);
        }

        // POST: Admin/HoaDonBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoHDB,NgayBan,MaKhach,TongTien")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhach = new SelectList(db.KhachHangs, "MaKhach", "TenKhach", hoaDonBan.MaKhach);
            return View(hoaDonBan);
        }

        // GET: Admin/HoaDonBans/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonBan);
        }

        // POST: Admin/HoaDonBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            db.HoaDonBans.Remove(hoaDonBan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
