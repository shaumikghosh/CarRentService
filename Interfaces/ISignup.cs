using DataModel.ViewModels;
using System.Threading.Tasks;

namespace CarRentService.Interfaces {
    public interface ISignup {
        Task<string> SignUp(SignupModel signup);
    }
}
