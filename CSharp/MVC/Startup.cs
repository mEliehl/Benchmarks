using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mvc.Data;
using MVC.Configuration;
using MVC.Data;
using Npgsql;
using System;
using System.Data.Common;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Mvc
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnv)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnv.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnv.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                ;

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);

            var appSettings = Configuration.Get<AppSettings>();
            Console.WriteLine($"Database: {appSettings.ConnectionString}");
            services.AddSingleton<DbProviderFactory>(NpgsqlFactory.Instance);
            services.AddScoped<RawDb>();
            services.AddScoped<DapperDb>();

            var settings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.Katakana, UnicodeRanges.Hiragana);
            settings.AllowCharacter('\u2014');  // allow EM DASH through
            services.AddWebEncoders((options) =>
            {
                options.TextEncoderSettings = settings;
            });


            services
                .AddMvcCore()
                .AddViews()
                .AddRazorViewEngine()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc().UseStaticFiles();
        }
    }
}
