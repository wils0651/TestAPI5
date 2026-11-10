using System;

namespace TestAPI5.ExternalTypes
{
    public class SnowForecastReturn
    {
        public DateOnly TargetDate { get; set; }
        public int LeadDays { get; set; }
        public double PredictedSnowInches { get; set; }
        /// <summary>Calibrated P(snow), 0–1; null when the forecast model doesn't produce one (buoy LSTM).</summary>
        public double? SnowProbability { get; set; }
        public bool IsEvent { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateOnly WindowEndDate { get; set; }
        public string ModelVersion { get; set; }
        public bool IsStale { get; set; }
    }
}
