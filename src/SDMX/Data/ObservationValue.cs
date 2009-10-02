using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public struct ObservationValue
    {
        private double doubleValue;
        private TimeSpan duration;
        public TextType Type { get; private set; }

        public ObservationValue(double value)
            : this()
        {
            this.Type = TextType.Double; 
            this.doubleValue = value; 
        }

        public ObservationValue(TimeSpan duration)
            : this()
        {
            this.Type = TextType.Timespan;
            this.duration = duration;
        }
    }
  
}
