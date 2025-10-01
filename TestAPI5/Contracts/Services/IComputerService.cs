using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts.Services
{
    public interface IComputerService
    {
        Task<ComputerDetailReturn> GetComputerDetailAsync(int computerId);
        Task<List<ComputerInfoReturn>> ListComputerInfoReturnsAsync();
        Task<ComputerDetailReturn> CreateComputerAsync(CreateComputerRequest request);
    }
}
