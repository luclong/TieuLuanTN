using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;
using MailChimp;
using MailChimp.Helper;

namespace ecommerce.Controllers
{
    public class QuanLyThanhVienController : Controller
    {
        // GET: QuanLyThanhVien
        DbecommerceEntities db = new DbecommerceEntities();
        public ActionResult Index()
        {
            var lstThanhVien = db.ThanhVien.Where(n => (n.MaLoaiTV == 3 || n.MaLoaiTV == 4) && n.GuiMailChimp==false);
            return View(lstThanhVien);
        }
        public ActionResult AddSubscribe(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien tv = db.ThanhVien.SingleOrDefault(n => n.MaTV == id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            string userEmail = tv.Email;
            MailChimpManager mc = new MailChimpManager(System.Configuration.ConfigurationManager.AppSettings["MailChimpAPIKey"]);
            EmailParameter email = new EmailParameter()
            {
                Email = userEmail
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            EmailParameter results = mc.Subscribe(System.Configuration.ConfigurationManager.AppSettings["MailChimpAPIKeySubsribeListID"], email);
            tv.GuiMailChimp = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}