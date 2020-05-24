using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YOBA_Web.Models;

[assembly: HostingStartup(typeof(YOBA_Web.Areas.Identity.IdentityHostingStartup))]
namespace YOBA_Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<YOBA_IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("YOBA_DbConnection")));
            });
        }
    }
}