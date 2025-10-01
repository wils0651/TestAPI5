using Microsoft.EntityFrameworkCore;

namespace TestAPI5.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItem { get; set; }

        public DbSet<Computer> Computer { get; set; }
        public DbSet<ComputerTask> ComputerTask { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<UnclassifiedMessage> UnclassifiedMessage { get; set; }
        public DbSet<Probe> Probe { get; set; }
        public DbSet<ProbeData> ProbeData { get; set; }
        public DbSet<TemperatureStatistic> TemperatureStatistic { get; set; }
        public DbSet<GarageDistance> GarageDistance { get; set; }
        public DbSet<GarageEventLog> GarageEventLog { get; set; }
        public DbSet<GarageEventType> GarageEventType { get; set; }
        public DbSet<GarageStatus> GarageStatus { get; set; }
    }
}