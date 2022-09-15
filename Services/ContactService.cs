using CarRentService.Interfaces;
using DataModel.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CarRentService.Services {
    public class ContactService : IContact{

        public readonly IConfiguration _config;

        public ContactService(IConfiguration config) {
            _config = config;
        }

        public string SendMail(SendMail sendMail) {
            var mailMessege = new MailMessage();
            mailMessege.To.Add(_config.GetSection("SMTP").GetSection("Mail").Value);
            mailMessege.Subject = sendMail.Subject;
            mailMessege.Body = $"Contact: {sendMail.PhoneNumber}<br/><br/><br/>" +sendMail.Message;
            mailMessege.From = new MailAddress(_config.GetSection("SMTP").GetSection("Mail").Value);
            mailMessege.IsBodyHtml = true;
            mailMessege.ReplyToList.Add(sendMail.Email);

            var smtp = new SmtpClient(_config.GetSection("SMTP").GetSection("Host").Value, Int32.Parse(_config.GetSection("SMTP").GetSection("Port").Value));
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_config.GetSection("SMTP").GetSection("Mail").Value, _config.GetSection("SMTP").GetSection("Password").Value);
            smtp.Send(mailMessege);

            return "MailSendingCompleted";
        }
    }
}
