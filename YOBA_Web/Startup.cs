using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;
using YOBA_LibraryData.DAL;
using YOBA_Web.Models;
using YOBA_Web.Models.JwtAuth;

namespace YOBA_Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Data access layer
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<YOBAContext>(config => config.UseSqlServer(Configuration.GetConnectionString("YOBA_DbConnection")));
            #endregion

            #region CORS

            services.AddCors(config => config.AddPolicy(name: "Web_UI", builder =>
            {
                builder.WithOrigins("http://yoba.netlify.app/")
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));
            #endregion
            services.AddControllers();
            #region Autentification
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.SignIn.RequireConfirmedEmail = true;

                config.Lockout.AllowedForNewUsers = true;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                config.Lockout.MaxFailedAccessAttempts = 3;
            })
                .AddEntityFrameworkStores<YOBA_IdentityContext>()
                .AddDefaultTokenProviders();
            services.AddTokenAuthentication(Configuration);
            services.AddDbContext<YOBA_IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("YOBA_IdentityContext")));
            services.AddHttpContextAccessor();
            services.AddAntiforgery(o => {
                o.Cookie.Name = "X-CSRF-TOKEN";
            });

            services.AddMailKit(config => config.UseMailKit(Configuration.GetSection("Email").Get<MailKitOptions>()));
            #endregion

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(1));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            #region Logging
            ////////////////////// LOGGER DON'T WORK ON SmarterASP.NET Hosting  ////////////
            //if (Directory.GetCurrentDirectory() + "/Logs" != null)
            //{
            //    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Logs");
            //}
            //string path = Directory.GetCurrentDirectory() + "/Logs";
            //loggerFactory.AddFile(Path.Combine(path, $"{DateTime.UtcNow.Date.ToString("yyyy/MM/dd")}_logs.txt"));
            //var logger = loggerFactory.CreateLogger("FileLogger");
            #endregion

            app.UseHttpsRedirection();
            app.UseRouting();

            #region CORS
            app.UseCors("Web_UI");
            #endregion

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


        }
    }
}
