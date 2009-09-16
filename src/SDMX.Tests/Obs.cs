using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class Observation
    {

        public DateTime TimePeriod { get; set; }

        public void SetValue(string valueString)
        {
            Value = decimal.Parse(valueString);
        }

        public void SetTimePeriod(string timePeriodString)
        {
            TimePeriod = DateTime.Parse(timePeriodString);
        }
        
        public object Value { get; set;}
    }
}
