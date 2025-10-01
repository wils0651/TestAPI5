using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts
{
    public interface IGarageEventLogService
    {
        Task<GarageEventLogReturn> GetLatestAsync();
        Task<List<GarageEventLogReturn>> ListLatestAsync(int count = 25);
    }
}
