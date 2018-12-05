using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("hosting.json", optional: true)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(
                    (context, options) => options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                .UseKestrel();

            var threadCount = GetThreadCount(config);

            webHostBuilder.UseSockets(x =>
            {
                if (threadCount > 0)
                {
                    x.IOQueueCount = threadCount;
                }

                Console.WriteLine($"Using Sockets with {x.IOQueueCount} threads");
            });

            return webHostBuilder;
        }

        private static int GetThreadCount(IConfigurationRoot config)
        {
            var threadCountValue = config["threadCount"];
            return threadCountValue == null ? -1 : int.Parse(threadCountValue);
        }
    }
}
