using Microsoft.EntityFrameworkCore;

namespace TestAPI5.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItem { get; set; }

        public DbSet<Computer> Computer { get; set; }
        public DbSet<Models.ComputerTask> ComputerTask { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}