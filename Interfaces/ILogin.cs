using DataModel.ViewModels;
using System.Threading.Tasks;

namespace CarRentService.Interfaces {
    public interface ILogin {
        Task<string> LogIn(LoginModel loginModel);
    }
}
