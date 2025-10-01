using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts.Repositories
{
    public interface IComputerRepository
    {
        Task<Computer> GetComputerByIdAsync(int computerId);
        public Task<List<Computer>> ListComputersAsync();
        void Add(Computer computer);
        Task SaveChangesAsync();
    }
}
