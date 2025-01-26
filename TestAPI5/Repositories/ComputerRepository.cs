using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly DatabaseContext _context;

        public ComputerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Computer>> ListComputersAsync()
        {
            return await _context.Computer
                .OrderBy(c => c.ComputerId)
                .ToListAsync();
        }

        public async Task<Computer> GetComputerByIdAsync(int computerId)
        {
            return await _context.Computer
                .FirstOrDefaultAsync(c => c.ComputerId == computerId);
        }
    }
}
