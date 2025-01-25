using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts;
using TestAPI5.Models;

namespace TestAPI5.Repositories
{
    public class UnclassifiedMessageRepository : IUnclassifiedMessageRepository
    {
        private readonly DatabaseContext _context;

        public UnclassifiedMessageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<UnclassifiedMessage>> ListAsync()
        {
            return await _context.UnclassifiedMessage
                .OrderByDescending(u => u.CreatedDate)
                .ToListAsync();
        }
    }
}
