using System;

namespace TestAPI5.ExternalTypes
{
    public class TemperatureStatisticReturn
    {
        public long TemperatureStatisticId { get; set; }

        public int ProbeId { get; set; }

        public string ProbeDescription { get; set; }

        public int DataCount { get; set; }

        public decimal Mean { get; set; }

        public decimal StandardDeviation { get; set; }

        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; }

        public DateTime MeasurementDate { get; set; }
    }
}
