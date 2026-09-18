using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts.Services
{
    public interface IPermitWatchService
    {
        Task<List<PermitFindingReturn>> ListRecentFindingsAsync(DateTime? startDate);
        Task<List<PermitWatchReturn>> ListWatchConfigAsync();
    }
}
