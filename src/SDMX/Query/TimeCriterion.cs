using System.Collections.Generic;

namespace SDMX
{
    public interface ITimeCriterion : ICriterion
    { }

    public class TimeCriterion : ITimeCriterion
    {
        public TimePeriod Time { get; set; }
    }
}
