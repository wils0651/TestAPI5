using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts.Services
{
    public interface IComputerTaskService
    {
        Task<List<ComputerTaskReturn>> ListAsync();
    }
}
