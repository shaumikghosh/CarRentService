using Microsoft.AspNetCore.Mvc;
using CarRentService.Areas.Admin.Customs;
using CarRentService.Areas.Admin.Interfaces;

namespace CarRentService.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("admin")]
    [ServerAuthorize]
    public class LogoutController : Controller {
        private readonly IAdminLogout _adminLogout;

        public LogoutController(IAdminLogout adminLogout) {
            _adminLogout = adminLogout;
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout() {
            if (User.Identity.IsAuthenticated) {
                _adminLogout.LogoutUser();
                return RedirectToAction("AdminLogin", "Login", new { area = "Admin" });
            }
            return null;
        }
    }
}
