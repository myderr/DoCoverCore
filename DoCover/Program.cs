using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DoCover
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://0.0.0.0:443");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
