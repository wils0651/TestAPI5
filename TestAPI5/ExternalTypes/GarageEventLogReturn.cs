using System;

namespace TestAPI5.ExternalTypes
{
    public class GarageEventLogReturn
    {
        public long GarageEventLogId { get; set; }

        public int GarageEventTypeId { get; set; }

        public string GarageEventTypeName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
