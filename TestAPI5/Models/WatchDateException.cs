using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestAPI5.Models
{
    // A single is_included flag per date models both "exclude a date in-range" and "re-include a
    // date out-of-range" -- a toggle is always insert-or-update, never a delete.
    [Table("watch_date_exceptions")]
    [PrimaryKey(nameof(PermitId), nameof(ExceptionDate))]
    public class WatchDateException
    {
        [Column("permit_id")]
        public int PermitId { get; set; }

        [Column("exception_date")]
        public DateOnly ExceptionDate { get; set; }

        [Column("is_included")]
        public bool IsIncluded { get; set; }
    }
}
