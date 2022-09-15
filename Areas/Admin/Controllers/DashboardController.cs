using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRentService.Areas.Admin.Customs;
using CarRentService.Areas.Admin.Interfaces;

namespace CarRentService.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("admin")]

    [ServerAuthorize]
    public class DashboardController : Controller {

        private readonly IUserService _userService;

        public DashboardController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet, Route("dashboard", Name = "AdminDashboard")]
        public async Task<IActionResult> Dashboard() {
            var users = await _userService.GetAll();
            return View(users);
        }
    }
}
