using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("todoitem")]
    public class TodoItem
    {
        public Int64 Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}