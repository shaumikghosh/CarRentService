using CarRentService.Interfaces;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentService.Controllers {
    public class ContactController : Controller {

        public readonly IContact _contact;
        public ContactController(IContact contact) {
            _contact = contact;
        }

        [HttpGet, Route("contact-us")]
        public IActionResult Contact() {
            return View();
        }

        [HttpPost, Route("contact-us")]
        public IActionResult Contact(SendMail sendMail) {
            var result = _contact.SendMail(sendMail);
            if (result == "MailSendingCompleted") {
                TempData["MessageDelivered"] = "We have recevied your mail, you and your mail is too important to us. We will contact you shortly!";
                return RedirectToAction("Contact", "Contact");
            } else {
                TempData["MessageNotDelivered"] = "Ooops! Someting went wrong please try again later.";
                return RedirectToAction("Contact", "Contact");
            }
        }
    }
}
