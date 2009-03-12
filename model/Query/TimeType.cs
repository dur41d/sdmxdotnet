using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Query
{
    public class TimeType
    {
        private SDMX_Common.TimePeriodType _starttime;
        private SDMX_Common.TimePeriodType _endtime;
        private SDMX_Common.TimePeriodType _time;

        public SDMX_Common.TimePeriodType StartTime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }

        public SDMX_Common.TimePeriodType EndTime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }

        public SDMX_Common.TimePeriodType Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
