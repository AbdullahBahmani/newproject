using BlazorCheckHealth.Data;
using BlazorCheckHealth.HealtchChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCheckHealth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<ResponseTimeHealthCheck>();
            services.AddHealthChecks()
                    //.AddCheck("Foo Service",
                    //    () => HealthCheckResult.Degraded("The check of the foo service did not worked well."),
                    //    new[] {"service"})
                    //.AddCheck("Bar Service",
                    //    () => HealthCheckResult.Healthy("The check of the foo service worked."),
                    //    new[] {"service"})
                    .AddCheck<ResponseTimeHealthCheck>("Network speed test", null, new[] { "service" })
                    .AddCheck("Database",
                    () => HealthCheckResult.Unhealthy("The check of the database did not worked."),
                    new[] {"database", "sql"});
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/quickhealth", new HealthCheckOptions()
                {
                    Predicate = _ => false
                });
                endpoints.MapHealthChecks("/health/services", new HealthCheckOptions()
                {
                    Predicate = reg => reg.Tags.Contains("service"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

                });
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
