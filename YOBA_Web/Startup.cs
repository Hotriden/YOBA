using Microsoft.AspNetCore.Authentication.Cookies;
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
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using System.Text;
using System.Threading.Tasks;
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

            #region Autentification
            services.AddDbContext<YOBA_IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("YOBA_IdentityContext")));

            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Secret));

                    config.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }

                            return Task.CompletedTask;
                        }
                    };

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audiance,
                        IssuerSigningKey = key,
                    };
                });

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
                .AddEntityFrameworkStores<YOBA_IdentityContext> ()
                .AddDefaultTokenProviders();

            //services.ConfigureApplicationCookie(config =>
            //{
            //    config.Cookie.Name = "Identity.Cookie";
            //    config.LoginPath = "/Login/Login";
            //    config.LogoutPath = "/Api/Logout";
            //});

            services.AddMailKit(config => config.UseMailKit(Configuration.GetSection("Email").Get<MailKitOptions>()));
            #endregion

            services.AddControllers();
            services.AddTokenAuthentication(Configuration);

            #region CORS
            services.AddCors(config => config.AddPolicy(name: "Web_UI", builder =>
            {
                builder.WithOrigins("https://yoba.netlify.app/", "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
