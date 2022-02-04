using Microsoft.Extensions.Options;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;

namespace LoggingApi
{
    public class Startup
    {
     
        public static WebApplication Initialize(string[] args)
        {
            var builder = WebApplication
                            .CreateBuilder(args);
                            
            ConfigureServices (builder);
            builder.Host.UseSerilog();
            var app = builder.Build();
            Congifure (app);
            return app;
        }

        private static void Congifure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            app.UseRouting();
            app.UseHttpMetrics();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var jaegerHost= builder.Configuration.GetValue<string>("openTelemetry:jaegerHost");
            builder.Services.AddOpenTelemetryTracing (_builder =>
            {
                _builder.SetResourceBuilder (ResourceBuilder
                                            .CreateDefault()
                                            .AddService(builder.Environment.ApplicationName))
                                            .AddAspNetCoreInstrumentation()
                                            .AddHttpClientInstrumentation()
                                            .AddJaegerExporter(options =>
                                            {
                                                options.AgentHost = jaegerHost;
                                            })
                                            .AddConsoleExporter();
            });
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
