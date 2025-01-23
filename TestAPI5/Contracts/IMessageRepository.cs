using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Models;

namespace TestAPI5.Contracts
{
    public interface IMessageRepository
    {
        public Task<List<Message>> GetMessagesAsync();

        public Task<Message> GetByIdAsync(long messageId);

        public Task<Message> LastMessageByComputerIdAsync(int computerId);
    }
}
