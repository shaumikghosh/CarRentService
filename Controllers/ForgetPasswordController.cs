using DataModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRentService.Interfaces;

namespace CarRentService.Controllers {
    public class ForgetPasswordController : Controller {

        private readonly IForgetPassword _forgetPassword;

        public ForgetPasswordController(IForgetPassword forgetPassword) {
            _forgetPassword = forgetPassword;
        }

        [HttpGet, Route("forget-password")]
        public IActionResult ForgetPassword() {
            return View();
        }

        [HttpPost, Route("forget-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordEmail model) {
            if (ModelState.IsValid) {
                var result = await _forgetPassword.ForgetPasswordEmail(model);
                if (result == "NotFoundUser") {
                    TempData["NotFoundUser"] = "No user found accossiated with this email!";
                } else {
                    TempData["MailSentReport"] = "Email with token has been sent to you mail please check!";
                }
            }
            return View(model);
        }

        [HttpGet, Route("reset-password/{token}")]
        public async Task<IActionResult> ResetPassword(string token) {
            var result = await _forgetPassword.CheckToken(token);
            if(result == true) {
                ViewBag.TokenError = true;
            }
            return View();
        }

        [HttpPost, Route("reset-password/{token}")]
        public async Task<IActionResult> ResetPassword(string token, ChangeForgotPassword model) {
            if (ModelState.IsValid) {
                var result = await _forgetPassword.ResetPassword(token, model);
                if (result is not null) {
                    if (result == "InvalidTokenError") {
                        TempData["InvalidTokenError"] = "Make sure you did not modify any single letter from token to change password!";
                    } else {
                        if (result == "PasswordResetSuccess") {
                            TempData["PasswordResetSuccess"] = "password has been reset successfully you can login with your new password!";
                            return RedirectToAction("UserLogin", "Login");
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
