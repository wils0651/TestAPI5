using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    // One row per permit -- Phase 8 deliberately doesn't support multiple windows per permit, so
    // permit_id is the primary key rather than a surrogate id. Duplicated from PermitWatcher's own
    // Models/WatchWindow.cs; DateOnly columns have no Npgsql timestamp trap, unlike DateTime ones.
    [Table("watch_windows")]
    public class WatchWindow
    {
        [Key]
        [Column("permit_id")]
        public int PermitId { get; set; }

        [Column("start_date")]
        public DateOnly StartDate { get; set; }

        [Column("end_date")]
        public DateOnly EndDate { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}
