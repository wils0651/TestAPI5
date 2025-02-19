using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts
{
    public interface ITemperatureStatisticRepository
    {
        Task<List<TemperatureStatistic>> ListAsync(int probeId);
    }
}
