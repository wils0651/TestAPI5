using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts.Services
{
    public interface ISnowForecastService
    {
        Task<List<SnowForecastReturn>> GetLatestForecastAsync();
        Task<SnowHindcastSeasonsReturn> GetHindcastSeasonsAsync();
        Task<SnowHindcastReturn> GetHindcastAsync(int season, IReadOnlyCollection<int> leads);
    }
}
