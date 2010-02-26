using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class BiannualValue : TimePeriod
    {
        int _year;
        Biannual _annum;

        public int Year
        {
            get { return _year; }
        }

        public BiannualValue(int year, Biannual annum)
        {
            // use date time to validate the integer
            var dateTime = new DateTime(year, 1, 1);
            _year = dateTime.Year;
            _annum = annum;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", _year, _annum);
        }
    }
}
