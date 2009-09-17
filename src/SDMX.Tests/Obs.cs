using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class TimePeriod
    {
        public DateTime Value { get; set; }

        public static TimePeriod Parse(string timePeriod)
        {
            var period = new TimePeriod();
            period.Value = DateTime.Parse(timePeriod);

            return period;
        }
    }

    public class Observation
    {
        public TimePeriod TimePeriod { get; set; }
        public object Value { get; set;}

        public static Observation Create(string timePeriod, string value)
        {
            var obs = new Observation();
            obs.TimePeriod = TimePeriod.Parse(timePeriod);
            obs.Value = decimal.Parse(value);

            return obs;
        }
    }
}
