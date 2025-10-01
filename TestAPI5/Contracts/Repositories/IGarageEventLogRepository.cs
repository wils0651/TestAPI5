using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts.Repositories
{
    public interface IGarageEventLogRepository
    {
        Task<GarageEventLog> GetLatestAsync();
        Task<List<GarageEventLog>> ListLatestAsync(int count = 25);
    }
}
