using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentService.Controllers {
    public class BlogController : Controller {

        [HttpGet, Route("blog")]
        public IActionResult Blog() {
            return View();
        }

        [HttpGet, Route("blog/{slug?}")]
        public IActionResult SingleBlog() {
            return View();
        }
    }
}
