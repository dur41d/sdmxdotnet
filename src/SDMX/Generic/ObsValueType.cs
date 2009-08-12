using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Generic
{
    /// <summary>
    /// ObsValueType describes the information set for an observation value. 
    /// This is associated with the primary measure concept declared in the 
    /// key family. The startTime attribute is only used if the textFormat 
    /// of the observation is of the Timespan type in the key family 
    /// (in which case the value field takes a duration).
    /// </summary>
    public class ObsValueType
    {
        private double _value;
        private string _starttime;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string StartTime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }



    }
}
