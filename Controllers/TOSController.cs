using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentService.Controllers {
    public class TOSController : Controller {

        [HttpGet, Route("terms-of-service")]
        public IActionResult TermsAndService() {
            return View();
        }
    }
}
