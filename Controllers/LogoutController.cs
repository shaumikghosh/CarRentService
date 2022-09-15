using Microsoft.AspNetCore.Mvc;
using CarRentService.Interfaces;

namespace CarRentService.Controllers.Clientside {
    public class LogoutController : Controller {

        private readonly ILogout _logout;

        public LogoutController(ILogout logout) {
            _logout = logout;
        }

        [HttpGet]
        [Route("/user/logout")]
        public IActionResult Logout() {
            if (User.Identity.IsAuthenticated) {
                _logout.LogoutUser();
                return RedirectToAction("Index", "Home");
            } else {
                return Content("Don't worry you are not authorized!");
            }
        }
    }
}
