using CarRentService.Areas.User.Customs;
using CarRentService.Data;
using DataModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRentService.Areas.User.Controllers {

    [Area("User")]
    [Route("user")]
    [ClientAuthorize]
    public class NotificationController : Controller {

        protected readonly DatabaseContext _databaseContext;
        protected readonly UserManager<ApplicationUser> _userManager;

        public NotificationController (DatabaseContext databaseContext, UserManager<ApplicationUser> userManager) {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }

        [HttpGet, Route("user-notification")]
        public async Task<IActionResult> UserNotification () {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _databaseContext.UserNotifications.Where(x=>x.UserId == id && x.UserType=="user").OrderByDescending(x=>x.CreatetAt).ToListAsync();
            return Ok(result);
        }


        [HttpGet, Route("user-notification-seen")]
        public async Task<IActionResult> UserSeenNotification() {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _databaseContext.UserNotifications.Where(x => x.UserId == id && x.Seen==false && x.UserType=="user").OrderByDescending(x => x.CreatetAt).CountAsync();
            return Ok(new { result=result});
        }

        [HttpGet, Route("user-mark-notf-read")]
        public async Task<IActionResult> UserBotfmarlAsRead() {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _databaseContext.UserNotifications.Where(x => x.UserId == id && x.Seen == false && x.UserType == "user").ToListAsync();
            foreach(var nf in result) {
                nf.Seen = true;
                _databaseContext.UserNotifications.Update(nf);
                await _databaseContext.SaveChangesAsync();
            }
            return Ok(new { success = true });
        }

        [HttpGet, Route("user-notf-delete")]
        public async Task<IActionResult> DeeleteNotf() {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _databaseContext.UserNotifications.Where(x => x.UserId == id && x.UserType == "user").ToListAsync();
            foreach (var nf in result) {
                nf.Seen = true;
                _databaseContext.UserNotifications.Remove(nf);
                await _databaseContext.SaveChangesAsync();
            }
            return Ok(new { success = true });
        }
    }
}
