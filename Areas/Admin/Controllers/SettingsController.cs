using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRentService.Areas.Admin.Customs;
using CarRentService.Areas.Admin.Interfaces;

namespace CarRentService.Areas.Admin.Controllers {

    [Area("Admin")]
    [Route("admin")]
    [ServerAuthorize]
    public class SettingsController : Controller {

        private readonly ISettings _settings;
        private readonly UserManager<ApplicationUser> _userManager;

        public SettingsController(ISettings settings, UserManager<ApplicationUser> userManager) {
            _settings = settings;
            _userManager = userManager;
        }

        [HttpGet, Route("change-password")]
        public IActionResult ChangePassword() {
            var changePassword = new ChangePassword();
            return View(changePassword);
        }

        [HttpPost, Route("change-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword) {
            if (ModelState.IsValid) {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _settings.ChnagePassword(changePassword, userId);
                if (result.Succeeded) {
                    TempData["ChangePasswordSucceded"] = "Password has been changed successfully!";
                    return RedirectToAction("ChangePassword", "Settings", new { area = "Admin" });
                } else {
                    TempData["CurrentPasswordError"] = "Entered password is mismatched to exist password!";
                    return RedirectToAction("ChangePassword", "Settings", new { area = "Admin" });
                }
            }
            return View(changePassword);
        }


        [HttpGet, Route("profile-setting")]
        public async Task<IActionResult> ProfileSettings() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _settings.GetEditAbleProfile(userId);
            return View(result);
        }


        [HttpPost, Route("profile-setting")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfileSettings(ChangeProfileDetails changeProfileDetails) {
            if (ModelState.IsValid) {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _settings.ChangeProfileDetails(changeProfileDetails, userId);
                if (result.Succeeded) {
                    TempData["ProfileDetailsChangeSuccess"] = "Personal data successfully updated!";
                    return RedirectToAction("ProfileSettings", "Settings", new { area = "Admin" });
                } else {
                    TempData["ProfileDetailsChangeFailed"] = "Something went wrong try again please!";
                    return RedirectToAction("ProfileSettings", "Settings", new { area = "Admin" });
                }
            }
            return View(changeProfileDetails);
        }
    }
}
