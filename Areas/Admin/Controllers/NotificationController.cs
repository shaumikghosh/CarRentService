using CarRentService.Areas.Admin.Customs;
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

namespace CarRentService.Areas.Admin.Controllers {

    [Area("Admin")]
    [Route("admin")]
    [ServerAuthorize]
    public class NotificationController : Controller {

        protected readonly DatabaseContext _databaseContext;
        protected readonly UserManager<ApplicationUser> _userManager;

        public NotificationController(DatabaseContext databaseContext, UserManager<ApplicationUser> userManager) {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }

        [HttpGet, Route("admin-all-notifications")]
        public IActionResult AllNotification() {
            return View();
        }

        [HttpGet, Route("admin-notification")]
        public async Task<IActionResult> UserNotification() {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id);
            var result = await _databaseContext.UserNotifications.Where(x => x.UserType == "admin" && x.Seen==false).OrderByDescending(x => x.Seen == false).OrderByDescending(x=>x.CreatetAt).ToListAsync();
            return Ok(result);
        }


        [HttpGet, Route("admin-notification-seen")]
        public async Task<IActionResult> UserSeenNotification() {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id);
            var result = await _databaseContext.UserNotifications.Where(x => x.Seen == false && x.UserType == "admin").OrderByDescending(x => x.CreatetAt).CountAsync();
            return Ok(new { result = result });
        }

        [HttpPost, Route("admin-mark-notf-read/{userId}/{doctype}")]
        public async Task<IActionResult> UserBotfmarlAsRead(string userId, string docType) {
            var nf = await _databaseContext.UserNotifications.Where(x => x.UserId == userId && x.Seen == false && x.UserType == "admin" && x.DocType==docType).FirstOrDefaultAsync();
            nf.Seen = true;
            _databaseContext.UserNotifications.Update(nf);
            await _databaseContext.SaveChangesAsync();
            return Ok(new { success = true });
        }

        //[HttpGet, Route("user-notf-delete")]
        //public async Task<IActionResult> DeeleteNotf() {
        //    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(id);
        //    var result = await _databaseContext.UserNotifications.Where(x => x.User == user).ToListAsync();
        //    foreach (var nf in result) {
        //        nf.Seen = true;
        //        _databaseContext.UserNotifications.Remove(nf);
        //        await _databaseContext.SaveChangesAsync();
        //    }
        //    return Ok(new { success = true });
        //}
    }
}
