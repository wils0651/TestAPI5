using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class MessageService : IMessageService
    {
        private readonly TodoContext _context;

        public MessageService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<MessageReturn>> GetMessagesAsync()
        {
            var messages = await _context.Message
                .Include(m => m.Computer)
                .Include(m => m.ComputerTask)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();

            return messages
                .Select(MapToReturn)
                .ToList();
        }

        public async Task<MessageReturn> GetByIdAsync(long messageId)
        {
            var message = await _context.Message
                .Include(m => m.Computer)
                .Include(m => m.ComputerTask)
                .FirstOrDefaultAsync(m => m.MessageId == messageId);

            return MapToReturn(message);
        }

        private static MessageReturn MapToReturn(Message message)
        {
            return new MessageReturn
            {
                MessageId = message.MessageId,
                ComputerId = message.ComputerId,
                ComputerName = message.Computer.Name,
                ComputerDescription = message.Computer.Description,
                ComputerTaskId = message.ComputerTaskId,
                ComputerTaskName = message.ComputerTask.Name,
                ComputerTaskDescription = message.ComputerTask.Description,
                CreatedDate = message.CreatedDate.ToLocalTime(),
                Note = message.Note
            };
        }
    }
}
