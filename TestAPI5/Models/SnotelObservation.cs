using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    // Read-only view of the SnowPredictor solution's snotel_observations table (composite key
    // configured in SnowDatabaseContext).
    [Table("snotel_observations")]
    public class SnotelObservation
    {
        [Column("station_id")]
        public string StationId { get; set; }

        [Column("observed_on")]
        public DateOnly ObservedOn { get; set; }

        [Column("snow_depth")]
        public double? SnowDepth { get; set; }

        [Column("swe")]
        public double? Swe { get; set; }
    }
}
