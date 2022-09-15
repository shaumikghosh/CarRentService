using DataModel.ViewModels;
using System.Threading.Tasks;

namespace CarRentService.Areas.Admin.Interfaces {
    public interface ISettings {
        Task<dynamic> ChnagePassword(ChangePassword changePassword, string userId);
        Task<dynamic> ChangeProfileDetails(ChangeProfileDetails changeProfileDetails, string id);
        Task<dynamic> GetEditAbleProfile(string id);
    }
}
