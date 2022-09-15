using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Areas.User.Interfaces;
using System.IO;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CarRentService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;


namespace CarRentService.Areas.User.Services {
    public class ProfileSettingsService : IProfileSettings {

        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IConfiguration _config;
        protected readonly DatabaseContext _databaseContext;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly ILogger<ProfileSettingsService> _logger;

        public ProfileSettingsService(UserManager<ApplicationUser> userManager, IConfiguration config, DatabaseContext databaseContext, IWebHostEnvironment webHostEnvironment, ILogger<ProfileSettingsService> logger) {
            _userManager = userManager;
            _config = config;
            _databaseContext = databaseContext;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<dynamic> ChnagePassword(ChangePassword change, string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, change.CurrentPassword, change.Password);
            ConfirmationMail(emailFor: "PasswordUpdate", user, null);
            return result;
        }

        public async Task<dynamic> ChangeEmail(ChangeEmail model, string userId) {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) {
                var updateUser = await _userManager.FindByIdAsync(userId);
                updateUser.Email = model.Email;
                updateUser.NormalizedEmail = model.Email;
                updateUser.UserName = model.Email;
                updateUser.EmailConfirmed = false;

                var generatedToken = EmailVerificationToken(50, userId);
                var token = new VerificationTokens {
                    Token = generatedToken,
                    Used = false,
                    CreatedAt = DateTime.Now,
                    User = updateUser
                };
                await _databaseContext.Tokens.AddAsync(token);
                ConfirmationMail(emailFor:"EmailUpdate",updateUser, token.Token);
                await _userManager.UpdateAsync(updateUser);
                return "UserEmailIsUpdated";
            } else {
                return "EmailInUse";
            }
        }

        public async Task<string> ChangeProfileDetails(IFormCollection form, string id) {

            var user_data = await _userManager.Users.Where(user => user.Id == id).FirstOrDefaultAsync();

            user_data.FirstName = form["FirstName"];
            user_data.LastName = form["LastName"];

            await _userManager.UpdateAsync(user_data);
            await _databaseContext.SaveChangesAsync();

            var adinfo = await _databaseContext.UserAdditionalInfos.Where(x=>x.UserId == id).FirstOrDefaultAsync();

            if(form.Files.Count > 0) {
                foreach (var file in form.Files) {
                    adinfo.AdditionalEmail = form["AddtionalEmail"];
                    adinfo.CountryCode = form["CountryCode"];
                    adinfo.PhoneNumber = form["Phone"];
                    adinfo.Country = form["Country"];
                    adinfo.State = form["State"];
                    adinfo.City = form["City"];
                    adinfo.Zip = form["Zip"];
                    adinfo.StreetAddress = form["StreetAddress"];
                    adinfo.HouseNumber = form["HouseNumber"];

                    if (user_data.UserAdditionalInfo.ProfileImage != null) {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile/img/profile/", user_data.UserAdditionalInfo.ProfileImage);
                        var fileinfo = new FileInfo(uploadsFolder);
                        if (fileinfo != null) {
                            System.IO.File.Delete(uploadsFolder);
                        }
                    }
                    adinfo.ProfileImage = UploadedFile(file);
                    _databaseContext.UserAdditionalInfos.Update(adinfo);
                }
            } else {
                adinfo.AdditionalEmail = form["AddtionalEmail"];
                adinfo.CountryCode = form["CountryCode"];
                adinfo.PhoneNumber = form["Phone"];
                adinfo.Country = form["Country"];
                adinfo.State = form["State"];
                adinfo.City = form["City"];
                adinfo.Zip = form["Zip"];
                adinfo.StreetAddress = form["StreetAddress"];
                adinfo.HouseNumber = form["HouseNumber"];
                adinfo.ProfileImage = adinfo.ProfileImage;
                _databaseContext.UserAdditionalInfos.Update(adinfo);
            }

            var acs = _databaseContext.AccountStatuses.Where(x => x.UserId == id).FirstOrDefault();
            acs.AccountApproved = false;
            _databaseContext.AccountStatuses.Update(acs);

            var formsubmitted = _databaseContext.FormSubmitteds.Where(x => x.UserId == id).FirstOrDefault();
            formsubmitted.IdentityFormSubmit = false;
            _databaseContext.FormSubmitteds.Update(formsubmitted);

            await _databaseContext.SaveChangesAsync();

            return "UserDataUpdated";
        }


        private string UploadedFile(IFormFile model) {
            string uniqueFileName = null;

            if (model.FileName != null) {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile/img/profile/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                model.CopyTo(fileStream);
            }
            return uniqueFileName;
        }


        private string EmailVerificationToken(int length, string id) {
            var str_build = new StringBuilder();
            var random = new Random();

            char letter;

            for (int i = 0; i < length; i++) {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            var username = _userManager.Users.Where(x => x.Id == id).Include(toke => toke.Tokens).FirstOrDefault();

            if (username == null) {
                return str_build.ToString().ToLower();
            } else {
                return EmailVerificationToken(50, null);
            }
        }

        public async Task<string> DeactivateAccount(string id) {
            var user = await _userManager.FindByIdAsync(id);
            user.LockoutEnabled = true;
            await _userManager.UpdateAsync(user);
            ConfirmationMail(emailFor: "DeactivateAccount", user, null);
            return "AccountDeactivated";
        }

        public async Task<string> DeleteAccount(string id) {
            var email = "";
            var user = await _userManager.FindByIdAsync(id);
            email = user.Email;
            await _userManager.DeleteAsync(user);
            var storeEmail = new ApplicationUser {
                Email = email,
                NormalizedEmail = email,
                UserName = email,
                Deleted = true,
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(storeEmail);
            await _userManager.AddToRoleAsync(storeEmail, "User");
            ConfirmationMail(emailFor: "DeletedAccount", storeEmail, null);
            return "UserAccountDeleted";
        }

        public async Task<string> UploadDocument(IFormCollection keyValuePairs, string id) {
            var user = await _userManager.FindByIdAsync(id);
            foreach (var file in keyValuePairs.Files) {
                var data = new VerifyAppuser {
                    Image = UploadedFiles(file),
                    CreatedAt = DateTime.Now,
                    User = user
                };

                await _databaseContext.VerifyAppusers.AddAsync(data);
                await _databaseContext.SaveChangesAsync();
            }
            var form = await _databaseContext.FormSubmitteds.Where(x=>x.UserId==id).FirstOrDefaultAsync();
            form.IdentityFormSubmit = true;
            _databaseContext.Update(form);

            var notification = new Notification {
                Message = $"{user.FullName()} has submitted driving lisence.",
                Seen = false,
                CreatetAt = DateTime.Now,
                UserId = id,
                Approved = false,
                UserType = "admin",
                DocType = "dl"
            };
            _databaseContext.UserNotifications.Add(notification);
            _databaseContext.SaveChanges();

            return "DocumentsUploaded";
        }

        private string UploadedFiles(IFormFile model) {
            string uniqueFileName = null;

            if (model.FileName != null) {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile/img/documents/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                model.CopyTo(fileStream);
            }
            return uniqueFileName;
        }


        public async Task<string> AddbankDetails(IFormCollection form, string id) {
            var bank = await _databaseContext.UserBankDetails.Where(x => x.AppUserId == id).FirstOrDefaultAsync();
            bank.BankName = form["BankName"];
            bank.BankAccountNumber = form["BankAccountNumber"];
            bank.BankSwiftCode = form["BankAccountSwiftCode"];
            _databaseContext.UserBankDetails.Update(bank);

            var forms = await _databaseContext.FormSubmitteds.Where(x => x.UserId == id).FirstOrDefaultAsync();
            forms.BankFormSubmit = true;
            _databaseContext.Update(forms);

            var acss = await _databaseContext.AccountStatuses.Where(x => x.UserId == id).FirstOrDefaultAsync();
            acss.BankApproved = false;
            _databaseContext.Update(acss);

            var user = await _userManager.FindByIdAsync(id);

            var notification = new Notification {
                Message = $"{user.FullName()} has submitted bank information.",
                Seen = false,
                CreatetAt = DateTime.Now,
                UserId = id,
                Approved = false,
                UserType = "admin",
                DocType = "bi"
            };
            _databaseContext.UserNotifications.Add(notification);
            _databaseContext.SaveChanges();
            return "BankDataAdded";
        }

        private void ConfirmationMail(string emailFor, ApplicationUser model, string token) {

            if (emailFor == "EmailUpdate") {
                var str = new StreamReader(@"Templates/EmailUpdateConfirmation.html");
                string MailText = str.ReadToEnd();
                str.Close();
                var url = _config.GetSection("DomainAddress").Value + "/email/" + token;
                MailText = MailText.Replace("[url]", url);
                MailText = MailText.Replace("[Full_Name]", model.FirstName + " " + model.LastName);

                var mailMessege = new MailMessage();
                mailMessege.To.Add(model.Email);
                mailMessege.Subject = "Email verification mail";
                mailMessege.Body = MailText;
                mailMessege.From = new MailAddress(_config.GetSection("SMTP").GetSection("Mail").Value);
                mailMessege.IsBodyHtml = true;

                var smtp = new SmtpClient(_config.GetSection("SMTP").GetSection("Host").Value, Int32.Parse(_config.GetSection("SMTP").GetSection("Port").Value));
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_config.GetSection("SMTP").GetSection("Mail").Value, _config.GetSection("SMTP").GetSection("Password").Value);
                smtp.Send(mailMessege);
            }else if(emailFor == "PasswordUpdate") {
                var str = new StreamReader(@"Templates/PasswordUpdateConfirmation.html");
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[Full_Name]", model.FirstName + " " + model.LastName);

                var mailMessege = new MailMessage();
                mailMessege.To.Add(model.Email);
                mailMessege.Subject = "Password update mail";
                mailMessege.Body = MailText;
                mailMessege.From = new MailAddress(_config.GetSection("SMTP").GetSection("Mail").Value);
                mailMessege.IsBodyHtml = true;

                var smtp = new SmtpClient(_config.GetSection("SMTP").GetSection("Host").Value, Int32.Parse(_config.GetSection("SMTP").GetSection("Port").Value));
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_config.GetSection("SMTP").GetSection("Mail").Value, _config.GetSection("SMTP").GetSection("Password").Value);
                smtp.Send(mailMessege);
            } else if (emailFor == "DeactivateAccount") {

                var body = $"Hello {model.FirstName} {model.LastName},\nYour account is currently deactivated. To get your account back you can contact us any time. Thank you.";

                var mailMessege = new MailMessage();
                mailMessege.To.Add(model.Email);
                mailMessege.Subject = "Account Deactivate Confirmation Mail";
                mailMessege.Body = body;
                mailMessege.From = new MailAddress(_config.GetSection("SMTP").GetSection("Mail").Value);
                mailMessege.IsBodyHtml = true;

                var smtp = new SmtpClient(_config.GetSection("SMTP").GetSection("Host").Value, Int32.Parse(_config.GetSection("SMTP").GetSection("Port").Value));
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_config.GetSection("SMTP").GetSection("Mail").Value, _config.GetSection("SMTP").GetSection("Password").Value);
                smtp.Send(mailMessege);
            } else if (emailFor == "DeletedAccount") {

                var body = $"Hello {model.FirstName} {model.LastName},\nYour account is pemenantly deleted. To inform you unfortunately we won't be able to restore your account ever. It's final decision. Take care.";

                var mailMessege = new MailMessage();
                mailMessege.To.Add(model.Email);
                mailMessege.Subject = "Account Delete Confirmation Mail";
                mailMessege.Body = body;
                mailMessege.From = new MailAddress(_config.GetSection("SMTP").GetSection("Mail").Value);
                mailMessege.IsBodyHtml = true;

                var smtp = new SmtpClient(_config.GetSection("SMTP").GetSection("Host").Value, Int32.Parse(_config.GetSection("SMTP").GetSection("Port").Value));
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_config.GetSection("SMTP").GetSection("Mail").Value, _config.GetSection("SMTP").GetSection("Password").Value);
                smtp.Send(mailMessege);
            }
        }
    }
}
