using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts
{
    public interface IProbeDataService
    {
        Task<List<ProbeDataReturn>> ListAsync(DateTime? startDate);
        Task<List<ProbeDataReturn>> ListByProbeIdAsync(int probeId, DateTime? startDate);
        Task<ProbeDataReturn> GetLatestByProbeIdAsync(int probeId);
    }
}
