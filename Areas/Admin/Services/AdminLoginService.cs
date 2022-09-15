using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CarRentService.Areas.Admin.Interfaces;

namespace CarRentService.Areas.Admin.Services {
    public class AdminLoginService : IAdminLogin {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminLoginService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> LogIn(LoginModel loginModel) {

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            var isLockOut = await _userManager.FindByEmailAsync(loginModel.Email);

            if (await _userManager.CheckPasswordAsync(user, loginModel.Password) == false) {
                return "InvalidCredentialError";

            } else if (isLockOut.LockoutEnabled == true) {
                return "UserDeactivated";
            } else {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);

                if (result.Succeeded) {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (!roles.Contains("User")) {
                        return "Admin";
                    } else {
                        return "InvalidCredentialError";
                    }
                }
            }
            return null;
        }
    }
}
