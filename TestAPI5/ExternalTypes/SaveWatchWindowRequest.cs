using System;

namespace TestAPI5.ExternalTypes
{
    public class SaveWatchWindowRequest
    {
        public int PermitId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
