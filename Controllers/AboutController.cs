using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentService.Controllers {
    public class AboutController : Controller {

        [HttpGet, Route("about-us")]
        public IActionResult About() {
            return View();
        }
    }
}
