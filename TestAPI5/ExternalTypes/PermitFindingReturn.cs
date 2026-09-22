using System;

namespace TestAPI5.ExternalTypes
{
    public class PermitFindingReturn
    {
        public long FindingId { get; set; }

        public int PermitId { get; set; }

        public string DisplayName { get; set; }

        public int DivisionId { get; set; }

        public DateOnly LaunchDate { get; set; }

        public int Remaining { get; set; }

        public int Total { get; set; }

        public DateTime FoundAt { get; set; }

        public DateTime? NotifiedAt { get; set; }

        public bool IsRecent { get; set; }
    }
}
