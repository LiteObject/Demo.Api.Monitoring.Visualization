using Demo.Weather.Api.Services;
using Prometheus;

namespace Demo.Weather.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            // Add services to the container.
            builder.Services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddSeq(config.GetSection("Seq"));
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IWeatherService, WeatherService>();

            var app = builder.Build();

            // HTTP Logging is enabled with UseHttpLogging, which adds HTTP logging middleware
            app.UseHttpLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHttpMetrics();

            app.UseAuthorization();

            app.MapControllers();

            app.MapMetrics();

            app.Run();
        }
    }
}