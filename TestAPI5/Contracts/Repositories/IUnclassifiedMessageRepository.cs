using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts.Repositories
{
    public interface IUnclassifiedMessageRepository
    {
        Task<List<UnclassifiedMessage>> ListAsync();

        Task<List<UnclassifiedMessage>> ListLatestAsync(int numberOfDays);
    }
}
