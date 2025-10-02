using System;

namespace TestAPI5.ExternalTypes
{
    public class ProbeDataReturn
    {
        public long ProbeDataId { get; set; }

        public int ProbeId { get; set; }

        public string ProbeName { get; set; }

        public string ProbeDescription { get; set; }

        public decimal Temperature { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public bool IsStale { get; set; }
    }
}
