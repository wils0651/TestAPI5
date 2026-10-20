using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("permit_findings")]
    public class PermitFinding
    {
        [Key]
        [Column("finding_id")]
        public long FindingId { get; set; }

        [Column("permit_id")]
        public int PermitId { get; set; }

        [ForeignKey(nameof(PermitId))]
        public Permit Permit { get; set; }

        [Column("division_id")]
        public int DivisionId { get; set; }

        [Column("launch_date")]
        public DateOnly LaunchDate { get; set; }

        [Column("remaining")]
        public int Remaining { get; set; }

        [Column("total")]
        public int Total { get; set; }

        // Explicit TypeName is required: without it Npgsql builds query parameters for these
        // columns as timestamptz and rejects a Kind=Unspecified value at runtime. The trap is
        // per-context, not per-project -- re-applied here from PermitWatcher's own
        // Models/PermitFinding.cs, which has the fuller explanation.
        [Column("found_at", TypeName = "timestamp without time zone")]
        public DateTime FoundAt { get; set; }

        [Column("notified_at", TypeName = "timestamp without time zone")]
        public DateTime? NotifiedAt { get; set; }
    }
}
