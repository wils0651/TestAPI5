using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.ExternalTypes;

namespace TestAPI5.Contracts
{
    public interface IMessageService
    {
        Task<MessageReturn> GetByIdAsync(long messageId);
        Task<List<MessageReturn>> GetMessagesAsync();

        public Task<List<ComputerInfoReturn>> ListComputerInfoReturnsAsync();
    }
}
