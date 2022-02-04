using Microsoft.Extensions.Configuration;
using Nito.AsyncEx;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

static async Task MainAsync(string[] args)
{
    // for (int i = 0; i < 100; i++)
    // {
    //     await Task.Delay(1000);
    //      Console.Write("\r{0}   ",i);
    // }
    //  await Task.Delay(1000);
    
    //  return ;
    // var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    // var configurationBuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
    // var configuration= configurationBuilder.Build();
    // var jaegerHost = configuration.GetValue<string>("openTelemetry:jaegerHost");
    var jaegerHost ="localhost";
    using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                                    .SetResourceBuilder(ResourceBuilder
                                                        .CreateDefault()
                                                        .AddService(typeof(Program).Assembly.GetName().Name))
                                    .AddHttpClientInstrumentation()
                                    .AddJaegerExporter(options =>{
                                        options.AgentHost= jaegerHost;
                                    })
                                    .AddConsoleExporter()
                                    .Build();
                                    



    HttpClient httpClient = new HttpClient();
    string apiUrl="http://localhost:80";
    httpClient.BaseAddress= new Uri(apiUrl);
    while(true)
    {
        Thread.Sleep(1000);
        try
        {
            var response = await httpClient.GetAsync("/weatherforecast");
            string data= await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {data}");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"{ex.Message}\n {ex.StackTrace}");
        }
    }

}


AsyncContext.Run(()=> MainAsync(args));