using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class ProbeService : IProbeService
    {
        IProbeRepository _probeRepository;
        public ProbeService(IProbeRepository probeRepository)
        {
            _probeRepository = probeRepository;
        }

        public async Task<ProbeReturn> GetByIdAsync(int probeId)
        {
            var probe = await _probeRepository.GetByIdAsync(probeId);

            return MapToReturn(probe);
        }

        public async Task<List<ProbeReturn>> ListAsync()
        {
            var probes = await _probeRepository.ListAsync();

            return probes
                .Select(MapToReturn)
                .ToList();
        }

        private static ProbeReturn MapToReturn(Probe probe)
        {
            if (probe == null)
            {
                return null;
            }

            return new ProbeReturn
            {
                ProbeId = probe.ProbeId,
                ProbeName = probe.ProbeName,
                Description = probe.Description,
            };
        }
    }
}
