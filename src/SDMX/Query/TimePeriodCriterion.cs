using System.Collections.Generic;

namespace SDMX
{
    public class TimePeriodCriterion : ITimeCriterion
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
