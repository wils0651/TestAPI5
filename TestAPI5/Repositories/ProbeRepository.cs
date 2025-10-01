using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class ProbeRepository : IProbeRepository
    {
        private readonly DatabaseContext _context;

        public ProbeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Probe> GetByIdAsync(int probeId)
        {
            return await _context.Probe
                .FirstOrDefaultAsync(p => p.ProbeId == probeId);
        }

        public async Task<List<Probe>> ListAsync()
        {
            return await _context.Probe
                .ToListAsync();
        }
    }
}
