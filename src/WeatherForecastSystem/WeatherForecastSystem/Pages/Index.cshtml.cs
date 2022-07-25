using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherForecastSystem.DTOs;
using WeatherForecastSystem.Interfaces;
using WeatherForecastSystem.Services;

namespace WeatherForecastSystem.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ILocationService _locationService;
    private readonly IWeatherService _weatherService;

    [BindProperty]
    public LocationRequest LocationRequest { get; set; }
    [BindProperty]
    public WeatherResponse WeatherResponse { get; set; }
    public IndexModel(ILogger<IndexModel> logger
        ,ILocationService locationService
        , IWeatherService weatherService)
    {
        _logger = logger;
        _locationService = locationService;
        _weatherService = weatherService;
    }

    public void OnGet()
    {

    }
    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid || LocationRequest is null)
        {
            ModelState.AddModelError(string.Empty,"Request Was Invalid");
            return Page();
        }
        var locations = await _locationService.GetLocationsResponseAsync(LocationRequest);
        if(locations.LocationResponses is not null && locations.LocationResponses.Any())
        {
            WeatherResponse = await _weatherService.GetWeatherResponseAsync(locationResponse: locations.LocationResponses.First());
        }
        return Page();
    }
}

