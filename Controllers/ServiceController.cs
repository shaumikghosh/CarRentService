using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentService.Controllers {
    public class ServiceController : Controller {

        [HttpGet, Route("our-service")]
        public IActionResult Services() {
            return View();
        }
    }
}
