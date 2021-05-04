using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;
using PagedList;

namespace BTL_QuanLyBanGiay.Controllers
{
    public class SearchController : Controller
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        // GET: Search
        [HttpPost]
        public ActionResult Search(FormCollection f,int ? page)
        {
            string search = f["txtSearch"].ToString();
            ViewBag.keyW = search;
            List<SanPham> lsp = db.SanPhams.Where(x => x.TenSP.Contains(search)).ToList();
            lsp = lsp.GroupBy(x => x.TenSP).Select(grb => grb.First()).ToList();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if(lsp.Count==0)
            {
                ViewBag.Err = "Khong tim thay san pham nao";
                return View(lsp.ToPagedList(pageNumber, pageSize));
            }
            return View(lsp.OrderBy(x=>x.TenSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Search(int?page,string search)
        {
            ViewBag.keyW = search;
            List<SanPham> lsp = db.SanPhams.Where(x => x.TenSP.Contains(search)).ToList();
            lsp = lsp.GroupBy(x => x.TenSP).Select(grb => grb.First()).ToList();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (lsp.Count == 0)
            {
                ViewBag.Err = "Khong tim thay san pham nao";
                return View(lsp.ToPagedList(pageNumber, pageSize));
            }
            return View(lsp.OrderBy(x => x.TenSP).ToPagedList(pageNumber, pageSize));
        }
    }
}