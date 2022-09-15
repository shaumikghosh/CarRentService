using DataModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentService.Interfaces {
    public interface IForgetPassword {
        Task<string> ForgetPasswordEmail(ForgetPasswordEmail model);
        Task<bool> CheckToken(string token);
        Task<string> ResetPassword(string token, ChangeForgotPassword model);
    }
}
