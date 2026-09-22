using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts.Repositories
{
    public interface IPermitWatchRepository
    {
        Task<List<Permit>> ListPermitsAsync();
        Task<List<WatchWindow>> ListWatchWindowsAsync();
        Task<List<WatchDateException>> ListWatchDateExceptionsAsync();

        Task<WatchWindow> GetWatchWindowAsync(int permitId);
        void AddWatchWindow(WatchWindow watchWindow);
        void UpdateWatchWindow(WatchWindow watchWindow);

        Task<WatchDateException> GetWatchDateExceptionAsync(int permitId, DateOnly exceptionDate);
        void AddWatchDateException(WatchDateException watchDateException);
        void UpdateWatchDateException(WatchDateException watchDateException);

        Task SaveChangesAsync();
    }
}
