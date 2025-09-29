using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class GarageDistanceService : IGarageDistanceService
    {
        private readonly IGarageDistanceRepository _garageDistanceRepository;

        public GarageDistanceService(IGarageDistanceRepository garageDistanceRepository)
        {
            _garageDistanceRepository = garageDistanceRepository;
        }

        public async Task<List<GarageDistanceReturn>> ListAsync(int count = 25)
        {
            var garageDistances = await _garageDistanceRepository.ListAsync(count);

            return garageDistances
                .Select(MapToReturn)
                .ToList();
        }

        public async Task<GarageDistanceReturn> GetLatestAsync()
        {
            var garageDistance = await _garageDistanceRepository.GetLatestAsync();
            return MapToReturn(garageDistance);
        }

        private static GarageDistanceReturn MapToReturn(GarageDistance garageDistance)
        {
            return new GarageDistanceReturn
            {
                GarageDistanceId = garageDistance.GarageDistanceId,
                CreatedDate = garageDistance.CreatedDate,
                Distance = garageDistance.Distance,
                GarageStatusName = garageDistance.GarageStatus.GarageStatusName
            };
        }
    }
}
