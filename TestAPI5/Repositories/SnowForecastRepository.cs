using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class SnowForecastRepository : ISnowForecastRepository
    {
        private readonly SnowDatabaseContext _context;

        public SnowForecastRepository(SnowDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<SnowForecast>> GetLatestRunAsync()
        {
            var latestGeneratedAt = await _context.SnowForecast
                .OrderByDescending(f => f.GeneratedAt)
                .Select(f => f.GeneratedAt)
                .FirstOrDefaultAsync();

            return await _context.SnowForecast
                .Where(f => f.GeneratedAt == latestGeneratedAt)
                .OrderBy(f => f.TargetDate)
                .ToListAsync();
        }
    }
}
