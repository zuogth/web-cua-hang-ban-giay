using BTL_QuanLyBanGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BTL_QuanLyBanGiay.Controllers
{
    public class HomeController : Controller
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult TheLoai()
        {
            return PartialView(db.TheLoais.OrderBy(x=>x.MaLoai).ToList());
        }
        public ViewResult Shop(int?page,string MaLoai)
        {
            int pageSize;
            int pageNumber;
            List<SanPham> lsp;
            if (MaLoai!=null)
            {
                pageSize = 6;
                pageNumber = (page ?? 1);
                lsp = db.SanPhams.Where(x => x.MaLoai == MaLoai).OrderBy(x => x.MaSP).ToList();
                lsp = lsp.GroupBy(x => x.TenSP).Select(grb => grb.First()).ToList();
                if (lsp.Count == 0)
                {
                    ViewBag.Err = "Khong co san pham nao";
                }
                return View(lsp.ToPagedList(pageNumber, pageSize));
            }
            pageSize = 6;
            pageNumber = (page ?? 1);
            lsp = db.SanPhams.OrderBy(x => x.TenSP).ToList();
            lsp = lsp.GroupBy(x => x.TenSP).Select(grb => grb.First()).ToList();
            if (lsp.Count == 0)
            {
                ViewBag.sanpham = "Khong co san pham nao";
            }                 
            return View(lsp.ToPagedList(pageNumber,pageSize));
        }
        public ViewResult XemChiTiet(string MaSP="SP07")
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }     
        public PartialViewResult AnhSP(string TenSP,string MaMau)
        {
            List<AnhSP> lasp = db.AnhSPs.Where(x => x.TenSP == TenSP && x.MaMau==MaMau).ToList();
            if(lasp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return PartialView(lasp);
        }
        public PartialViewResult Size(string TenSP,string MaMau)
        {
            List<SanPham> lsp = db.SanPhams.Where(x => x.TenSP == TenSP && x.MaMau == MaMau).OrderBy(x => x.Co.TenCo).ToList();
            return PartialView(lsp);
        }
        public PartialViewResult DanhGia(string TenSP)
        {
            List<DanhGia> ldg = db.DanhGias.Where(x => x.TenSP == TenSP).ToList();
            if(ldg.Count==0)
            {
                ViewBag.Err = "Không có đánh giá nào";
            }
            return PartialView(ldg);
        }
    }
}