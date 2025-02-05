using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts
{
    public interface IProbeDataRepository
    {
        Task<List<ProbeData>> ListProbeDataAsync(DateTime startDate);
        Task<List<ProbeData>> ListByProbeIdAsync(int probeId, DateTime startDate);
        Task<ProbeData> GetLatestByProbeIdAsync(int probeId);
    }
}
