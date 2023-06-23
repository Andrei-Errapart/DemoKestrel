using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoKestrel
{
    // Sample app from: https://github.com/dotnet/aspnetcore/tree/main/src/Servers/Kestrel/samples/SampleApp
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
            //     "windowsAuthentication": true,
            // "anonymousAuthentication": true,
            builder.UseConsoleLifetime(); // https://stackoverflow.com/questions/64837558/how-to-cancel-webhost-when-process-is-stopping

            var r = builder.ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://0.0.0.0:4444");
                    webBuilder.UseStartup<Startup>();
                });
            return r;

        }
    }
}
