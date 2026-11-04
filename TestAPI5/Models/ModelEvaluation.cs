using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    // One metric of one scoring run, written by SnowPredictor/trainer/evaluate_hindcast.py (DDL in
    // SnowPredictor/schema.sql). Read-only here; composite key configured in SnowDatabaseContext.
    [Table("model_evaluations")]
    public class ModelEvaluation
    {
        [Column("model_version")]
        public string ModelVersion { get; set; }

        [Column("evaluated_at", TypeName = "timestamp without time zone")]
        public DateTime EvaluatedAt { get; set; }

        [Column("holdout_seasons")]
        public string HoldoutSeasons { get; set; }

        [Column("metric")]
        public string Metric { get; set; }

        [Column("value")]
        public double? Value { get; set; }

        [Column("ci_low")]
        public double? CiLow { get; set; }

        [Column("ci_high")]
        public double? CiHigh { get; set; }

        [Column("n_days")]
        public int NDays { get; set; }

        [Column("notes")]
        public string Notes { get; set; }
    }
}
