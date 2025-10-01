using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class GarageEventLogService : IGarageEventLogService
    {
        private readonly IGarageEventLogRepository _garageEventLogRepository;

        public GarageEventLogService(IGarageEventLogRepository garageEventLogRepository)
        {
            _garageEventLogRepository = garageEventLogRepository;
        }

        public async Task<List<GarageEventLogReturn>> ListLatestAsync(int count = 25)
        {
            var latestGarageEventLogs = await _garageEventLogRepository.ListLatestAsync(count);

            return latestGarageEventLogs
                .Select(MapToReturn)
                .ToList();
        }

        public async Task<GarageEventLogReturn> GetLatestAsync()
        {
            var latestGarageEventLog = await _garageEventLogRepository.GetLatestAsync();

            return MapToReturn(latestGarageEventLog);
        }

        private static GarageEventLogReturn MapToReturn(GarageEventLog garageEventLog)
        {
            return new GarageEventLogReturn
            {
                GarageEventLogId = garageEventLog.GarageEventLogId,
                GarageEventTypeId = garageEventLog.GarageEventTypeId,
                GarageEventTypeName = garageEventLog.GarageEventType.GarageEventTypeName,
                CreatedDate = garageEventLog.CreatedDate,
            };
        }
    }
}
