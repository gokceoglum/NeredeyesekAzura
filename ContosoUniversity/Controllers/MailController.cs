using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace mailgonder.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MailContact model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Ad & Soyad: " + model.Name);
                body.AppendLine("E-Mail Adresi: " + model.Email);
                body.AppendLine("Konu: " + model.Subject);
                body.AppendLine("Mesaj: " + model.Message);
                Mail mail = new Mail();
                mail.MailSender(body.ToString(), model.Email);
            }
            return View();
        }
    }
}