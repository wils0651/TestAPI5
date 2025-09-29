using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class GarageDistanceRepository : IGarageDistanceRepository
    {
        private readonly DatabaseContext _context;
        public GarageDistanceRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GarageDistance>> ListAsync(int count = 25)
        {
            return await _context.GarageDistance
                .Include(g => g.GarageStatus)
                .OrderByDescending(g => g.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<GarageDistance> GetLatestAsync()
        {
            return await _context.GarageDistance
                .Include(g => g.GarageStatus)
                .OrderByDescending(g => g.CreatedDate)
                .FirstOrDefaultAsync();
        }
    }
}
