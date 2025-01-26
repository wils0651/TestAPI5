using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts
{
    public interface IComputerTaskRepository
    {
        Task<List<ComputerTask>> ListAsync();
    }
}
