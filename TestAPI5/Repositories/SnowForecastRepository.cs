using System;
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

        public async Task<string> GetLatestHindcastModelVersionAsync()
        {
            return await _context.SnowHindcast
                .OrderByDescending(h => h.GeneratedAt)
                .Select(h => h.ModelVersion)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DateOnly>> GetHindcastTargetDatesAsync(string modelVersion)
        {
            return await _context.SnowHindcast
                .Where(h => h.ModelVersion == modelVersion)
                .Select(h => h.TargetDate)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<SnowHindcast>> GetHindcastAsync(
            string modelVersion, DateOnly from, DateOnly to, IReadOnlyCollection<int> leads)
        {
            return await _context.SnowHindcast
                .AsNoTracking()
                .Where(h => h.ModelVersion == modelVersion
                    && h.TargetDate >= from && h.TargetDate <= to
                    && leads.Contains(h.LeadDays))
                .OrderBy(h => h.LeadDays).ThenBy(h => h.TargetDate)
                .ToListAsync();
        }

        public async Task<List<SnotelObservation>> GetSnotelAsync(string stationId, DateOnly from, DateOnly to)
        {
            return await _context.SnotelObservation
                .AsNoTracking()
                .Where(o => o.StationId == stationId && o.ObservedOn >= from && o.ObservedOn <= to)
                .OrderBy(o => o.ObservedOn)
                .ToListAsync();
        }
    }
}
