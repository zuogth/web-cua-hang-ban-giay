using BTL_QuanLyBanGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_QuanLyBanGiay.Controllers
{
    public class CartController : Controller
    {
        CuaHangBanGiayEntities db = new CuaHangBanGiayEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(string MaSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (Session["cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { SanPham = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP), SoLuong = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["cart"];
                int index = isExist(MaSP);
                if (index != -1)
                {
                    cart[index].SoLuong++;
                }
                else
                {
                    cart.Add(new Cart { SanPham = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP), SoLuong = 1 });
                }
                Session["cart"] = cart;
            }
            Session["countCart"] = ((List<Cart>)Session["cart"]).Sum(x => x.SoLuong);
            return Json(new { Success = true,Counter=Session["countCart"],
                Money= ((List<Cart>)Session["cart"]).Sum(x => x.SoLuong*x.SanPham.DonGiaBan) },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(string id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["countCart"] = cart.Sum(x => x.SoLuong);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Plus(string MaSP)
        {
            double money,allMoney;
            int sl,count;
            List<Cart> cart = (List<Cart>)Session["cart"];
            int index = isExist(MaSP);
            cart[index].SoLuong++;

            Session["countCart"] = count =cart.Sum(x => x.SoLuong);
            Session["cart"] = cart;
            sl = cart[index].SoLuong;
            money =double.Parse((cart[index].SoLuong * cart[index].SanPham.DonGiaBan).ToString());
            allMoney = double.Parse(cart.Sum(x => x.SoLuong * x.SanPham.DonGiaBan).ToString());
            return Json(new { Success = true,Money= money,TotalM=allMoney,SoLuong=sl,Counter=count}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Minus(string MaSP)
        {
            double money,allMoney;
            int sl,count;
            List<Cart> cart = (List<Cart>)Session["cart"];
            int index = isExist(MaSP);
            cart[index].SoLuong--;
            Session["countCart"] =count= cart.Sum(x => x.SoLuong);
            Session["cart"] = cart;   
            sl = cart[index].SoLuong;
            money = double.Parse((cart[index].SoLuong * cart[index].SanPham.DonGiaBan).ToString());
            allMoney = double.Parse(cart.Sum(x => x.SoLuong * x.SanPham.DonGiaBan).ToString());
            return Json(new { Success = true, Money = money,TotalM=allMoney, SoLuong = sl,Counter=count }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Cart(string MaSP="SP01")
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            return PartialView(sp);
        } 
        private int isExist(string id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].SanPham.MaSP.Equals(id))
                    return i;
            return -1;
        }

    }
}