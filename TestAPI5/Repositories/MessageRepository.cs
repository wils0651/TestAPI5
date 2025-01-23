using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext _context;

        public MessageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _context.Message
                .Include(m => m.Computer)
                .Include(m => m.ComputerTask)
                .ToListAsync();
        }

        public async Task<Message> GetByIdAsync(long messageId)
        {
            return await _context.Message
                .Include(m => m.Computer)
                .Include(m => m.ComputerTask)
                .FirstOrDefaultAsync(m => m.MessageId == messageId);
        }
    }
}
