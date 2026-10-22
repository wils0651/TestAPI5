using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts.Repositories
{
    public interface ISnowForecastRepository
    {
        Task<List<SnowForecast>> GetLatestRunAsync();

        Task<string> GetLatestHindcastModelVersionAsync();
        Task<List<DateOnly>> GetHindcastTargetDatesAsync(string modelVersion);
        Task<List<SnowHindcast>> GetHindcastAsync(string modelVersion, DateOnly from, DateOnly to, IReadOnlyCollection<int> leads);
        Task<List<SnotelObservation>> GetSnotelAsync(string stationId, DateOnly from, DateOnly to);
    }
}
