using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModel.ViewModels;
using CarRentService.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using CarRentService.Data;
using DataModel.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarRentService.Controllers.Clientside {
    public class SignupController : Controller {

        private readonly ISignup _signup;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly DatabaseContext _databaseContext;

        public SignupController(ISignup signup, UserManager<ApplicationUser> userManager, DatabaseContext databaseContext) {
            _signup = signup;
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("/user/signup")]
        public IActionResult Signup() {
            var signupModel = new SignupModel();
            return View(signupModel);
        }

        [HttpPost]
        [Route("/user/signup")]
        public async Task<IActionResult> Signup(SignupModel signupModel) {

            if (ModelState.IsValid) {
                var result = await _signup.SignUp(signupModel);
                if (result is not null) {
                    if (result == "EmailInUse") {
                        TempData["EmailInUse"] = "E-Mail already in use!";
                        return View(signupModel);
                    } else {
                        if (result == "RegistrationCompleted") {
                            TempData["RegistrationCompleteMessage"] = "Registration completed successfully, To login check your email and verify first.";
                            return RedirectToAction("UserLogin", "Login");
                        }
                    }
                }
            }
            return View(signupModel);
        }


        [HttpGet, Route("email/{token}")]
        public async Task<IActionResult> VerifyEmail(string token) {
            var useriD = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _databaseContext.Tokens.Where(x => x.Token == token && x.Used == false).Include(x=>x.User).FirstOrDefaultAsync();

            if (result != null) {
                result.Used = true;
                _databaseContext.SaveChanges();
                var user = await _userManager.Users.Where(x=>x.Id==result.User.Id).FirstOrDefaultAsync();
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                TempData["EmailVerified"] = "Thanks for verifying your mail .... !";
            } else {
                TempData["TokenError"] = "You'r trying to use a wrong / used token .... !";
            }
            return View();
        }
    }
}
