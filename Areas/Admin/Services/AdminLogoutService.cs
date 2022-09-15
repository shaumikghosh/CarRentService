using DataModel.Models;
using Microsoft.AspNetCore.Identity;
using CarRentService.Areas.Admin.Interfaces;

namespace CarRentService.Areas.Admin.Services {
    public class AdminLogoutService : IAdminLogout {

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminLogoutService(SignInManager<ApplicationUser> signInManager) {
            _signInManager = signInManager;
        }

        public void LogoutUser() {
            _signInManager.SignOutAsync();
        }
    }
}
