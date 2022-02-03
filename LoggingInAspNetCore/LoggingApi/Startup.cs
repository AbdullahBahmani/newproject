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
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
