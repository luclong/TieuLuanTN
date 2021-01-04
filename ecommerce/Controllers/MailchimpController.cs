using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailChimp;
using MailChimp.Helper;
using MailChimp.Lists;
using ecommerce.Models;
using System.Net;

namespace ecommerce.Controllers
{
    public class MailchimpController : Controller
    {
        string MailChimpAPIKey = System.Configuration.ConfigurationManager.AppSettings["MailChimpAPIKey"];
        string MailChimpAPIKeySubsribeListID = System.Configuration.ConfigurationManager.AppSettings["MailChimpAPIKeySubsribeListID"];
        // GET: Mailchimp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSubscribe(FormCollection frc)
        {
            string userEmail = frc["subscribe"];
            MailChimpManager mc = new MailChimpManager(MailChimpAPIKey);
            EmailParameter email = new EmailParameter()
            {
                Email = userEmail
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            EmailParameter results = mc.Subscribe(MailChimpAPIKeySubsribeListID, email);
            Message msg = new Message
            {
                TextMessage = "Cảm ơn bạn đã đăng ký nhận thông tin khuyến mãi của chúng tôi"
            };
            return View("~/Views/Home/TinTuc.cshtml",msg);
        }
    }
}