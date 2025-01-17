using System;

namespace TestAPI5.ExternalTypes
{
    public class MessageReturn
    {
        public long MessageId { get; set; }

        public int ComputerId { get; set; }

        public string ComputerName { get; set; }

        public string ComputerDescription { get; set; }

        public int ComputerTaskId { get; set; }

        public string ComputerTaskName { get; set; }

        public string ComputerTaskDescription { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Note { get; set; }
    }
}
