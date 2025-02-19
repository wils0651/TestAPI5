using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts
{
    public interface ITemperatureStatisticService
    {
        Task<List<TemperatureStatisticReturn>> ListAsync(int probeId);
    }
}
