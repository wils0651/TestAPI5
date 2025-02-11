using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts
{
    public interface IProbeRepository
    {
        Task<Probe> GetByIdAsync(int probeId);
        Task<List<Probe>> ListAsync();
    }
}
