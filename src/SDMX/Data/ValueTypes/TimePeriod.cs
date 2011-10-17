using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;
using OXM;

namespace SDMX
{
    enum TimePeriodType
    { 
        Year,
        YearMonth,
        Date,
        DateTime,
        Weekly,
        Quarterly,
        Biannual,
        Triannual
    }

    public class TimePeriod : IEquatable<TimePeriod>
    {
        internal TimePeriodType TimePeriodType { get; private set; }

        DateTimeOffset _value;

        Weekly _weekly;
        Quarterly _quarterly;
        Biannual _biannaul;
        Triannual _triannual;

        public int Year 
        { 
            get 
            {
                switch (TimePeriodType)
                {
                    case TimePeriodType.Weekly: return _weekly.Year;
                    case TimePeriodType.Quarterly: return _quarterly.Year;
                    case TimePeriodType.Biannual: return _biannaul.Year;
                    case TimePeriodType.Triannual: return _triannual.Year;
                    case TimePeriodType.Date: return _value.Year;
                    case TimePeriodType.DateTime: return _value.Year;
                    case TimePeriodType.Year: return _value.Year;
                    case TimePeriodType.YearMonth: return _value.Year;
                    default: throw new InvalidOperationException();
                }
            } 
        }

        public int Month 
        { 
            get 
            {
                if (TimePeriodType == TimePeriodType.YearMonth
                    || TimePeriodType == TimePeriodType.Date
                    || TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value.Month;
                }
                else
                {
                    return 0;
                }
            } 
        }

        public int Day
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Date
                    || TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value.Day;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Hour
        {
            get
            {
                if (TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value.Hour;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Minute
        {
            get
            {
                if (TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value.Minute;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Second
        {
            get
            {
                if (TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value.Second;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Millisecond
        {
            get
            {
                if (TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value.Millisecond;
                }
                else
                {
                    return 0;
                }
            }
        }

        public TimeSpan Offset
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Year
                    || TimePeriodType == TimePeriodType.YearMonth
                    || TimePeriodType == TimePeriodType.Date
                    || TimePeriodType == TimePeriodType.DateTime)                
                {
                    return _value.Offset;
                }
                else
                {
                    return TimeSpan.FromTicks(0);
                }
            }
        }

        public Quarter Quarter
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Quarterly)
                {
                    return _quarterly.Quarter;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Week Week
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Weekly)
                {
                    return _weekly.Week;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Biannum Biannum
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Biannual)
                {
                    return _biannaul.Annum;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Triannum Triannum
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Triannual)
                {
                    return _triannual.Annum;
                }
                else
                {
                    return 0;
                }
            }
        }

        public DateTimeOffset DateTimeOffset
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Year
                   || TimePeriodType == TimePeriodType.YearMonth
                   || TimePeriodType == TimePeriodType.Date
                   || TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value;
                }
                else
                {
                    throw new SDMXException("Cannot access DateTimeOffset for TimePeriod of type: {0}.", TimePeriodType);
                }
            }
        }

        public DateTime DateTime
        {
            get
            {
                if (TimePeriodType == TimePeriodType.Year
                   || TimePeriodType == TimePeriodType.YearMonth
                   || TimePeriodType == TimePeriodType.Date
                   || TimePeriodType == TimePeriodType.DateTime)
                {
                    return _value.DateTime;
                }
                else
                {
                    throw new SDMXException("Cannot access DateTime for TimePeriod of type: {0}.", TimePeriodType);
                }
            }
        }

        private TimePeriod()
        {
        }

        public override string ToString()
        {
            if (TimePeriodType == TimePeriodType.Weekly)
                return _weekly.ToString();
            if (TimePeriodType == TimePeriodType.Quarterly)
                return _quarterly.ToString();
            if (TimePeriodType == TimePeriodType.Biannual)
                return _biannaul.ToString();
            if (TimePeriodType == TimePeriodType.Triannual)
                return _triannual.ToString();
            else
                return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TimePeriod);
        }

        public bool Equals(TimePeriod other)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(TimePeriod x, TimePeriod y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(TimePeriod x, TimePeriod y)
        {
            return !(x == y);
        }

        public static TimePeriod FromYear(DateTimeOffset value)
        {
            var timePeriod = new TimePeriod();
            timePeriod._value = new DateTimeOffset(value.Year, 1, 1, 0, 0, 0, 0, value.Offset);
            timePeriod.TimePeriodType = TimePeriodType.Year;
            return timePeriod;
        }

        public static TimePeriod FromYearMonth(DateTimeOffset value)
        {
            var timePeriod = new TimePeriod();
            timePeriod._value = new DateTimeOffset(value.Year, value.Month, 1, 0, 0, 0, 0, value.Offset);
            timePeriod.TimePeriodType = TimePeriodType.YearMonth;
            return timePeriod;
        }

        public static TimePeriod FromDate(DateTimeOffset value)
        {
            var timePeriod = new TimePeriod();
            timePeriod._value = new DateTimeOffset(value.Year, value.Month, value.Day, 0, 0, 0, 0, value.Offset);
            timePeriod.TimePeriodType = TimePeriodType.Date;
            return timePeriod;
        }

        public static TimePeriod FromDateTime(DateTimeOffset value)
        {
            var timePeriod = new TimePeriod();
            timePeriod._value = value;
            timePeriod.TimePeriodType = TimePeriodType.DateTime;
            return timePeriod;
        }

        public static TimePeriod FromWeekly(Weekly weekly)
        {
            var timePeriod = new TimePeriod();
            timePeriod._weekly = weekly;
            timePeriod.TimePeriodType = TimePeriodType.Weekly;
            return timePeriod;
        }

        public static TimePeriod FromQuarterly(Quarterly quarterly)
        {
            var timePeriod = new TimePeriod();
            timePeriod._quarterly = quarterly;
            timePeriod.TimePeriodType = TimePeriodType.Quarterly;
            return timePeriod;
        }

        public static TimePeriod FromBiannual(Biannual biannual)
        {
            var timePeriod = new TimePeriod();
            timePeriod._biannaul = biannual;
            timePeriod.TimePeriodType = TimePeriodType.Biannual;
            return timePeriod;
        }

        public static TimePeriod FromTriannual(Triannual triannaul)
        {
            var timePeriod = new TimePeriod();
            timePeriod._triannual = triannaul;
            timePeriod.TimePeriodType = TimePeriodType.Triannual;
            return timePeriod;
        }
    }

}