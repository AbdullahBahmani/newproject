
using LoggingApi;
using Serilog;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env}.json", true)
    .AddEnvironmentVariables()
   
    .AddCommandLine(args)
    .Build();
 // .AddUserSecrets(typeof(Program).Assembly)
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

    

    try
    {
        var app = Startup.Initialize(args);
        app.Run();
        return 0;

    }
    catch (Exception ex)
    {
        Log.Fatal(ex.Message);
        return 1;
    }
    finally
    {
        Log.CloseAndFlush();
    }
