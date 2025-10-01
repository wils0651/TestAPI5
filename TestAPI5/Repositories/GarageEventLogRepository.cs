using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class GarageEventLogRepository : IGarageEventLogRepository
    {
        private readonly DatabaseContext _context;

        public GarageEventLogRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GarageEventLog>> ListLatestAsync(int count = 25)
        {
            return await _context.GarageEventLog
                .AsNoTracking()
                .Include(gel => gel.GarageEventType)
                .OrderByDescending(gel => gel.GarageEventLogId)
                .Take(count)
                .ToListAsync();
        }

        public async Task<GarageEventLog> GetLatestAsync()
        {
            return await _context.GarageEventLog
                .AsNoTracking()
                .Include(gel => gel.GarageEventType)
                .OrderByDescending(gel => gel.GarageEventLogId)
                .FirstOrDefaultAsync();
        }
    }
}
