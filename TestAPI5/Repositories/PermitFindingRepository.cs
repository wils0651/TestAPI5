using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class PermitFindingRepository : IPermitFindingRepository
    {
        private readonly PermitDatabaseContext _context;

        public PermitFindingRepository(PermitDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<PermitFinding>> ListRecentAsync(DateTime cutoff)
        {
            return await _context.PermitFinding
                .Include(f => f.Permit)
                .Where(f => f.FoundAt >= cutoff)
                .OrderByDescending(f => f.FoundAt)
                .ToListAsync();
        }
    }
}
