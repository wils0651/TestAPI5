using System;

namespace TestAPI5.ExternalTypes
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
