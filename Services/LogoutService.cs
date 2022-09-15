using DataModel.Models;
using Microsoft.AspNetCore.Identity;
using CarRentService.Interfaces;

namespace CarRentService.Services {
    public class LogoutService : ILogout {

        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutService(SignInManager<ApplicationUser> signInManager) {
            _signInManager = signInManager;
        }

        public void LogoutUser() {
            _signInManager.SignOutAsync();
        }
    }
}
