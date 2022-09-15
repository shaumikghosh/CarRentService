using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Areas.Admin.Interfaces;
using CarRentService.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CarRentService.Areas.Admin.Services {
    public class UserService : IUserService{

        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly DatabaseContext _databaseContext;
        protected readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(UserManager<ApplicationUser> userManager, DatabaseContext databaseContext, IWebHostEnvironment webHostEnvironment) {
            _userManager = userManager;
            _databaseContext = databaseContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<dynamic> GetAll() {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<dynamic> GetSuperAdmins() {
            return await _userManager.GetUsersInRoleAsync("SuperAdmin");
        }

        public async Task<dynamic> GetAdmins() {
            return await _userManager.GetUsersInRoleAsync("Admin");
        }

        public async Task<dynamic> GetUsers() {
            return await _userManager.GetUsersInRoleAsync("User");
        }

        public async Task<dynamic> CreateUser(CreateUser createUser) {

            var dateTime = DateTime.Now;

            var user = new ApplicationUser {
                UserName = createUser.Email,
                NormalizedUserName = createUser.Email,
                Email = createUser.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedAt = dateTime,
            };

            var userCheck = await _userManager.FindByEmailAsync(user.Email);

            if (userCheck == null) {
                var result = await _userManager.CreateAsync(user, createUser.Password);
                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(user, createUser.UserRole);
                    var data = new Hashtable() {
                        ["UserCreated201"] = "New user is created successfully!",
                        ["Email"] = createUser.Email,
                        ["Password"] = createUser.Password,
                        ["Role"] = createUser.UserRole
                    };
                    await _databaseContext.FormSubmitteds.AddAsync(new FormSubmitted { UserId = user.Id, BankFormSubmit = false, IdentityFormSubmit = false });
                    await _databaseContext.UserAdditionalInfos.AddAsync(new UserAdditionalInfo { UserId = user.Id});
                    await _databaseContext.AccountStatuses.AddAsync(new AccountStatus { UserId = user.Id, AccountApproved = false, BankApproved = false });
                    await _databaseContext.UserBankDetails.AddRangeAsync(new UserBankDetails { AppUserId = user.Id });
                    await _databaseContext.SaveChangesAsync();
                    return data;
                }
            } else {
                return "EmailExist";
            }
            return null;
        }

        public async Task<dynamic> DeleteUser(string userId) {
            var user = _userManager.Users.Where(user => user.Id == userId).FirstOrDefault();
            return await _userManager.DeleteAsync(user);
        }

        public async Task<dynamic> GetEditAbleUser(string id) {
            var user = await _userManager.Users.Where(user => user.Id == id).Include(role => role.ApplicationUserRole).ThenInclude(role => role.ApplicationRole).FirstOrDefaultAsync();
            if (user != null) {
                var updateUser = new UpdateUser() {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserRole = user.ApplicationUserRole.Select(x=>x.ApplicationRole.Name).FirstOrDefault(),
                    UserEnablity = user.LockoutEnabled
                };
                return updateUser;
            }
            return null;
        }

        public async Task<dynamic> UpdateUser(string Id, UpdateUser updateUser) {
            var user = await _userManager.FindByIdAsync(Id);
            var currentRoles = await _userManager.GetRolesAsync(user);

            if (user != null) {
                var updatedUserData = new ApplicationUser() {
                    FirstName = updateUser.FirstName,
                    LastName = updateUser.LastName,
                    Email = updateUser.Email,
                    NormalizedEmail = updateUser.Email,
                    NormalizedUserName = updateUser.Email
                };
                var result = await _userManager.UpdateAsync(updatedUserData);

                if (result.Succeeded) {
                    await _userManager.RemoveFromRolesAsync(updatedUserData, currentRoles);
                    await _userManager.AddToRoleAsync(updatedUserData, updateUser.UserRole);
                    await _userManager.SetLockoutEnabledAsync(updatedUserData, updateUser.UserEnablity);
                    return "UserUpdate201";
                }
            } else {
                return "IdValue";
            }
            return null;
        }

        public async Task<dynamic> GetSearchedUser(string keyword) {
            return await _userManager.Users.Include(user => user.ApplicationUserRole).ThenInclude(role => role.ApplicationRole).Where(x => x.Email.Contains(keyword) || x.UserName.Contains(keyword) || x.FirstName.Contains(keyword) || x.LastName.Contains(keyword)).ToListAsync();
        }

        public async Task<dynamic> ApproveDrivingLisence(string id) {
            var user = await _databaseContext.AccountStatuses.Where(x => x.UserId == id).FirstOrDefaultAsync();
            if(user == null) {
                await _databaseContext.AccountStatuses.AddAsync(new AccountStatus { UserId=id, AccountApproved = true});
                await _databaseContext.SaveChangesAsync();
            } else {
                user.UserId = id;
                user.AccountApproved = true;
                _databaseContext.AccountStatuses.Update(user);
                _databaseContext.SaveChanges();
            }
            var notification = new Notification {
                Message = "Congrats! Your driving lisence is approved!",
                Seen = false,
                CreatetAt = DateTime.Now,
                UserId = id,
                Approved = true,
                UserType = "user"
            };
            _databaseContext.UserNotifications.Add(notification);
            _databaseContext.SaveChanges();
            return "UserApproved";
        }

        public async Task<dynamic> RejectDrivingLisence(string id) {
            var user = await _userManager.FindByIdAsync(id);
            var result = _databaseContext.VerifyAppusers.Where(x=>x.User==user);
            foreach(var item in result) {
                _databaseContext.VerifyAppusers.Remove(item);
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile/img/documents/", item.Image);
                var fileinfo = new FileInfo(uploadsFolder);
                if (fileinfo != null) {
                    //System.IO.File.Delete(uploadsFolder);
                }
            }
            var rs = await _databaseContext.FormSubmitteds.Where(x => x.UserId == id).FirstOrDefaultAsync();
            rs.IdentityFormSubmit = false;
            _databaseContext.FormSubmitteds.Update(rs);

            var usr = await _userManager.FindByIdAsync(id);
            var notification = new Notification {
                Message = "Sorry! Your identity is rejected due to could not verify it, try again!",
                Seen = false,
                CreatetAt = DateTime.Now,
                UserId = id,
                Approved = false,
                UserType = "user"
            };
            _databaseContext.UserNotifications.Add(notification);
            _databaseContext.SaveChanges();
            return "DrivingLisenceRejected";
        }

        public async Task<dynamic> ApproveBankDetails(string id) {
            var user = await _databaseContext.AccountStatuses.Where(x => x.UserId == id).FirstOrDefaultAsync();
            if (user == null) {
                await _databaseContext.AccountStatuses.AddAsync(new AccountStatus { UserId = id, BankApproved = true });
                await _databaseContext.SaveChangesAsync();
            } else {
                user.UserId = id;
                user.BankApproved = true;
                _databaseContext.AccountStatuses.Update(user);
                _databaseContext.SaveChanges();
            }
            var usr = await _userManager.FindByIdAsync(id);
            var notification = new Notification {
                Message = "Congrats! Your bank infor is approved!",
                Seen = false,
                CreatetAt = DateTime.Now,
                UserId = id,
                Approved = true,
                UserType = "user"
            };
            _databaseContext.UserNotifications.Add(notification);
            _databaseContext.SaveChanges();
            return "BankApproved";
        }

        public async Task<dynamic> RejectBankDetails(string id) {
            var bank_info = await _databaseContext.UserBankDetails.Where(x => x.AppUserId == id).FirstOrDefaultAsync() ;
            bank_info.BankName = null;
            bank_info.BankSwiftCode = null;
            bank_info.BankAccountNumber = null;
            _databaseContext.UserBankDetails.Update(bank_info);

            var rs = await _databaseContext.FormSubmitteds.Where(x => x.UserId == id).FirstOrDefaultAsync();
            rs.BankFormSubmit = false;
            _databaseContext.FormSubmitteds.Update(rs);

            var usr = await _userManager.FindByIdAsync(id);
            var notification = new Notification {
                Message = "Sorry! Your bank info is rejected due to could not verify it, try again!",
                Seen = false,
                CreatetAt = DateTime.Now,
                UserId = id,
                Approved = false,
                UserType = "user"
            };
            _databaseContext.UserNotifications.Add(notification);
            _databaseContext.SaveChanges();
            return "BankRejected";
        }

        public async Task<dynamic> UpdateUserInformation(ChangeUserInfo model, string id) {
            var result = await _databaseContext.UserAdditionalInfos.Where(x => x.UserId == id).FirstOrDefaultAsync();
            result.AdditionalEmail = model.AdditionalEmail;
            result.PhoneNumber = model.PhoneNumber;
            result.CountryCode = model.CountryCode;
            result.City = model.City;
            result.State = model.State;
            result.Zip = model.Zip;
            result.State = model.State;
            result.HouseNumber = model.HouseNumber;

            _databaseContext.UserAdditionalInfos.Update(result);
            await _databaseContext.SaveChangesAsync();

            return "DataUpdated";
        }

        public async Task<dynamic> UpdateUserBankInformation(ChangeUserBankDetails model, string id) {
            var data =  _databaseContext.UserBankDetails.Where(x => x.AppUserId == id).FirstOrDefault();
            data.BankName = model.BankName;
            data.BankAccountNumber = model.BankAccountNumber;
            data.BankSwiftCode = model.BankSwiftCode;
            _databaseContext.UserBankDetails.Update(data);
            await _databaseContext.SaveChangesAsync();

            return "BankInfoUpdated";
        }
    }
}
