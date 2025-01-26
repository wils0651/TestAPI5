using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class ComputerTaskRepository : IComputerTaskRepository
    {
        private readonly DatabaseContext _context;

        public ComputerTaskRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<ComputerTask>> ListAsync()
        {
            return await _context.ComputerTask
                .OrderBy(c => c.ComputerTaskId)
                .ToListAsync();
        }
    }
}
