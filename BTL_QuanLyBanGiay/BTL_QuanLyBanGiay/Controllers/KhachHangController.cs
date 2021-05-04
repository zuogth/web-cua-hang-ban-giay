using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;

namespace BTL_QuanLyBanGiay.Controllers
{
    public class KhachHangController : Controller
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        // GET: User
        [HttpGet]
        public ActionResult Edit(int MaKhach)
        {
            if(MaKhach==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.KhachHangs.Find(MaKhach);
            if(kh==null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }
        [HttpPost]
        public ActionResult Edit(KhachHang kh)
        {
            if(ModelState.IsValid)
            {
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["KhachHang"] = kh.TenKhach;
            }
            return View(kh);
        }
        public ActionResult HisOrder(int MaKhach)
        {
            List<HoaDonBan> hdb = db.HoaDonBans.Where(x => x.MaKhach == MaKhach).OrderBy(x=>x.NgayBan).ToList();
            if(hdb.Count==0)
            {
                ViewBag.Err = "Quý khách chưa có hóa đơn nào";
            }
            return View(hdb);
        }
        public PartialViewResult HisProdOrder(int SoHDB)
        {
            return PartialView(db.ChiTietHDBs.Where(x => x.SoHDB == SoHDB).OrderBy(x => x.MaSP).ToList());
        }
        [HttpPost]
        public JsonResult AddDanhGia(DanhGia dg)
        {
            if(ModelState.IsValid)
            {
                DanhGia danhGia=new DanhGia();
                danhGia.MaKhach = (int)Session["MaKhach"];
                danhGia.TenSP = dg.TenSP;
                danhGia.NoiDung = dg.NoiDung;
                danhGia.NgayDG = DateTime.Now;
                danhGia.DanhGiaStar = dg.DanhGiaStar;
                db.DanhGias.Add(danhGia);
                db.SaveChanges();
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(KhachHang kh)
        {
            KhachHang khach = db.KhachHangs.SingleOrDefault(x => x.DienThoai == kh.DienThoai);
            if(khach==null || khach.Password==null)
            {
                ViewBag.Err = "Tài khoản không tồn tại";
                return View();
            }
            if(kh.Password!=khach.Password.Trim())
            {
                ViewBag.Err = "Mật khẩu không chính xác";
                return View();
            }
            Session["KhachHang"] = khach.TenKhach;
            Session["MaKhach"] = khach.MaKhach;
            return RedirectToAction("Shop","Home");
        }
        public ActionResult Logout()
        {
            Session.Remove("KhachHang");
            Session.Remove("MaHang");
            return RedirectToAction("Login");
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(KhachHang kh)
        {
            KhachHang khach = db.KhachHangs.SingleOrDefault(x => x.DienThoai == kh.DienThoai);
            if(khach==null)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                if(khach.Password!=null)
                {
                    ViewBag.Err = "Tài khoản đã tồn tại";
                    return View();
                }
                else
                {
                    khach.Password = kh.Password;
                    db.Entry(khach).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
        }
    }
}