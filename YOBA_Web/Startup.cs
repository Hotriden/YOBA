using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using React.AspNet;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;
using YOBA_LibraryData.DAL;
using YOBA_Web.Models;
using YOBA_Web.Models.Logger;

namespace YOBA_Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Data access layer
            string yobaDbConnection = "Server=(localdb)\\mssqllocaldb;Database=YobaDb;Trusted_Connection=True;MultipleActiveResultSets=true;";
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<YOBAContext>(options => options.UseSqlServer(yobaDbConnection));
            #endregion

            #region Autentification
            services.AddDbContext<YOBA_IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<YOBA_IdentityContext>();
            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDirectoryBrowser();

            #region React
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();
            services.AddControllers();
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            #region Logging
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.UtcNow.Date.ToString("dd/MM/yyyy")}_logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            #endregion

            #region React
            app.UseReact(config => { });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
