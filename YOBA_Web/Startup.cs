using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using System.IO;
using System.Reflection;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;
using YOBA_LibraryData.DAL;
using YOBA_Web.Filters;
using YOBA_Web.Models;
using YOBA_Web.Models.JwtAuth;
using YOBA_Web.Models.Logger;

namespace YOBA_Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "YOBA API",
                    Version = "v1",
                    Description = "YOBA Controllers API",
                    Contact = new OpenApiContact
                    {
                        Name = "Rifter",
                        Email = "kvpdotnet@gmail.com",
                        Url = new Uri("https://github.com/Hotriden"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
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
            #region Get User from context
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSession();
            #endregion
            #region Autentification
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.SignIn.RequireConfirmedEmail = true;

                config.Lockout.AllowedForNewUsers = true;
                //config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                //config.Lockout.MaxFailedAccessAttempts = 3;
            })
                .AddEntityFrameworkStores<YOBA_IdentityContext>()
                .AddDefaultTokenProviders();
            services.AddTokenAuthentication(Configuration);
            services.AddDbContext<YOBA_IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("YOBA_IdentityContext")));
            services.AddAntiforgery(o => {
                o.Cookie.Name = "X-CSRF-TOKEN";
            });

            services.AddMailKit(config => config.UseMailKit(Configuration.GetSection("Email").Get<MailKitOptions>()));
            #endregion
            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(1));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.

            #endregion
            #region Logging
            if (Directory.GetCurrentDirectory() + "/Logs" != null)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Logs");
            }
            string path = Directory.GetCurrentDirectory() + "/Logs";
            loggerFactory.AddFile(Path.Combine(path, $"{DateTime.UtcNow.Date.ToString("yyyy/MM/dd")}_logs.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");
            #endregion
            #region Global Exception Handler
            app.UseMiddleware<ExceptionMiddleware>();
            #endregion
            app.UseHttpsRedirection();
            app.UseRouting();
            #region CORS
            app.UseCors("Web_UI");
            #endregion
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
