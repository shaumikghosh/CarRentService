using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CarRentService.Interfaces;

namespace CarRentService.Services {
    public class LoginService : ILogin {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> LogIn(LoginModel loginModel) {

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (await _userManager.CheckPasswordAsync(user, loginModel.Password) == false) {
                return "InvalidCredentialError";

            } else if (user.LockoutEnabled == true) {
                return "UserDeactivated";
            }else if(user.Deleted == true) {
                return "AccountDeleted";
            }
            else {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);

                if (result.Succeeded) {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("User")) {
                        return "User";
                    } else {
                        return "InvalidCredentialError";
                    }
                } else {
                    return "VerificationError";
                }
            }
        }
    }
}
