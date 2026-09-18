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
    }
}
