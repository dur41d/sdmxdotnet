using System;
using Common;

namespace SDMX
{
    public enum TimePeriodType
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
        public TimePeriodType Type { get; private set; }

        DateTimeOffset _value;

        Weekly _weekly;
        Quarterly _quarterly;
        Biannual _biannaul;
        Triannual _triannual;

        public bool IsWeekly { get { return Type == TimePeriodType.Weekly; } }
        public bool IsQuarterly { get { return Type == TimePeriodType.Quarterly; } }
        public bool IsBiannual { get { return Type == TimePeriodType.Biannual; } }
        public bool IsTriannual { get { return Type == TimePeriodType.Triannual; } }
        public bool IsDate { get { return Type == TimePeriodType.Date; } }
        public bool IsDateTime { get { return Type == TimePeriodType.DateTime; } }
        public bool IsYear { get { return Type == TimePeriodType.Year; } }
        public bool IsYearMonth { get { return Type == TimePeriodType.YearMonth; } }

        public int Year 
        { 
            get 
            {
                switch (Type)
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
                if (Type == TimePeriodType.YearMonth
                    || Type == TimePeriodType.Date
                    || Type == TimePeriodType.DateTime)
                {
                    return _value.Month;
                }
                else
                {
                    throw new SDMXException("Month property is only valid for TimePeriodTypes YearMonth, Date, Datetime. The current type is: {0}.", Type);
                }
            } 
        }

        public int Day
        {
            get
            {
                if (Type == TimePeriodType.Date
                    || Type == TimePeriodType.DateTime)
                {
                    return _value.Day;
                }
                else
                {
                    throw new SDMXException("Day property is only valid for TimePeriodTypes Date and Datetime. The current type is: {0}.", Type);
                }
            }
        }

        public int Hour
        {
            get
            {
                if (Type == TimePeriodType.DateTime)
                {
                    return _value.Hour;
                }
                else
                {
                    throw new SDMXException("Hour property is only valid for TimePeriodType.Datetime. The current type is: {0}.", Type);
                }
            }
        }

        public int Minute
        {
            get
            {
                if (Type == TimePeriodType.DateTime)
                {
                    return _value.Minute;
                }
                else
                {
                    throw new SDMXException("Minute property is only valid for TimePeriodType.Datetime. The current type is: {0}.", Type);
                }
            }
        }

        public int Second
        {
            get
            {
                if (Type == TimePeriodType.DateTime)
                {
                    return _value.Second;
                }
                else
                {
                    throw new SDMXException("Second property is only valid for TimePeriodType.Datetime. The current type is: {0}.", Type);
                }
            }
        }

        public int Millisecond
        {
            get
            {
                if (Type == TimePeriodType.DateTime)
                {
                    return _value.Millisecond;
                }
                else
                {
                    throw new SDMXException("Millisecond property is only valid for TimePeriodType.Datetime. The current type is: {0}.", Type);
                }
            }
        }

        public TimeSpan Offset
        {
            get
            {
                if (Type == TimePeriodType.Year
                    || Type == TimePeriodType.YearMonth
                    || Type == TimePeriodType.Date
                    || Type == TimePeriodType.DateTime)                
                {
                    return _value.Offset;
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }
        }

        public Quarter Quarter
        {
            get
            {
                if (Type == TimePeriodType.Quarterly)
                {
                    return _quarterly.Quarter;
                }
                else
                {
                    throw new SDMXException("Quarter property is only valid for TimePeriodType.Quarterly. The current type is: {0}.", Type);
                }
            }
        }

        public Week Week
        {
            get
            {
                if (Type == TimePeriodType.Weekly)
                {
                    return _weekly.Week;
                }
                else
                {
                    throw new SDMXException("Week property is only valid for TimePeriodType.Weekly. The current type is: {0}.", Type);
                }
            }
        }

        public Biannum Biannum
        {
            get
            {
                if (Type == TimePeriodType.Biannual)
                {
                    return _biannaul.Annum;
                }
                else
                {
                    throw new SDMXException("Biannum property is only valid for TimePeriodType.Biannual. The current type is: {0}.", Type);
                }
            }
        }

        public Triannum Triannum
        {
            get
            {
                if (Type == TimePeriodType.Triannual)
                {
                    return _triannual.Annum;
                }
                else
                {
                    throw new SDMXException("Triannum property is only valid for TimePeriodType.Triannual. The current type is: {0}.", Type);
                }
            }
        }

        public DateTimeOffset DateTimeOffset
        {
            get
            {
                if (Type == TimePeriodType.Date
                   || Type == TimePeriodType.DateTime)
                {
                    return _value;
                }
                else
                {
                    throw new SDMXException("DateTimeOffset property is only valid for TimePeriodTypes Date and Datetime. The current type is: {0}.", Type);
                }
            }
        }

        public DateTime DateTime
        {
            get
            {
                if (Type == TimePeriodType.Date
                   || Type == TimePeriodType.DateTime)
                {
                    return _value.DateTime;
                }
                else
                {
                    throw new SDMXException("DateTime property is only valid for TimePeriodTypes Date and Datetime. The current type is: {0}.", Type);
                }
            }
        }

        private TimePeriod()
        {
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return ToStringInternal(() => _value.ToString(formatProvider));
        }

        public string ToString(string format)
        {
            return ToStringInternal(() => _value.ToString(format));
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToStringInternal(() => _value.ToString(format, formatProvider));
        }

        public override string ToString()
        {
            return ToStringInternal(() => _value.ToString());
        }

        string ToStringInternal(Func<string> valueToString)
        { 
            if (Type == TimePeriodType.Weekly)
                return _weekly.ToString();
            if (Type == TimePeriodType.Quarterly)
                return _quarterly.ToString();
            if (Type == TimePeriodType.Biannual)
                return _biannaul.ToString();
            if (Type == TimePeriodType.Triannual)
                return _triannual.ToString();
            if (Type == TimePeriodType.Year)
                return _value.Offset.Ticks > 0 ? _value.ToString("yyyyKK") : _value.ToString("yyyy");
            if (Type == TimePeriodType.YearMonth)
                return _value.Offset.Ticks > 0 ? _value.ToString("yyyy-MMKK") : _value.ToString("yyyy-MM");
            else 
                return valueToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TimePeriod);
        }

        public bool Equals(TimePeriod other)
        {
            return this.Equals(other, () => 
                Type == other.Type
                && _value == other._value
                && _quarterly == other._quarterly
                && _weekly == other._weekly
                && _biannaul == other._biannaul
                && _triannual == other._triannual);
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
            timePeriod.Type = TimePeriodType.Year;
            return timePeriod;
        }

        public static TimePeriod FromYearMonth(DateTimeOffset value)
        {
            var timePeriod = new TimePeriod();
            timePeriod._value = new DateTimeOffset(value.Year, value.Month, 1, 0, 0, 0, 0, value.Offset);
            timePeriod.Type = TimePeriodType.YearMonth;
            return timePeriod;
        }

        public static TimePeriod FromDate(DateTimeOffset value)
        {
            var timePeriod = new TimePeriod();
            timePeriod._value = new DateTimeOffset(value.Year, value.Month, value.Day, 0, 0, 0, 0, value.Offset);
            timePeriod.Type = TimePeriodType.Date;
            return timePeriod;
        }

        public static TimePeriod FromDateTime(DateTimeOffset value)
        {
            var timePeriod = new TimePeriod();
            timePeriod._value = value;
            timePeriod.Type = TimePeriodType.DateTime;
            return timePeriod;
        }

        public static TimePeriod FromWeekly(Weekly weekly)
        {
            var timePeriod = new TimePeriod();
            timePeriod._weekly = weekly;
            timePeriod.Type = TimePeriodType.Weekly;
            return timePeriod;
        }

        public static TimePeriod FromQuarterly(Quarterly quarterly)
        {
            var timePeriod = new TimePeriod();
            timePeriod._quarterly = quarterly;
            timePeriod.Type = TimePeriodType.Quarterly;
            return timePeriod;
        }

        public static TimePeriod FromBiannual(Biannual biannual)
        {
            var timePeriod = new TimePeriod();
            timePeriod._biannaul = biannual;
            timePeriod.Type = TimePeriodType.Biannual;
            return timePeriod;
        }

        public static TimePeriod FromTriannual(Triannual triannaul)
        {
            var timePeriod = new TimePeriod();
            timePeriod._triannual = triannaul;
            timePeriod.Type = TimePeriodType.Triannual;
            return timePeriod;
        }
    }

}