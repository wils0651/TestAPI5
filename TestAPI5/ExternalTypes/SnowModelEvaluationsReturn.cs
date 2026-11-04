using System;
using System.Collections.Generic;

namespace TestAPI5.ExternalTypes
{
    public class SnowModelEvaluationsReturn
    {
        // Oldest first.
        public List<SnowModelEvaluation> Evaluations { get; set; } = new List<SnowModelEvaluation>();
    }

    public class SnowModelEvaluation
    {
        public string ModelVersion { get; set; }
        public DateTime EvaluatedAt { get; set; }
        // Evaluations are only comparable when this matches (e.g. "2007,2011,2020").
        public string HoldoutSeasons { get; set; }
        public int NDays { get; set; }
        public string Notes { get; set; }
        // Keyed by metric name as stored (e.g. "auc_lift_lead_1_3", "mae_skill", "useful_horizon_days").
        public Dictionary<string, SnowModelMetric> Metrics { get; set; } = new Dictionary<string, SnowModelMetric>();
    }

    public class SnowModelMetric
    {
        public double? Value { get; set; }
        public double? CiLow { get; set; }
        public double? CiHigh { get; set; }
    }
}
