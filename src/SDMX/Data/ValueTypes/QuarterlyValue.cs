using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class QuarterlyValue : TimePeriod
    {
        int _year;
        Quarterly _quarter;

        public int Year
        {
            get { return _year; }
        }

        public QuarterlyValue(int year, Quarterly quarter)
        {
            // use date time to validate the integer
            var dateTime = new DateTime(year, 1, 1);
            _year = dateTime.Year;
            _quarter = quarter;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", _year, _quarter);
        }
    }
}
