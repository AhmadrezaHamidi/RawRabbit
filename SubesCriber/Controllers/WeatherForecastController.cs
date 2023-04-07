using Microsoft.AspNetCore.Mvc;

namespace MicroTest1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IActivityEventService _eventService;


    public WeatherForecastController(ILogger<WeatherForecastController> logger, IActivityEventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var chatEvent = new ChatDeletedEvent("salam");
        _eventService.Send(chatEvent);

        _logger.LogInformation("fevevervrvrqqvvrvq");


        var createUserEvent = new CreateUserEvent("ahmad", "ahmad", "ahmad");
        _eventService.Send(createUserEvent);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
       .ToArray();



        _logger.LogInformation("fevevervrvrqqvvrvq");


    }
}
