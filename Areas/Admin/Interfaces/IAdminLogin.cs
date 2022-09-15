using DataModel.ViewModels;
using System.Threading.Tasks;

namespace CarRentService.Areas.Admin.Interfaces {
    public interface IAdminLogin {
        Task<string> LogIn(LoginModel loginModel);
    }
}
