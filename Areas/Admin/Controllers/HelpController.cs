using Microsoft.AspNetCore.Mvc;

namespace CarRentService.Areas.Admin.Controllers {
    public class HelpController : Controller {
        public IActionResult UserHelps() {
            return View();
        }
    }
}
