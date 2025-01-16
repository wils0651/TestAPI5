using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("message")]
    public class Message
    {
        [Key]
        [Column("messageid")]
        public long MessageId { get; set; }

        [Column("computerid")]
        public int ComputerId { get; set; }

        [ForeignKey(nameof(ComputerId))]
        public Computer Computer { get; set; }

        [Column("computertaskid")]
        public int ComputerTaskId { get; set; }

        [ForeignKey(nameof(ComputerTaskId))]
        public ComputerTask ComputerTask { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }

        [Column("note")]
        [MaxLength(255)]
        public string Note { get; set; }
    }
}
