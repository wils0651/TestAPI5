using Microsoft.EntityFrameworkCore;

namespace TestAPI5.Models
{
    public class PermitDatabaseContext : DbContext
    {
        public PermitDatabaseContext(DbContextOptions<PermitDatabaseContext> options) : base(options) { }

        public DbSet<Permit> Permit { get; set; }
        public DbSet<WatchWindow> WatchWindow { get; set; }
        public DbSet<WatchDateException> WatchDateException { get; set; }
        public DbSet<PermitFinding> PermitFinding { get; set; }
    }
}
