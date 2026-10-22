using System;
using System.Collections.Generic;

namespace TestAPI5.ExternalTypes
{
    public class SnowHindcastSeasonsReturn
    {
        public string ModelVersion { get; set; }
        // Each value is the calendar year the season starts in (2019 = the 2019–20 season).
        public List<int> Seasons { get; set; } = new List<int>();
    }

    public class SnowHindcastReturn
    {
        public int Season { get; set; }
        public DateOnly SeasonStart { get; set; }
        public DateOnly SeasonEnd { get; set; }
        public string ModelVersion { get; set; }
        public List<SnowActualPoint> Actuals { get; set; } = new List<SnowActualPoint>();
        public List<SnowHindcastLead> Predictions { get; set; } = new List<SnowHindcastLead>();
    }

    public class SnowActualPoint
    {
        public DateOnly Date { get; set; }
        public double SnowInches { get; set; }
        public bool DidSnow { get; set; }
    }

    public class SnowHindcastLead
    {
        public int LeadDays { get; set; }
        public List<SnowHindcastPoint> Points { get; set; } = new List<SnowHindcastPoint>();
    }

    public class SnowHindcastPoint
    {
        public DateOnly TargetDate { get; set; }
        public double PredictedSnowInches { get; set; }
        public bool IsEvent { get; set; }
    }
}
