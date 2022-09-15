using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CarRentService.Areas.User.Interfaces {
    public interface IProfileSettings {
        Task<dynamic> ChnagePassword(ChangePassword change, string userId);
        Task<dynamic> ChangeEmail(ChangeEmail email, string userId);
        Task<string> ChangeProfileDetails(IFormCollection keyValuePairs, string id);
        Task<string> DeactivateAccount(string id);
        Task<string> DeleteAccount(string id);
        Task<string> UploadDocument(IFormCollection keyValuePairs, string id);
        Task<string> AddbankDetails(IFormCollection form, string id);
    }
}
