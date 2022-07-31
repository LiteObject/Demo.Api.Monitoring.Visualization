using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace Demo.Weather.Api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ILogger<WeatherService> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherService(ILogger<WeatherService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            _logger.LogInformation($"Instantiated {nameof(WeatherService)} class.");
        }

        public async Task<WeatherForecast[]> GeForecastAsync()
        {
            _logger.LogInformation($"Invoked {nameof(WeatherService)}.{nameof(GeForecastAsync)} method.");

            var requestUriString = "http://host.docker.internal:5006/WeatherForecast";

            var envRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") ?? "false";

            if (Boolean.TryParse(envRunningInContainer, out var runningInContainer) && !runningInContainer)
            {
                requestUriString = "http://localhost:5006/WeatherForecast";
            }

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUriString)
            {
                Headers =
                {
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            _logger.LogDebug($"{nameof(httpResponseMessage.StatusCode)}: {httpResponseMessage?.StatusCode}");
            _logger.LogDebug($"{nameof(httpResponseMessage.ReasonPhrase)}: {httpResponseMessage?.ReasonPhrase}");

            List<WeatherForecast> results = new();

            if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                var tempResults = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(contentStream);

                _logger.LogDebug($"{nameof(tempResults)} length is {tempResults?.Count() ?? 0}");

                if (tempResults?.Count() > 0)
                {
                    results.AddRange(tempResults);
                    _logger.LogDebug($"Added {nameof(tempResults)} to {nameof(results)}");
                }
            }

            /*var results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray(); */

            return await Task.FromResult(results.ToArray());
        }
    }
}
