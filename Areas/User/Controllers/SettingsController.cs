using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRentService.Areas.User.Customs;
using CarRentService.Areas.User.Interfaces;
using CarRentService.Data;

namespace CarRentService.Areas.User.Controllers {
    [Area("User")]
    [Route("user")]
    [ClientAuthorize]
    public class SettingsController : Controller {

        private readonly IProfileSettings _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DatabaseContext _databaseContext;

        public SettingsController(IProfileSettings settings, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, SignInManager<ApplicationUser> signInManager, DatabaseContext databaseContext) {
            _settings = settings;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _databaseContext = databaseContext;
        }

        [HttpGet, Route("settings")]
        public async Task<IActionResult> UserSettings() {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user_data = await _userManager.Users.Where(user => user.Id == id).Include(x => x.UserAdditionalInfo).Include(x => x.AccountStatus).Include(x => x.VerifyAppusers).Include(x => x.FormSubmitted).Include(x => x.AccountStatus).Include(x => x.UserBankDetails).FirstOrDefaultAsync();

            ViewBag.FirstName = user_data.FirstName;
            ViewBag.LastName = user_data.LastName;
            ViewBag.Email = user_data.UserAdditionalInfo.AdditionalEmail;
            ViewBag.BankName = user_data.UserBankDetails.BankName;
            ViewBag.BankSwiftCode = user_data.UserBankDetails.BankSwiftCode;
            ViewBag.AccountNumber = user_data.UserBankDetails.BankAccountNumber;
            ViewBag.Phone = user_data.UserAdditionalInfo.PhoneNumber;
            ViewBag.Country = user_data.UserAdditionalInfo.Country;
            ViewBag.State = user_data.UserAdditionalInfo.State;
            ViewBag.City = user_data.UserAdditionalInfo.City;
            ViewBag.CityCode = user_data.UserAdditionalInfo.CountryCode;
            ViewBag.Zip = user_data.UserAdditionalInfo.Zip;
            ViewBag.StreetAddress = user_data.UserAdditionalInfo.StreetAddress;
            ViewBag.HouseNumber = user_data.UserAdditionalInfo.HouseNumber;
            ViewBag.ProfileImage = user_data.UserAdditionalInfo.ProfileImage;
            ViewBag.DrivingApprove = user_data.AccountStatus.AccountApproved;
            ViewBag.BankApprove = user_data.AccountStatus.BankApproved;
            ViewBag.VASID = user_data.VerifyAppusers.Count();
            ViewBag.VIFSubmitted = user_data.FormSubmitted.IdentityFormSubmit;
            ViewBag.BVFSubmitted = user_data.FormSubmitted.BankFormSubmit;
            ViewBag.IdentityImage = user_data.VerifyAppusers.Count();


            return View();
        }

        [HttpPost, Route("settings"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UserSettings(IFormCollection form) {
            if (ValidateUserDetailsForm(form) == true) {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _settings.ChangeProfileDetails(form, id);
                if (result == "UserDataUpdated") {
                    TempData["InfoUpdatedSuccess"] = "Your information is updated!";
                    return RedirectToAction("UserSettings", "Settings", new { area = "User" });
                }
            } else {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user_data = await _userManager.Users.Where(user => user.Id == id).Include(x => x.UserAdditionalInfo).Include(x => x.AccountStatus).Include(x => x.VerifyAppusers).Include(x => x.FormSubmitted).Include(x => x.AccountStatus).Include(x => x.UserBankDetails).FirstOrDefaultAsync();
                ViewBag.FirstName = user_data.FirstName;
                ViewBag.LastName = user_data.LastName;
                ViewBag.FirstName = user_data.FirstName;
                ViewBag.LastName = user_data.LastName;
                ViewBag.Email = user_data.UserAdditionalInfo.AdditionalEmail;
                ViewBag.BankName = user_data.UserBankDetails.BankName;
                ViewBag.BankSwiftCode = user_data.UserBankDetails.BankSwiftCode;
                ViewBag.AccountNumber = user_data.UserBankDetails.BankAccountNumber;
                ViewBag.Phone = user_data.UserAdditionalInfo.PhoneNumber;
                ViewBag.Country = user_data.UserAdditionalInfo.Country;
                ViewBag.State = user_data.UserAdditionalInfo.State;
                ViewBag.City = user_data.UserAdditionalInfo.City;
                ViewBag.CityCode = user_data.UserAdditionalInfo.CountryCode;
                ViewBag.Zip = user_data.UserAdditionalInfo.Zip;
                ViewBag.StreetAddress = user_data.UserAdditionalInfo.StreetAddress;
                ViewBag.HouseNumber = user_data.UserAdditionalInfo.HouseNumber;
                ViewBag.ProfileImage = user_data.UserAdditionalInfo.ProfileImage;
                ViewBag.DrivingApprove = user_data.AccountStatus.AccountApproved;
                ViewBag.BankApprove = user_data.AccountStatus.BankApproved;
                ViewBag.VASID = user_data.VerifyAppusers.Count();
                ViewBag.VIFSubmitted = user_data.FormSubmitted.IdentityFormSubmit;
                ViewBag.BVFSubmitted = user_data.FormSubmitted.BankFormSubmit;

                TempData["ValidationErroMessage"] = "All stared fileds are required!";
            }
            return View();
        }

        public bool ValidateUserDetailsForm(IFormCollection form) {
            if(form["FirstName"]=="" || form["LastName"]=="" || form["CountryCode"] == ""|| form["Phone"]=="" || form["Country"]=="" || form["State"]=="" || form["City"] == "" || form["Zip"]=="" || form["StreetAddress"]=="" || form["StreetAddress"]=="") {
                return false;
            }else {
                return true;
            }
        }

        [HttpPost, Route("settings/upload-identity-document"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadIndetityDocument(IFormCollection form) {
            if (form["IdentityDocuments"] != "") {
                if (form.Files.Count() > 1 && form.Files.Count() < 3) {
                    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result = await _settings.UploadDocument(form, id);
                    if (result == "DocumentsUploaded") {
                        TempData["DocumentsUploadedSuccessfully"] = "Thanks for the documents, Please update your bank details!";
                        return RedirectToAction("UserSettings", "Settings", new { area = "User" });
                    }
                } else {
                    TempData["DocumentsUploadedFailed"] = "File upload limit is 2 in number!";
                    return RedirectToAction("UserSettings", "Settings", new { area = "User" });
                }
            } else {
                TempData["DocumentsUploadedFailed"] = "File upload limit is 2 in number!";
                return RedirectToAction("UserSettings", "Settings", new { area = "User" });
            }
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }


        [HttpPost, Route("settings/add-bank-details"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBankDetails(IFormCollection form) {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _settings.AddbankDetails(form, id);
            if(ValidateBankForm(form) == null) {
                if (result == "BankDataAdded") {
                    TempData["BankDataAddedSucessfully"] = "Bank details updated successfully, Please wait for the confirmation while we are reviewing manualy!";
                    return RedirectToAction("UserSettings", "Settings", new { area = "User" });
                }
            }
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        public IActionResult ValidateBankForm(IFormCollection form) {
            if(string.IsNullOrEmpty(form["BankName"]) || string.IsNullOrEmpty(form["BankAccountNumber"]) || string.IsNullOrEmpty(form["BankAccountSwiftCode"])) {
                TempData["BankFormError"] = "All fields are required in back deatils form!";
                return RedirectToAction("UserSettings", "Settings", new { area = "User" });
            } else {
                return null;
            }
        }


        [HttpGet, Route("change-email")]
        public async Task<IActionResult> ChangeEmail() {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Email = user.Email;
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        /*
         * @ Form validation for change email
         */
        [HttpPost, Route("change-email"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(IFormCollection formValue) {
            if (CheckEmailValidation(formValue) == null) {
                var viewModel = new ChangeEmail {
                    Email = formValue["Email"]
                };
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _settings.ChangeEmail(viewModel, id);
                if (result == "UserEmailIsUpdated") {
                    await _signInManager.SignOutAsync();
                    TempData["ChangeEmailSuccess"] = "Email has been updated successfully, Please verify email first!";
                    return RedirectToAction("UserLogin", "Login");
                } else {
                    TempData["EmailInUse"] = "Email is already in use!";
                    return RedirectToAction("ChangeEmail", "Settings", new { area = "User" });
                }
            }
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Email = user.Email;

            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        public IActionResult CheckEmailValidation(IFormCollection formValue) {
            if (String.IsNullOrEmpty(formValue["Email"])) {
                TempData["EmailFieldNull"] = "Email is required!";
                return RedirectToAction("ChangeEmail", "Settings", new { area = "User" });
            } else {
                return null;
            }
        }

        [HttpGet, Route("change-password")]
        public IActionResult ChangePassword() {
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        /*
         * UserSettings is a instance of chnage passowrd for user profile
         */
        [HttpPost, Route("change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(IFormCollection values) {
            if (CheckformForChangePassword(values) == null) {
                var data = new ChangePassword() {
                    CurrentPassword = values["CurrentPassword"],
                    Password = values["NewPassword"],
                };
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _settings.ChnagePassword(data, userId);
                if (result.Succeeded) {
                    TempData["ChangePasswordSucceded"] = "Password has been changed successfully!";
                    return RedirectToAction("ChangePassword", "Settings", new { area = "User" });
                } else {
                    TempData["CurrentPasswordError"] = "Entered password is mismatched to exist password!";
                    return RedirectToAction("ChangePassword", "Settings", new { area = "User" });
                }
            }
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        /*
         * @ Form validation for change password
         */
        private IActionResult CheckformForChangePassword(IFormCollection keyValues) {
            if (String.IsNullOrEmpty(keyValues["CurrentPassword"]) && String.IsNullOrEmpty(keyValues["NewPassword"]) && String.IsNullOrEmpty(keyValues["ConfirmPassword"])) {
                TempData["AllFieldRequired_1"] = "All field is required!";
                return RedirectToAction("ChangePassword", "Settings", new { area = "User" });
            } else if (String.IsNullOrEmpty(keyValues["CurrentPassword"])) {
                TempData["CurrentPasswordEmpty"] = "Current password is required!";
                return RedirectToAction("ChangePassword", "Settings", new { area = "User" });
            } else if (String.IsNullOrEmpty(keyValues["NewPassword"])) {
                TempData["NewPasswordError"] = "New password is required!";
                return RedirectToAction("UserSettChangePasswordings", "Settings", new { area = "User" });
            } else if (keyValues["NewPassword"] != keyValues["ConfirmPassword"]) {
                TempData["PasswordVerificationError"] = "Password confirmation was failed!";
                return RedirectToAction("ChangePassword", "Settings", new { area = "User" });
            } else {
                return null;
            }
        }

        [HttpGet, Route("/user/deactivation")]
        public IActionResult DeactivateUser() {
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        [HttpGet, Route("/user/deactive/{id}")]
        public async Task<IActionResult> DeactivateUser(string id) {
            var result = await _settings.DeactivateAccount(id);
            if(result == "AccountDeactivated") {
                TempData["AccountDeactivated"] = "You are account is deactivated! You are not able to login to this account right now.";
                await _signInManager.SignOutAsync();
                return RedirectToAction("UserLogin", "Login");
            }
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        [HttpGet, Route("/user/account/delete")]
        public IActionResult DeleteUser() {
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }

        [HttpGet, Route("/user/account/delete/{id}")]
        public async Task<IActionResult> DeleteMyAccount(string id) {
            var result = await _settings.DeleteAccount(id);
            if(result == "UserAccountDeleted") {
                await _signInManager.SignOutAsync();
                TempData["UserAccountDeleted"] = "You are account is deleted! We are not able to restore this account any more. Take care!";
                return RedirectToAction("UserLogin", "Login");
            }
            return View("~/Areas/User/Views/Settings/UserSettings.cshtml");
        }
    }
}
