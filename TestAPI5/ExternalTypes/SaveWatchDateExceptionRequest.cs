using System;

namespace TestAPI5.ExternalTypes
{
    public class SaveWatchDateExceptionRequest
    {
        public int PermitId { get; set; }
        public DateOnly ExceptionDate { get; set; }
        public bool IsIncluded { get; set; }
    }
}
