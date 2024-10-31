using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("todoitem")]
    public class TodoItem
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("iscomplete")]
        public bool IsComplete { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }
    }
}