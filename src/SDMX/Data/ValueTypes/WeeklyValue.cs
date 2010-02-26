using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class WeeklyValue : TimePeriod
    {
        int _year;
        Weekly _week;

        public int Year
        {
            get { return _year; }
        }

        public WeeklyValue(int year, Weekly week)
        {
            // use date time to validate the integer
            var dateTime = new DateTime(year, 1, 1);
            _year = dateTime.Year;
            _week = week;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", _year, _week);
        }
    }
}
