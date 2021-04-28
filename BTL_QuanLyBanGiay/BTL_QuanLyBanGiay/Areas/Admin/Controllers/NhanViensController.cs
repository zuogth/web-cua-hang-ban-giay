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
    public class NhanViensController : Controller
    {
        private CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();

        // GET: Admin/NhanViens
        public ActionResult Index(int ? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var nhanViens = db.NhanViens.Include(n => n.CongViec);
            return View(nhanViens.ToList().ToPagedList(pageNumber,pageSize));
        }

        // GET: Admin/NhanViens/Details/5
        public ActionResult Details(string MaNV)
        {
            if (MaNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(MaNV);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaCV = new SelectList(db.CongViecs, "MaCV", "TenCV");
            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkMaNV=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenNV,GioiTinh,NgaySinh,DienThoai,DiaChi,MaCV")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                if(db.NhanViens.Find(nhanVien.MaNV)!=null)
                {
                    ViewBag.Err = "Mã nhân viên đã tồn tại";
                }
                else
                {
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                             
            }

            ViewBag.MaCV = new SelectList(db.CongViecs, "MaCV", "TenCV", nhanVien.MaCV);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Edit/5
        public ActionResult Edit(string MaNV)
        {
            if (MaNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(MaNV);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCV = new SelectList(db.CongViecs, "MaCV", "TenCV", nhanVien.MaCV);
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkMaNV=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TenNV,GioiTinh,NgaySinh,DienThoai,DiaChi,MaCV")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCV = new SelectList(db.CongViecs, "MaCV", "TenCV", nhanVien.MaCV);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Delete/5
        public ActionResult Delete(string MaNV)
        {
            if (MaNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(MaNV);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string MaNV)
        {
            NhanVien nhanVien = db.NhanViens.Find(MaNV);
            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
