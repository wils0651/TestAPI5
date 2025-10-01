using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts.Services
{
    public interface IProbeService
    {
        Task<ProbeReturn> GetByIdAsync(int probeId);
        Task<List<ProbeReturn>> ListAsync();
    }
}
