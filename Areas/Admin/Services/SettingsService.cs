using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Areas.Admin.Interfaces;

namespace CarRentService.Areas.Admin.Services {
    public class SettingsService : ISettings {

        private readonly UserManager<ApplicationUser> _userManager;

        public SettingsService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public async Task<dynamic> ChnagePassword(ChangePassword changePassword, string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.Password);
            return result;
        }

        public async Task<dynamic> ChangeProfileDetails(ChangeProfileDetails changeProfileDetails, string id) {
            var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();

            user.FirstName = changeProfileDetails.FirstName;
            user.LastName = changeProfileDetails.LastName;
            user.Email = changeProfileDetails.Email;
            user.UserName = changeProfileDetails.Email;
            user.NormalizedUserName = changeProfileDetails.Email;
            user.NormalizedEmail = changeProfileDetails.Email;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<dynamic> GetEditAbleProfile(string id) {
            var user = await _userManager.FindByIdAsync(id);
            var changeProfileDetails = new ChangeProfileDetails() {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return changeProfileDetails;
        }
    }
}
