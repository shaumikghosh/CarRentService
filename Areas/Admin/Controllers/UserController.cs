using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRentService.Areas.Admin.Customs;
using CarRentService.Areas.Admin.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using CarRentService.Data;

namespace CarRentService.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("admin")]
    [ServerAuthorize]
    public class UserController : Controller {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext  _databaseContext;

        public UserController(
            RoleManager<ApplicationRole> roleManager,
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            DatabaseContext databaseContext
        ) {
            _roleManager = roleManager;
            _userService = userService;
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("all-users")]
        public async Task<IActionResult> AllUsers() {
            ViewBag.SuperAdmins = await _userService.GetSuperAdmins();
            return View();
        }

        [HttpGet]
        [Route("create-user")]
        public IActionResult CreateUser() {
            var createUser = new CreateUser();
            ViewBag.Roles = _roleManager.Roles;
            return View(createUser);
        }

        [HttpPost]
        [Route("create-user")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUser createUser) {
            ViewBag.Roles = _roleManager.Roles;
            if (ModelState.IsValid) {
                var result = await _userService.CreateUser(createUser);
                if (result is not null) {
                    if (result.GetType() is string && result == "EmailExist") {
                        TempData["EmailExist"] = "Email already in use!";
                        return View(createUser);
                    } else {
                        TempData["Email"] = result["email"];
                        TempData["Password"] = result["password"];
                        TempData["Role"] = result["Role"];
                        TempData["UserCreated201"] = result["UserCreated201"];
                        return RedirectToAction("CreateUser", "User", new { area = "Admin" });
                    }
                }
            }
            return View(createUser);
        }

        [HttpGet]
        [Route("admins")]
        public async Task<IActionResult> Admins() {
            ViewBag.Admins = await _userService.GetAdmins();
            return View();
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Users() {
            ViewBag.Users = await _userService.GetUsers();
            return View();
        }

        [HttpGet, Route("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id) {
            var result = await _userService.DeleteUser(id);
            if (result.Succeeded) {
                TempData["UserDeleteSuccess"] = "User is deleted successfully!";
                return RedirectToAction("AllUsers", "User", new { area = "Admin" });
            } else {
                if (result.Errors.Any()) {
                    TempData["UserDeleteFail"] = "Something was wrong, try again!";
                    return RedirectToAction("AllUsers", "User", new { area = "Admin" });
                }
            }
            return View();
        }

        [HttpGet, Route("edit-user/{id}")]
        public async Task<IActionResult> EditUser(string Id) {
            var result = await _userService.GetEditAbleUser(Id);
            ViewBag.Roles = _roleManager.Roles;
            ViewBag.Id = Id;
            if (result is not null) {
                return View(result);
            }
            return View();
        }

        [HttpPost, Route("edit-user/{id}")]
        public async Task<IActionResult> EditUser(string Id, UpdateUser updateUser) {
            ViewBag.Roles = _roleManager.Roles;
            if (ModelState.IsValid) {
                var result = await _userService.UpdateUser(Id, updateUser);
                if (result is not null) {
                    if (result == "IdValue") {
                        TempData["IdValue"] = "User Id has no value!";
                        return View(updateUser);
                    } else {
                        if (result == "UserUpdate201") {
                            TempData["UserUpdate201"] = "User data updated successfully!";
                            return RedirectToAction("EditUser", "User", new { area = "Admin" });
                        }
                    }
                }
            }
            return View(updateUser);
        }

        [HttpGet, Route("edit-user/ai/{id}")]
        public async Task<IActionResult> EditUserAdditionalInfo(string Id) {
            var result = await _databaseContext.UserAdditionalInfos.Where(x => x.UserId == Id).FirstOrDefaultAsync();
            var modelData = new ChangeUserInfo {
                AdditionalEmail = result.AdditionalEmail,
                CountryCode = result.CountryCode,
                PhoneNumber = result.PhoneNumber,
                Country = result.Country,
                City = result.City,
                StreetAddress = result.StreetAddress,
                HouseNumber = result.HouseNumber,
                State = result.State,
                Zip = result.Zip
            };
            
            return View(modelData);
        }

        [HttpPost, Route("edit-user/ai/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserAdditionalInfo(ChangeUserInfo model, string Id) {
            if (ModelState.IsValid) {
                var result = await _userService.UpdateUserInformation(model, Id);
                if (result == "DataUpdated") {
                    TempData["DataUpdated"] = "User information updated successfully!";
                    return Redirect($"/admin/edit-user/ai/{Id}");
                }
            }
            return View();
        }

        [HttpGet, Route("edit-user/bank/{id}")]
        public async Task<IActionResult> EditUserBankInfo(string Id) {
            var result = await _databaseContext.UserBankDetails.Where(x => x.AppUserId == Id).FirstOrDefaultAsync() ;
            var data = new ChangeUserBankDetails {
                BankAccountNumber = result.BankAccountNumber,
                BankSwiftCode = result.BankSwiftCode,
                BankName = result.BankName
            };
            return View(data);
        }

        [HttpPost, Route("edit-user/bank/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserBankInfo(ChangeUserBankDetails model,string Id) {
            if (ModelState.IsValid) {
                var result = await _userService.UpdateUserBankInformation(model, Id);
                if (result == "BankInfoUpdated") {
                    TempData["BankInfoUpdated"] = "Bank information successfully updated!";
                    return Redirect($"/admin/edit-user/bank/{Id}");
                }
            }
            return View();
        }

        [HttpGet, Route("search-user")]
        public async Task<IActionResult> SearchUser(string data) {
            var searchUser = new SearchUser();
            ViewBag.SearchResult = await _userService.GetSearchedUser(data);
            return View(searchUser);
        }

        [HttpGet, Route("view/user/{id}")]
        public async Task<IActionResult> SingleUser(string id) {
            var result = await _userManager.Users.Where(x => x.Id == id).Include(x => x.UserAdditionalInfo).Include(x => x.UserBankDetails).Include(x => x.VerifyAppusers).Include(x=>x.ApplicationUserRole).ThenInclude(x=>x.ApplicationRole).Include(x => x.AccountStatus).FirstOrDefaultAsync();

            ViewBag.Id = result.Id;
            ViewBag.FullName = result.FullName();
            ViewBag.Email = result.Email;
            ViewBag.Deleted = result.Deleted;
            ViewBag.EmailConfirmed = result.EmailConfirmed;
            ViewBag.Role = result.ApplicationUserRole;
            try {
                ViewBag.AdiEmail = result.UserAdditionalInfo.AdditionalEmail;
                ViewBag.Phone = result.UserAdditionalInfo.PhoneNumber;
                ViewBag.Phone = result.UserAdditionalInfo.CountryCode;
                ViewBag.Street = result.UserAdditionalInfo.StreetAddress;
                ViewBag.Zip = result.UserAdditionalInfo.Zip;
                ViewBag.HouseNumber = result.UserAdditionalInfo.HouseNumber;
                ViewBag.State = result.UserAdditionalInfo.State;
                ViewBag.City = result.UserAdditionalInfo.City;
                ViewBag.Country = result.UserAdditionalInfo.Country;
                ViewBag.ProfileImage = result.UserAdditionalInfo.ProfileImage;
            } catch (NullReferenceException) {
                ViewBag.AdiEmail = "";
                ViewBag.Phone = "";
                ViewBag.CountryCode = "";
                ViewBag.Street = "";
                ViewBag.Zip = "";
                ViewBag.HouseNumber = "";
                ViewBag.State = "";
                ViewBag.City = "";
                ViewBag.Country = "";
                ViewBag.ProfileImage = "";
            }

            try {
                ViewBag.DrivingLisence = result.VerifyAppusers.Count();
                ViewBag.LisenceImages = result.VerifyAppusers;
            } catch (NullReferenceException) {
                ViewBag.DrivingLisence = 0;
                ViewBag.LisenceImages = null;
            }

            try {
                ViewBag.BankDocument = result.UserBankDetails.AppUserId;
                ViewBag.BankName = result.UserBankDetails.BankName;
                ViewBag.BankAccount = result.UserBankDetails.BankAccountNumber;
                ViewBag.SwiftCode = result.UserBankDetails.BankSwiftCode;
            } catch (NullReferenceException) {
                ViewBag.BankDocument = "";
                ViewBag.BankName = "";
                ViewBag.BankAccount = "";
                ViewBag.SwiftCode = "";
            }

            try {
                ViewBag.DrivingApprove = result.AccountStatus.AccountApproved;
                ViewBag.BankApprove = result.AccountStatus.BankApproved;
            } catch (NullReferenceException) {
                ViewBag.DrivingApprove = false;
                ViewBag.BankApprove = false;
            }

            return View();
        }

        [HttpGet, Route("approve-driving-licence/{id}")]
        public async Task<IActionResult> ApproveDrivingLisence (string id) {
            var result = await _userService.ApproveDrivingLisence(id);
            if(result == "UserApproved") {
                TempData["UserApproved"] = "You have approved this user's driving lisence";
                return Redirect($"/admin/view/user/{id}");
            }
            return Content("Something went wrong");
        }


        [HttpGet, Route("reject-driving-licence/{id}")]
        public async Task<IActionResult> RejectDrivingLisence(string id) {
            var result = await _userService.RejectDrivingLisence(id);
            if (result == "DrivingLisenceRejected") {
                TempData["DrivingLisenceRejected"] = "You have rejected this user's driving lisence";
                return Redirect($"/admin/view/user/{id}");
            }
            return Content("Something went wrong");
        }


        [HttpGet, Route("approve-bank-licence/{id}")]
        public async Task<IActionResult> ApproveBank(string id) {
            var result = await _userService.ApproveBankDetails(id);
            if (result == "BankApproved") {
                TempData["BankApproved"] = "You have approved this user's bank details";
                return Redirect($"/admin/view/user/{id}");
            }
            return Content("Something went wrong");
        }


        [HttpGet, Route("reject-bank-licence/{id}")]
        public async Task<IActionResult> RejectBank(string id) {
            var result = await _userService.RejectBankDetails(id);
            if (result == "BankRejected") {
                TempData["BankRejected"] = "You have rejected this user's bank details";
                return Redirect($"/admin/view/user/{id}");
            }
            return Content("Something went wrong");
        }
    }
}
