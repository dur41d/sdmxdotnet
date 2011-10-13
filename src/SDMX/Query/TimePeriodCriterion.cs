using System.Collections.Generic;

namespace SDMX
{
    public class TimePeriodCriterion : ITimeCriterion
    {
        public TimePeriod StartTime { get; set; }
        public TimePeriod EndTime { get; set; }
    }
}
