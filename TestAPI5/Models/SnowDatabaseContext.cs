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
    }
}
