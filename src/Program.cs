using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace src
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostingConfig = new ConfigurationBuilder()
                .AddJsonFile("hosting.json", optional: false)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel(options => { options.Limits.MaxRequestBodySize = null; })
                .UseConfiguration(hostingConfig)
                .UseStartup<Startup>()
                .Build();

            await host.RunAsync();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
