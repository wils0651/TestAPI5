using Microsoft.EntityFrameworkCore;

namespace TestAPI5.Models
{
    // Second DbContext, second connection string ("SnowConnection") — the snow_forecasts table
    // lives in the snow_predictor database, a separate database from the one DatabaseContext
    // points at, populated by the SnowPredictor solution's forecast job.
    public class SnowDatabaseContext : DbContext
    {
        public SnowDatabaseContext(DbContextOptions<SnowDatabaseContext> options) : base(options) { }

        public DbSet<SnowForecast> SnowForecast { get; set; }
        public DbSet<SnowHindcast> SnowHindcast { get; set; }
        public DbSet<SnotelObservation> SnotelObservation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnowHindcast>().HasKey(h => new { h.ModelVersion, h.TargetDate, h.LeadDays });
            modelBuilder.Entity<SnotelObservation>().HasKey(o => new { o.StationId, o.ObservedOn });
        }
    }
}
