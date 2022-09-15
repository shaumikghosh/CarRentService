using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataModel.Models;
using System.Collections.Generic;

namespace CarRentService.Data {
    /* ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string> */
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>> {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<UserAdditionalInfo> UserAdditionalInfos { get; set; }
        public DbSet<VerificationTokens> Tokens { get; set; }
        public DbSet<VerifyAppuser> VerifyAppusers { get; set; }
        public DbSet<UserBankDetails> UserBankDetails { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<FormSubmitted> FormSubmitteds { get; set; }
        public DbSet<Notification> UserNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            CreateSuperAdmin(builder);
            CreateUser(builder);
            CreateAdmin(builder);
            UserRelationToTokens(builder);

            RelationWithUserRoleAndRoleConnector(builder);
            UserRelationToAdditionalInfo(builder);
            UserRelationToBankDetails(builder);
            UserRelationToVerifyIdentity(builder);
            UserRelationToAccountStatus(builder);
            UserRelationToFormSubmitted(builder);

            builder.Entity<UserBankDetails>()
                .Property(x => x.BankAccountNumber).IsRequired(false);
            builder.Entity<UserBankDetails>()
                .Property(x => x.BankName).IsRequired(false);
            builder.Entity<UserBankDetails>()
                .Property(x => x.BankSwiftCode).IsRequired(false);
        }

        private static void CreateSuperAdmin(ModelBuilder builder) {
            const string DEMO_SUPER_ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e575";
            const string DEMO_SUPER_ADMIN_ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e900";

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = DEMO_SUPER_ADMIN_ROLE_ID, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(

                new ApplicationUser {
                    Id = DEMO_SUPER_ADMIN_ID,
                    FirstName = "Super",
                    LastName = "User",
                    UserName = "admin@dotnet.project",
                    NormalizedUserName = "admin@dotnet.project".ToUpper(),
                    Email = "admin@dotnet.project",
                    NormalizedEmail = "admin@dotnet.project".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "password"),
                }
            );

            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole {
                    RoleId = DEMO_SUPER_ADMIN_ROLE_ID,
                    UserId = DEMO_SUPER_ADMIN_ID
                }
            );

            builder.Entity<FormSubmitted>().HasData(new FormSubmitted { UserId = DEMO_SUPER_ADMIN_ID, BankFormSubmit = false, IdentityFormSubmit= false});
            builder.Entity<UserAdditionalInfo>().HasData(new UserAdditionalInfo { UserId = DEMO_SUPER_ADMIN_ID });
            builder.Entity<AccountStatus>().HasData(new AccountStatus { UserId = DEMO_SUPER_ADMIN_ID,  AccountApproved = false, BankApproved=false});
            builder.Entity<UserBankDetails>().HasData(new UserBankDetails { AppUserId = DEMO_SUPER_ADMIN_ID});
        }

        private static void CreateAdmin(ModelBuilder builder) {
            const string DEMO_ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e579";
            const string DEMO_ADMIN_ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e909";

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = DEMO_ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "ADMIN" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser {
                    Id = DEMO_ADMIN_ID,
                    FirstName = "Demo",
                    LastName = "Admin",
                    UserName = "demoadmin@dotnet.project",
                    NormalizedUserName = "demoadmin@dotnet.project".ToUpper(),
                    Email = "demoadmin@dotnet.project",
                    NormalizedEmail = "demoadmin@dotnet.project".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "password"),
                }
            );

            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole {
                    RoleId = DEMO_ADMIN_ROLE_ID,
                    UserId = DEMO_ADMIN_ID
                }
            );

            builder.Entity<FormSubmitted>().HasData(new FormSubmitted { UserId = DEMO_ADMIN_ID, BankFormSubmit = false, IdentityFormSubmit = false });
            builder.Entity<UserAdditionalInfo>().HasData(new UserAdditionalInfo { UserId = DEMO_ADMIN_ID});
            builder.Entity<AccountStatus>().HasData(new AccountStatus { UserId = DEMO_ADMIN_ID, AccountApproved = false, BankApproved = false });
            builder.Entity<UserBankDetails>().HasData(new UserBankDetails { AppUserId = DEMO_ADMIN_ID });
        }

        private static void CreateUser(ModelBuilder builder) {
            const string DEMO_USER_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e571";
            const string DEMO_USER_ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e901";

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = DEMO_USER_ROLE_ID, Name = "User", NormalizedName = "USER" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(

                new ApplicationUser {
                    Id = DEMO_USER_ID,
                    FirstName = "Demo",
                    LastName = "User",
                    UserName = "demouser@dotnet.project",
                    NormalizedUserName = "demouser@dotnet.project".ToUpper(),
                    Email = "demouser@dotnet.project",
                    NormalizedEmail = "demouser@dotnet.project".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "password"),
                }
            );

            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole {
                    RoleId = DEMO_USER_ROLE_ID,
                    UserId = DEMO_USER_ID
                }
            );

            builder.Entity<FormSubmitted>().HasData(new FormSubmitted { UserId = DEMO_USER_ID, BankFormSubmit = false, IdentityFormSubmit = false });
            builder.Entity<UserAdditionalInfo>().HasData(new UserAdditionalInfo { UserId = DEMO_USER_ID});
            builder.Entity<AccountStatus>().HasData(new AccountStatus { UserId = DEMO_USER_ID, AccountApproved = false, BankApproved = false });
            builder.Entity<UserBankDetails>().HasData(new UserBankDetails { AppUserId = DEMO_USER_ID });
        }

        //Database relathionship on Applicationuser, UserRole and ApplicationUserRole
        protected void RelationWithUserRoleAndRoleConnector(ModelBuilder builder) {
            builder.Entity<ApplicationUserRole>().HasKey(key => new { key.UserId, key.RoleId });

            builder.Entity<ApplicationUserRole>()
                .HasOne<ApplicationUser>(userRole => userRole.ApplicationUser)
                .WithMany(user => user.ApplicationUserRole)
                .HasForeignKey(fk => fk.UserId);

            builder.Entity<ApplicationUserRole>()
                .HasOne<ApplicationRole>(userRole => userRole.ApplicationRole)
                .WithMany(role => role.ApplicationUserRole)
                .HasForeignKey(fk => fk.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        //Database relathionship on Users and verification token table
        protected void UserRelationToTokens(ModelBuilder builder) {
            builder.Entity<VerificationTokens>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithMany(x => x.Tokens)
                .OnDelete(DeleteBehavior.Cascade);
        }


        //Database relathionship on Users and Additional info
        protected void UserRelationToAdditionalInfo(ModelBuilder builder) {
            builder.Entity<UserAdditionalInfo>().HasKey(x=>x.UserId);
            builder.Entity<UserAdditionalInfo>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithOne(x => x.UserAdditionalInfo)
                .HasForeignKey<UserAdditionalInfo>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        //Database relathionship on Users and VerifyIdentity
        protected void UserRelationToVerifyIdentity(ModelBuilder builder) {
            builder.Entity<VerifyAppuser>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithMany(x => x.VerifyAppusers)
                .OnDelete(DeleteBehavior.Cascade);
        }

        //Database relathionship on Users and BankDetails
        protected void UserRelationToBankDetails(ModelBuilder builder) {
            builder.Entity<UserBankDetails>().HasKey(x => x.AppUserId);
            builder.Entity<UserBankDetails>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithOne(x => x.UserBankDetails)
                .HasForeignKey<UserBankDetails>(x=>x.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        //Database relathionship on Users and AccountStatus
        protected void UserRelationToAccountStatus(ModelBuilder builder) {
            builder.Entity<AccountStatus>().HasKey(x => x.UserId);
            builder.Entity<AccountStatus>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithOne(x => x.AccountStatus)
                .HasForeignKey<AccountStatus>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        //Database relathionship on Users and AccountStatus
        protected void UserRelationToFormSubmitted(ModelBuilder builder) {
            builder.Entity<FormSubmitted>().HasKey(x => x.UserId);
            builder.Entity<FormSubmitted>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithOne(x => x.FormSubmitted)
                .HasForeignKey<FormSubmitted>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
