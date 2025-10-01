using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class ProbeDataRepository : IProbeDataRepository
    {
        private readonly DatabaseContext _context;
        public ProbeDataRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<ProbeData>> ListProbeDataAsync(DateTime startDate)
        {
            return await _context.ProbeData
                .Include(pd => pd.Probe)
                .Where(pd => pd.CreatedDate >= startDate)
                .ToListAsync();
        }

        public async Task<List<ProbeData>> ListByProbeIdAsync(int probeId, DateTime startDate)
        {
            return await _context.ProbeData
                .Include(pd => pd.Probe)
                .Where(pd => pd.ProbeId == probeId)
                .Where(pd => pd.CreatedDate >= startDate)
                .ToListAsync();
        }

        public async Task<ProbeData> GetLatestByProbeIdAsync(int probeId)
        {
            return await _context.ProbeData
                .Include(pd => pd.Probe)
                .Where(pd => pd.ProbeId == probeId)
                .OrderByDescending(pd => pd.CreatedDate)
                .FirstOrDefaultAsync();
        }
    }
}
