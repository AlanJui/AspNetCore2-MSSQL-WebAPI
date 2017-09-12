using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebAPI_001
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"args[] = {args}");
            BuildWebHost(args).Run();
        }

        // Ref URL: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/hosting?tabs=aspnetcore2x
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    IHostingEnvironment env = builderContext.HostingEnvironment;

                    config
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("hosting.json", optional: true)
                        .AddCommandLine(args);

                    //var builder = new ConfigurationBuilder()
                    //    .SetBasePath(Directory.GetCurrentDirectory())
                    //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    //Configuration = builder.Build();
                    //myURL = Configuration["API_URL"];
                    //Console.WriteLine($"myURL = {myURL}");
                })
                //.UseUrls("http://0.0.0.0:5000")
                .PreferHostingUrls(true)
                .UseStartup<Startup>()
                .Build();
    }
}
