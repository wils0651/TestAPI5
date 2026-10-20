using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts.Repositories
{
    public interface IPermitFindingRepository
    {
        Task<List<PermitFinding>> ListRecentAsync(DateTime cutoff);
    }
}
