using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class ProbeDataService : IProbeDataService
    {
        private readonly IProbeDataRepository _probeDataRepository;

        public ProbeDataService(IProbeDataRepository probeDataRepository)
        {
            _probeDataRepository = probeDataRepository;
        }

        public async Task<List<ProbeDataReturn>> ListAsync(DateTime? startDate)
        {
            startDate ??= DateTime.Now.AddDays(-14);

            var probeDatas = await _probeDataRepository.ListProbeDataAsync(startDate.Value.ToUniversalTime());

            return probeDatas
                .Select(MapToReturn)
                .ToList();
        }

        public async Task<List<ProbeDataReturn>> ListByProbeIdAsync(int probeId, DateTime? startDate)
        {
            startDate ??= DateTime.Now.AddDays(-1);

            var probeDatas = await _probeDataRepository.ListByProbeIdAsync(
                probeId, startDate.Value.ToUniversalTime());

            return probeDatas
                .Select(MapToReturn)
                .ToList();
        }

        public async Task<ProbeDataReturn> GetLatestByProbeIdAsync(int probeId)
        {
            var probeData = await _probeDataRepository.GetLatestByProbeIdAsync(probeId);

            return MapToReturn(probeData);
        }

        private static ProbeDataReturn MapToReturn(ProbeData probeData)
        {
            return new ProbeDataReturn
            {
                ProbeDataId = probeData.ProbeDataId,
                ProbeId = probeData.ProbeId,
                ProbeName = probeData.Probe.ProbeName,
                ProbeDescription = probeData.Probe.Description,
                Temperature = probeData.Temperature,
                CreatedDate = probeData.CreatedDate,
            };
        }
    }
}
