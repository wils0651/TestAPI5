using System;
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

        public async Task<WatchWindow> GetWatchWindowAsync(int permitId)
        {
            return await _context.WatchWindow
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.PermitId == permitId);
        }

        public void AddWatchWindow(WatchWindow watchWindow)
        {
            _context.WatchWindow.Add(watchWindow);
        }

        public void UpdateWatchWindow(WatchWindow watchWindow)
        {
            _context.WatchWindow.Update(watchWindow);
        }

        public async Task<WatchDateException> GetWatchDateExceptionAsync(int permitId, DateOnly exceptionDate)
        {
            return await _context.WatchDateException
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.PermitId == permitId && e.ExceptionDate == exceptionDate);
        }

        public void AddWatchDateException(WatchDateException watchDateException)
        {
            _context.WatchDateException.Add(watchDateException);
        }

        public void UpdateWatchDateException(WatchDateException watchDateException)
        {
            _context.WatchDateException.Update(watchDateException);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
