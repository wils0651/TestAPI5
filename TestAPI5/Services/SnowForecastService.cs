using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class SnowForecastService : ISnowForecastService
    {
        private readonly ISnowForecastRepository _snowForecastRepository;

        public SnowForecastService(ISnowForecastRepository snowForecastRepository)
        {
            _snowForecastRepository = snowForecastRepository;
        }

        public async Task<List<SnowForecastReturn>> GetLatestForecastAsync()
        {
            var forecasts = await _snowForecastRepository.GetLatestRunAsync();

            return forecasts
                .Select(MapToReturn)
                .ToList();
        }

        private static SnowForecastReturn MapToReturn(SnowForecast forecast)
        {
            return new SnowForecastReturn
            {
                TargetDate = forecast.TargetDate,
                LeadDays = forecast.LeadDays,
                PredictedSnowInches = forecast.PredictedSnowInches,
                IsEvent = forecast.IsEvent,
                GeneratedAt = forecast.GeneratedAt,
                WindowEndDate = forecast.WindowEndDate,
                ModelVersion = forecast.ModelVersion,
                // GeneratedAt is written as DateTime.UtcNow by the forecast job (Kind=Unspecified
                // once round-tripped through `timestamp without time zone`, but the underlying
                // value is still UTC), so it's compared against DateTime.UtcNow here.
                IsStale = DateTime.UtcNow.Subtract(forecast.GeneratedAt).TotalHours > 36
            };
        }
    }
}
