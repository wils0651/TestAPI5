using System;
using System.Collections.Generic;

namespace TestAPI5.ExternalTypes
{
    public class PermitWatchReturn
    {
        public int PermitId { get; set; }

        public string DisplayName { get; set; }

        public int DivisionId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool IsActive { get; set; }

        public List<WatchDateExceptionReturn> Exceptions { get; set; }
    }
}
