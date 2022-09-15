using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentService.Areas.Admin.Interfaces {
    public interface IUserService {
        Task<dynamic> GetAll();
        Task<dynamic> GetSuperAdmins();
        Task<dynamic> GetAdmins();
        Task<dynamic> GetUsers();
        Task<dynamic> GetEditAbleUser(string id);
        Task<dynamic> CreateUser(CreateUser createUser);
        Task<dynamic> DeleteUser(string Id);
        Task<dynamic> UpdateUser(string Id, UpdateUser updateUser);
        Task<dynamic> GetSearchedUser(string keyword);
        Task<dynamic> ApproveDrivingLisence(string id);
        Task<dynamic> RejectDrivingLisence(string id);
        Task<dynamic> ApproveBankDetails(string id);
        Task<dynamic> RejectBankDetails(string id);
        Task<dynamic> UpdateUserInformation(ChangeUserInfo model, string id);
        Task<dynamic> UpdateUserBankInformation(ChangeUserBankDetails model, string id);
    }
}
