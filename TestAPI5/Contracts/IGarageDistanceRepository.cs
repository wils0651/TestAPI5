using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts
{
    public interface IGarageDistanceRepository
    {
        Task<GarageDistance> GetLatestAsync();
        Task<List<GarageDistance>> ListAsync(int count = 25);
    }
}
