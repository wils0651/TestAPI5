using System.Collections.Generic;

namespace TestAPI5.ExternalTypes
{
    public class ComputerDetailReturn
    {
        public int ComputerId { get; set; }

        public string ComputerName { get; set; }

        public string ComputerDescription { get; set; }

        public string IpAddress { get; set; }

        public List<ComputerTaskReturn> ComputerTasks { get; set; }
    }
}
