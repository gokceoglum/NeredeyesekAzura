using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Mail
    {
        public void MailSender(string body, string tomail)
        {
            var fromAddress = new MailAddress("afiyetolsun.neredeyesek@gmail.com");
            //var toAddress = new MailAddress("mustafa.gokceoglu14@gmail.com");
            var toAddress = new MailAddress(tomail);
            const string subject = "Nerede Yesek | Bugünkü Restaurantınız";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "neredeyesek1")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}