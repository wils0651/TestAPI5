using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("snow_forecasts")]
    public class SnowForecast
    {
        [Key]
        [Column("forecast_id")]
        public long ForecastId { get; set; }

        [Column("generated_at", TypeName = "timestamp without time zone")]
        public DateTime GeneratedAt { get; set; }

        [Column("window_end_date")]
        public DateOnly WindowEndDate { get; set; }

        [Column("target_date")]
        public DateOnly TargetDate { get; set; }

        [Column("lead_days")]
        public int LeadDays { get; set; }

        [Column("predicted_snow_inches")]
        public double PredictedSnowInches { get; set; }

        [Column("is_event")]
        public bool IsEvent { get; set; }

        [Column("model_version")]
        public string ModelVersion { get; set; }
    }
}
