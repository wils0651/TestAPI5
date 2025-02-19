using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class TemperatureStatisticRepository : ITemperatureStatisticRepository
    {
        private readonly DatabaseContext _context;

        public TemperatureStatisticRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<TemperatureStatistic>> ListAsync(int probeId)
        {
            return await _context.TemperatureStatistic
                .Include(ts => ts.Probe)
                .Where(ts => ts.ProbeId == probeId)
                .ToListAsync();
        }
    }
}
