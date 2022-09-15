using DataModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRentService.Areas.User.Customs;
using CarRentService.Data;

namespace CarRentService.Areas.User.Controllers {
    [Area("User")]
    [Route("user")]
    [ClientAuthorize]
    public class ProfileController : Controller {

        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly DatabaseContext _databaseContext;

        public ProfileController(UserManager<ApplicationUser> userManager, DatabaseContext databaseContext) {
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        [HttpGet, Route("profile")]
        public IActionResult Profile() {
            return View();
        }
    }
}
