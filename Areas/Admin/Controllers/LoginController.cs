using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModel.ViewModels;
using CarRentService.Areas.Admin.Interfaces;

namespace CarRentService.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("admin")]
    public class LoginController : Controller {

        private readonly IAdminLogin _adminLogin;

        public LoginController(IAdminLogin adminLogin) {
            _adminLogin = adminLogin;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult AdminLogin() {
            return View();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginModel loginModel, string to) {
            if (ModelState.IsValid) {
                var result = await _adminLogin.LogIn(loginModel);
                if (result is not null) {
                    if (result == "InvalidCredentialError") {
                        TempData["InvalidCredentialError"] = "Invalid credentials provided!";
                        return View();
                    }
                    if (result == "UserDeactivated") {
                        TempData["UserDeactivated"] = "You can not login due to inactive your account. Please contact to support for enabling your account again. Thanks.";
                    }
                    if (result == "Admin") {
                        HttpContext.Response.Cookies.Append("logedin", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                        if (!string.IsNullOrEmpty(to)) {
                            return LocalRedirect(to);
                        } else {
                            return RedirectToAction("Dashboard", "Dashboard", new { area = "Admin" });
                        }
                    }
                }
            }
            return View();
        }
    }
}
