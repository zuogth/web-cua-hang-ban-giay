using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;
using PagedList;

namespace BTL_QuanLyBanGiay.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        // GET: Admin/Index
        public ActionResult Products(int?page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            List<SanPham> lsp = db.SanPhams.OrderBy(x => x.MaSP).ToList();
            if(lsp.Count==0)
            {
                ViewBag.Err = "Khong co san pham nao";
            }
            return View(lsp.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Details(string MaSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux.ToList().OrderBy(x => x.TenChatLieu), "MaChatLieu", "TenChatLieu");
            ViewBag.MaCo = new SelectList(db.Coes.ToList().OrderBy(x => x.TenCo), "MaCo", "TenCo");
            ViewBag.MaDoiTuong = new SelectList(db.DoiTuongs.ToList().OrderBy(x =>x.TenDoiTuong), "MaDoiTuong", "TenDoiTuong");
            ViewBag.MaMau = new SelectList(db.Maus.ToList().OrderBy(x => x.TenMau), "MaMau", "TenMau");
            ViewBag.MaNuocSX = new SelectList(db.NuocSanXuats.ToList().OrderBy(x =>x.TenNuocSX), "MaNuocSX", "TenNuocSX");
            ViewBag.MaLoai = new SelectList(db.TheLoais.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(HttpPostedFileBase Anh, [Bind(Include ="MaSP,TenSP,MaLoai,MaCo,MaChatLieu,MaMau,MaDoiTuong,MaNuocSX,SoLuong,DonGiaNhap,DonGiaBan,Anh")]SanPham sp)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(Anh.FileName);
                string extension = Path.GetExtension(Anh.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                sp.Anh = fileName;
                string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                Anh.SaveAs(path);
                db.SanPhams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Edit(string MaSP)
        {
            if(MaSP==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.Find(MaSP);
            if(sp==null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux.ToList().OrderBy(x => x.TenChatLieu), "MaChatLieu", "TenChatLieu");
            ViewBag.MaCo = new SelectList(db.Coes.ToList().OrderBy(x => x.TenCo), "MaCo", "TenCo");
            ViewBag.MaDoiTuong = new SelectList(db.DoiTuongs.ToList().OrderBy(x => x.TenDoiTuong), "MaDoiTuong", "TenDoiTuong");
            ViewBag.MaMau = new SelectList(db.Maus.ToList().OrderBy(x => x.TenMau), "MaMau", "TenMau");
            ViewBag.MaNuocSX = new SelectList(db.NuocSanXuats.ToList().OrderBy(x => x.TenNuocSX), "MaNuocSX", "TenNuocSX");
            ViewBag.MaLoai = new SelectList(db.TheLoais.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai");
            return View(sp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase Anh, [Bind(Include = "MaSP,TenSP,MaLoai,MaCo,MaChatLieu,MaMau,MaDoiTuong,MaNuocSX,SoLuong,DonGiaNhap,DonGiaBan,Anh")] SanPham sp)
        {
            if(ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(Anh.FileName);
                string extension = Path.GetExtension(Anh.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                sp.Anh = fileName;
                string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                Anh.SaveAs(path);
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            return RedirectToAction("Products");
        }
        [HttpGet]
        public ActionResult Delete(string MaSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult ComfirmDelete(string MaSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            var anh = from p in db.AnhSPs
                      where p.TenSP == sp.TenSP
                      select p;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.AnhSPs.RemoveRange(anh);
            db.SanPhams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Products");
        }
    }
}