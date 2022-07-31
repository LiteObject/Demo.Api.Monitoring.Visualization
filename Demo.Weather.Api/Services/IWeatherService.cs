namespace Demo.Weather.Api.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast[]> GeForecastAsync();
    }
}
