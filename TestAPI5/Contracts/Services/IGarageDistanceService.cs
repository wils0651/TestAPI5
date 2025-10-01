using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts.Services
{
    public interface IGarageDistanceService
    {
        Task<GarageDistanceReturn> GetLatestAsync();
        Task<List<GarageDistanceReturn>> ListAsync(int count = 25);
    }
}
