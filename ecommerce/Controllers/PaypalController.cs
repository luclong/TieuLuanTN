using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;

namespace ecommerce.Controllers
{
    public class PaypalController : Controller
    {
        DbecommerceEntities db = new DbecommerceEntities();
        // GET: Paypal
        public List<ItemGioHang> LayGioHang()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult Index()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            var ls = Session["GioHang"] as List<ItemGioHang>;
            return View(ls);
        }
        public ActionResult GetDataPaypal()
        {
            var getData = new GetDataPaypal();
            var order = getData.InformationOrder(getData.GetPayPalResponse(Request.QueryString["tx"]));
            if (order.PaymentStatus == "Completed")
            {
                List<ThanhVien> tv = Session["TaiKhoan"] as List<ThanhVien>; 
                ViewBag.TT = "Thanh Toán PayPal Thành Công";
                return View();
            }
            else
            {
                return RedirectToAction("FailPaypal");
            }
        }
        public ActionResult FailPaypal()
        {
            return View();
        }
    }
}