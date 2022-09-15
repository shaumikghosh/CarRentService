using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CarRentService.Controllers {
    public class HomeController : Controller {
        [HttpGet, Route("/")]
        public IActionResult Index() {
            return View();
        }
    }
}
