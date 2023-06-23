using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoKestrel
{
    public class Startup
    {
        public const string AllowSpecificOrigins = "_myAllowSpecificOrigins";

        public readonly DemoSettings Settings = DemoSettings.Default();

        public Startup(IConfiguration configuration)
        {
            configuration.GetSection("Demo").Bind(Settings);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (!Settings.DisableAuthorization)
            {
                services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
                services.AddAuthorization(options =>
                {
                    options.FallbackPolicy = options.DefaultPolicy;
                });
            }

            services.AddCors((options) =>
            {
                options.AddPolicy(
                    name: AllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("*"); // TODO: maybe later disable it optionally in a configuration file.
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            if (!Settings.DisableAuthorization)
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            app.UseCors(AllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
