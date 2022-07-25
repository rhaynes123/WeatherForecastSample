using WeatherForecastSystem.Services;
using WeatherForecastSystem.Interfaces;
using Polly;
#region External Links
// https://www.youtube.com/watch?v=9pgvX_Dk0n8
#endregion External Links
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient(name:builder.Configuration["OpenWeatherApi:ClientName"],configureClient: client =>
{
    client.BaseAddress = new Uri("http://api.openweathermap.org/");

}).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder
.WaitAndRetryAsync( new[]
{
    TimeSpan.FromMilliseconds(300),
    TimeSpan.FromMilliseconds(600),
    TimeSpan.FromMilliseconds(800)
}));
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ILocationService, LocationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

