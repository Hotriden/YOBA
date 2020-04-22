using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace YOBA_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                ////////////////////// LOGGER DON'T WORK ON SmarterASP.NET Hosting  ////////////
                //ConfigureLogging(logBuilder =>
                //{
                //    logBuilder.ClearProviders();
                //    logBuilder.AddConsole();
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
