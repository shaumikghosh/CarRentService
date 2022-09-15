using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarRentService.Data;
using Microsoft.EntityFrameworkCore;
using DataModel.Models;
using CarRentService.Interfaces;
using CarRentService.Services;
using CarRentService.Areas.Admin.Interfaces;
using CarRentService.Areas.Admin.Services;
using CarRentService.Areas.User.Interfaces;
using CarRentService.Areas.User.Services;
using SendGrid.Helpers.Mail;
using System;

namespace CarRentService {
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddOptions();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

            services.AddRazorPages();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<DatabaseContext>();
            services.AddCors(option => {
                option.AddPolicy("MyPolicy", builder=> {
                    builder.WithOrigins("http://shaumik.somee.com");
                });
            });
            services.AddMvc();

            // For client side
            services.AddTransient<ISignup, SignupServices>();
            services.AddTransient<ILogin, LoginService>();
            services.AddTransient<ILogout, LogoutService>();
            services.AddTransient<IForgetPassword, ForgetPasswordService>();
            services.AddTransient<IContact, ContactService>();

            // For admin area
            services.AddTransient<IAdminLogin, AdminLoginService>();
            services.AddTransient<IAdminLogout, AdminLogoutService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISettings, SettingsService>();

            // For profile area
            services.AddTransient<IProfileSettings, ProfileSettingsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
