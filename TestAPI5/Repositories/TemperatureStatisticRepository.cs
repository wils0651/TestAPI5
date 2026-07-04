using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
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

        public async Task<List<TemperatureStatistic>> ListAsync(int probeId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.TemperatureStatistic
                .Include(ts => ts.Probe)
                .Where(ts => ts.ProbeId == probeId);

            if (startDate.HasValue)
            {
                query = query.Where(ts => ts.MeasurementDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(ts => ts.MeasurementDate <= endDate.Value);
            }

            return await query.OrderBy(ts => ts.MeasurementDate).ToListAsync();
        }
    }
}
