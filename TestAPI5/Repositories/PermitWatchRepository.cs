using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class PermitWatchRepository : IPermitWatchRepository
    {
        private readonly PermitDatabaseContext _context;

        public PermitWatchRepository(PermitDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Permit>> ListPermitsAsync()
        {
            return await _context.Permit.ToListAsync();
        }

        public async Task<List<WatchWindow>> ListWatchWindowsAsync()
        {
            return await _context.WatchWindow.ToListAsync();
        }

        public async Task<List<WatchDateException>> ListWatchDateExceptionsAsync()
        {
            return await _context.WatchDateException.ToListAsync();
        }
    }
}
