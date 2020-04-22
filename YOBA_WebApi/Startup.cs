using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;
using YOBA_LibraryData.DAL;
using YOBA_WebApi.Logger;
using Microsoft.AspNetCore.Identity;


namespace YOBA_WebApi
{
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
            string connectionString = Configuration.GetConnectionString("YOBA_DbConnection");

            #region Data access layer
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<YOBAContext>(options => options.UseSqlServer(connectionString));
            #endregion

            #region Autentification
            //services.AddDbContext<YOBA_IdentityContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString(connectionString)));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<YOBA_IdentityContext>();
            #endregion

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
