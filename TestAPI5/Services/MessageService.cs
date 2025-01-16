using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI5.Contracts;
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

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _context.Message
                .Include(m => m.Computer)
                .Include(m => m.ComputerTask)
                .ToListAsync();
        }
    }
}
