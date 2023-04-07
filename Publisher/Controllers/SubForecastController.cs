using Microsoft.AspNetCore.Mvc;

namespace MicroTest1.Controllers;

[ApiController]
[Route("[controller]")]
public class SubForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<SubForecastController> _logger;
    private readonly IActivityEventService _eventService;

    public SubForecastController(ILogger<SubForecastController> logger ,IActivityEventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();


    }
}
