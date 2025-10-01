using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class TemperatureStatisticService : ITemperatureStatisticService
    {
        private readonly ITemperatureStatisticRepository _temperatureStatisticRepository;

        public TemperatureStatisticService(ITemperatureStatisticRepository temperatureStatisticRepository)
        {
            _temperatureStatisticRepository = temperatureStatisticRepository;
        }

        public async Task<List<TemperatureStatisticReturn>> ListAsync(int probeId)
        {
            var temperatureStatistics = await _temperatureStatisticRepository.ListAsync(probeId);

            return temperatureStatistics
                .Select(MapToReturn)
                .ToList();
        }

        private static TemperatureStatisticReturn MapToReturn(TemperatureStatistic temperatureStatistic)
        {
            return new TemperatureStatisticReturn
            {
                TemperatureStatisticId = temperatureStatistic.TemperatureStatisticId,
                ProbeId = temperatureStatistic.ProbeId,
                ProbeDescription = temperatureStatistic.Probe.Description,
                DataCount = temperatureStatistic.DataCount,
                Mean = temperatureStatistic.Mean,
                StandardDeviation = temperatureStatistic.StandardDeviation,
                Minimum = temperatureStatistic.Minimum,
                Maximum = temperatureStatistic.Maximum,
                MeasurementDate = temperatureStatistic.MeasurementDate
            };
        }
    }
}
