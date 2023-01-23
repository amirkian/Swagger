using Microsoft.AspNetCore.Mvc;

namespace Swagger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


    }

    public class PolymorphismController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DynamicType>), StatusCodes.Status200OK)]
        public IEnumerable<DynamicType> Get()
        {
            return Enumerable.Range(1, 5).Select(index => {
                if (index % 2 == 0)
                {
                    return new DynamicType<string>()
                    {
                        DataType = "string",
                        Value = "This is my string value"
                    } as DynamicType;
                }
                return new DynamicType<decimal?>()
                {
                    DataType = "number",
                    Value = new decimal?(99.0m)
                } as DynamicType;
            })
            .ToArray();
        }
    }
}


public class DynamicType
    {
        public string DataType { get; set; }
    }

    public class DynamicType<T> : DynamicType
    {
        public T Value { get; set; }
    }
