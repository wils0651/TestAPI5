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

        // Snow season runs Oct 1 through May 31; a season is identified by the year it starts in.
        private const int SeasonStartMonth = 10;
        private const int SeasonEndMonth = 5;
        private const string SnotelStationId = "370";
        private const int SnotelLookbackDays = 30;

        public static bool IsInSeason(DateOnly date) => date.Month >= SeasonStartMonth || date.Month <= SeasonEndMonth;

        public static int SeasonOf(DateOnly date) => date.Month >= SeasonStartMonth ? date.Year : date.Year - 1;

        public async Task<SnowHindcastSeasonsReturn> GetHindcastSeasonsAsync()
        {
            var modelVersion = await _snowForecastRepository.GetLatestHindcastModelVersionAsync();
            if (modelVersion == null)
            {
                return new SnowHindcastSeasonsReturn();
            }

            var dates = await _snowForecastRepository.GetHindcastTargetDatesAsync(modelVersion);
            return new SnowHindcastSeasonsReturn
            {
                ModelVersion = modelVersion,
                Seasons = dates.Where(IsInSeason).Select(SeasonOf).Distinct().OrderBy(s => s).ToList()
            };
        }

        public async Task<SnowHindcastReturn> GetHindcastAsync(int season, IReadOnlyCollection<int> leads)
        {
            var seasonStart = new DateOnly(season, SeasonStartMonth, 1);
            var seasonEnd = new DateOnly(season + 1, SeasonEndMonth, 31);

            var modelVersion = await _snowForecastRepository.GetLatestHindcastModelVersionAsync();
            var result = new SnowHindcastReturn
            {
                Season = season,
                SeasonStart = seasonStart,
                SeasonEnd = seasonEnd,
                ModelVersion = modelVersion
            };

            // Fetch some days before the season so the first day's diff has a previous row to compare to.
            var observations = await _snowForecastRepository.GetSnotelAsync(
                SnotelStationId, seasonStart.AddDays(-SnotelLookbackDays), seasonEnd);
            result.Actuals = ComputeActuals(observations)
                .Where(a => a.Date >= seasonStart && a.Date <= seasonEnd)
                .ToList();

            if (modelVersion != null)
            {
                var hindcasts = await _snowForecastRepository.GetHindcastAsync(modelVersion, seasonStart, seasonEnd, leads);
                result.Predictions = hindcasts
                    .GroupBy(h => h.LeadDays)
                    .OrderBy(g => g.Key)
                    .Select(g => new SnowHindcastLead
                    {
                        LeadDays = g.Key,
                        Points = g.OrderBy(h => h.TargetDate).Select(h => new SnowHindcastPoint
                        {
                            TargetDate = h.TargetDate,
                            PredictedSnowInches = h.PredictedSnowInches,
                            IsEvent = h.IsEvent
                        }).ToList()
                    })
                    .ToList();
            }

            return result;
        }

        /// <summary>
        /// Observed daily snowfall, using the same definition the model is trained on (the SNOTEL
        /// "target" in snow-model-trainer/features.py `load_snotel_daily` and the SnowPredictor
        /// solution's FeatureFrameService.BuildSnotelDaily): compared with the previous row (not
        /// previous calendar day), a day counts as snow only when snow depth AND SWE both rose, and
        /// the amount is the depth increase. Keep these in sync.
        /// </summary>
        public static List<SnowActualPoint> ComputeActuals(IReadOnlyList<SnotelObservation> observations)
        {
            var actuals = new List<SnowActualPoint>();
            SnotelObservation previous = null;
            foreach (var current in observations.OrderBy(o => o.ObservedOn))
            {
                // A null/NaN on either side makes the comparison false, as pandas' NaN > 0 does.
                var depthDiff = current.SnowDepth - previous?.SnowDepth;
                var sweDiff = current.Swe - previous?.Swe;
                var didSnow = depthDiff > 0 && sweDiff > 0;
                actuals.Add(new SnowActualPoint
                {
                    Date = current.ObservedOn,
                    SnowInches = didSnow ? depthDiff.Value : 0,
                    DidSnow = didSnow
                });
                previous = current;
            }

            return actuals;
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
