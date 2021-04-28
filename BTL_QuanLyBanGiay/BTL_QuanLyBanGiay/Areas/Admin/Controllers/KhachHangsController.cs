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
    public class KhachHangsController : Controller
    {
        private CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();

        // GET: Admin/KhachHangs
        public ActionResult Index(int?page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(db.KhachHangs.ToList().ToPagedList(pageNumber,pageSize));
        }
       
        // GET: Admin/KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhach,TenKhach,Email,DiaChi,DienThoai")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                if(db.KhachHangs.Find(khachHang.MaKhach)!=null)
                {
                    ViewBag.Err = "Mã khách đã tồn tại";
                }
                else
                {
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }               
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Edit/5
        public ActionResult Edit(int id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhach,TenKhach,Email,DiaChi,DienThoai")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Delete/5
        public ActionResult Delete(int id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
