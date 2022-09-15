using DataModel.Models;
using DataModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentService.Data;
using CarRentService.Interfaces;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace CarRentService.Services {
    public class SignupServices : ISignup {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _config;
        public readonly ILogger<SignupServices> _loggoer;

        public SignupServices(UserManager<ApplicationUser> userManager, DatabaseContext databaseContext, IConfiguration config, ILogger<SignupServices> logger) {
            _userManager = userManager;
            _databaseContext = databaseContext;
            _config = config;
            _loggoer = logger;
        }

        public async Task<string> SignUp(SignupModel signupModel) {
            var dateTime = DateTime.Now;
            var user = new ApplicationUser {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                UserName = signupModel.Email,
                NormalizedUserName = signupModel.Email,
                Email = signupModel.Email,
                EmailConfirmed = false,
                PhoneNumberConfirmed = true,
                CreatedAt = dateTime.Date,
            };
            var generatedToken = EmailVerificationToken(50, user.Id);
            var token = new VerificationTokens {
                Token = generatedToken,
                Used = false,
                CreatedAt = dateTime,
                User = user
            };
            
            var userCheck = await _userManager.FindByEmailAsync(signupModel.Email);



            if (userCheck == null) {
                var result = await _userManager.CreateAsync(user, signupModel.Password);
                await _userManager.SetLockoutEnabledAsync(user, false);
                if (result.Succeeded) {
                    await _databaseContext.Tokens.AddAsync(token);
                    await _userManager.AddToRoleAsync(user, "User");

                    try {
                        EmailConfirmationMail(signupModel, generatedToken);
                    } catch (SmtpException ex) {
                        _loggoer.LogWarning(ex.Message);
                    }
                    await _databaseContext.FormSubmitteds.AddAsync(new FormSubmitted { UserId = user.Id, BankFormSubmit = false, IdentityFormSubmit = false });
                    await _databaseContext.UserAdditionalInfos.AddAsync(new UserAdditionalInfo {UserId = user.Id });
                    await _databaseContext.AccountStatuses.AddAsync(new AccountStatus { UserId = user.Id, AccountApproved = false, BankApproved = false });
                    await _databaseContext.UserBankDetails.AddRangeAsync(new UserBankDetails { AppUserId = user.Id });
                    await _databaseContext.SaveChangesAsync();
                    return "RegistrationCompleted";
                }
            }
            return "EmailInUse";
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


        private void EmailConfirmationMail(SignupModel model, string token) {
            var str = new StreamReader(@"Templates/EmailConfirmation.html");
            string MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText.Replace("[FirstName]", model.FirstName);
            MailText = MailText.Replace("[LastName]", model.LastName);
            var url = _config.GetSection("DomainAddress").Value + "/email/" + token;
            MailText = MailText.Replace("[url]", url);

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
        }
    }
}
