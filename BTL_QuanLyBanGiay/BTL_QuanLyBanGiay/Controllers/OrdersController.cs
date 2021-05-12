 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_QuanLyBanGiay.Models;

namespace BTL_QuanLyBanGiay.Controllers
{
    public class OrdersController : Controller
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        // GET: Orders
        public ActionResult Index([Bind(Include ="MaKhach,TenKhach,Email,DienThoai,DiaChi")] KhachHang khach)
        {
            bool ex = false;
            int sohdb;
            if(ModelState.IsValid)
            {
                KhachHang exist = db.KhachHangs.SingleOrDefault(x=>x.DienThoai==khach.DienThoai);
                if(exist==null)
                {
                    ex = true;
                    db.KhachHangs.Add(khach);
                    db.SaveChanges();
                }
                List<Cart> listcart = Session["cart"] as List<Cart>;
                HoaDonBan donBan = new HoaDonBan()
                {
                    MaKhach = ex==true?khach.MaKhach:exist.MaKhach,
                    NgayBan = DateTime.Now,
                    TongTien = listcart.Sum(x => x.SoLuong * x.SanPham.DonGiaBan)
                };
                db.HoaDonBans.Add(donBan);
                db.SaveChanges();
                sohdb = donBan.SoHDB;
                foreach (var item in listcart)
                {
                    ChiTietHDB chiTiet = new ChiTietHDB();
                    ChiTietHDB ctexist = db.ChiTietHDBs.FirstOrDefault(x => x.SanPham.TenSP == item.SanPham.TenSP);
                    if(ctexist != null)
                    {
                        HoaDonBan hbexist = db.HoaDonBans.FirstOrDefault(x => x.SoHDB == ctexist.SoHDB && x.MaKhach == donBan.MaKhach);
                        if (hbexist != null)
                        {
                            chiTiet.SoHDB = sohdb;
                            chiTiet.MaSP = item.SanPham.MaSP;
                            chiTiet.SoLuong = item.SoLuong;
                            chiTiet.MaDG = ctexist.MaDG;
                        }
                        else
                        {
                            chiTiet.SoHDB = sohdb;
                            chiTiet.MaSP = item.SanPham.MaSP;
                            chiTiet.SoLuong = item.SoLuong;
                        }
                    }                                      
                    else
                    {
                        chiTiet.SoHDB = sohdb;
                        chiTiet.MaSP = item.SanPham.MaSP;
                        chiTiet.SoLuong = item.SoLuong;
                    }
                    db.ChiTietHDBs.Add(chiTiet);
                    db.SaveChanges();
                }
            }
            

            return RedirectToAction("Shop", "Home");
        }
    }
}