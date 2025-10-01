using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("garageeventlog")]
    public class GarageEventLog
    {
        [Key]
        [Column("garageeventlogid")]
        public long GarageEventLogId { get; set; }

        [Column("garageeventtypeid")]
        public int GarageEventTypeId { get; set; }

        [ForeignKey(nameof(GarageEventTypeId))]
        public GarageEventType GarageEventType { get; set; }

        [Column("distance")]
        public decimal Distance { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }
    }
}
