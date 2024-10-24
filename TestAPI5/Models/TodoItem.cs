using System;
using System.ComponentModel.DataAnnotations;

namespace TestAPI5.Models
{
    public class TodoItem
    {
        public Int64 Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}