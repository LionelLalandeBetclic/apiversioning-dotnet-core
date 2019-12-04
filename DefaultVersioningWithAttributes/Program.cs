using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DefaultVersioningInHeaders
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        internal static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
