using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class {ModelName}Controller : ControllerBase
    {
        private static readonly string[] {ModelObject} = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<{ModelName}Controller> _logger;

        public {ModelName}Controller(ILogger<{ModelName}Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Get{ModelName}")]
        public IEnumerable<{ModelName}> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new {ModelName}
            {
                {ModelKey} = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}