using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public interface ITimePeriod : IValue
    { }

    public class DateTimeTimePeriod : ITimePeriod
    {
        DateTimeOffset _time;
        const string _pattern = @"(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})T(?<Hour>\d{2}):(?<Minute>\d{2}):(?<Second>\d{2})(?<Ticks>(?:\.\d+)?)(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?";

        public DateTimeTimePeriod(DateTimeOffset dateTime)
        {
            _time = dateTime;
        }

        public static DateTimeTimePeriod Parse(string input)
        {
            return new DateTimeTimePeriod(DateTimeOffset.Parse(input));
        }

        public static explicit operator DateTimeOffset(DateTimeTimePeriod input)
        {
            Contract.AssertNotNull(input, "input");
            return input._time;
        }

        public static explicit operator DateTimeTimePeriod(DateTimeOffset input)
        {            
            return new DateTimeTimePeriod(input);
        }

        public override string ToString()
        {
            return _time.ToString("yyyy-MM-ddThh:mm:ss.FFFFFFFK");
        }

        public static bool IsMatch(string value)
        {
            return Regex.IsMatch(value, _pattern);
        }

        internal static bool TryParse(string stringValue, out IValue value, out string reason)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimePeriod : ITimePeriod
    {
        DateTimeOffset _timeOffset;
        const string _pattern = @"(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?";

        public DateTimePeriod(DateTimeOffset dateTime)
        {
            _timeOffset = new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Offset);
        }
        public override string ToString()
        {
            if (_timeOffset.Offset.Ticks == 0)
                return _timeOffset.ToString("yyyy-MM-dd");
            else
                return _timeOffset.ToString("yyyy-MM-ddK");
        }

        public static DateTimePeriod Parse(string input)
        {
            var match = Regex.Match(input, _pattern);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", input);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            int day = int.Parse(match.Groups["Day"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            return new DateTimePeriod(new DateTimeOffset(year, month, day, 0, 0, 0, 0, offset));
        }

        public static explicit operator DateTimeOffset(DateTimePeriod input)
        {
            Contract.AssertNotNull(input, "input");
            return input._timeOffset;
        }

        public static explicit operator DateTimePeriod(DateTimeOffset input)
        {
            return new DateTimePeriod(input);
        }

        public static bool IsMatch(string value)
        {
            return Regex.IsMatch(value, _pattern);
        }

        internal static bool TryParse(string stringValue, out IValue value, out string reason)
        {
            throw new NotImplementedException();
        }
    }

    public class YearMonthTimePeriod : ITimePeriod
    {     
        const string _pattern = @"(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?";
        DateTimeOffset _timeOffset;

        public int Year
        {
            get { return _timeOffset.Year; }
        }

        public int Month
        {
            get { return _timeOffset.Month; }
        }

        public YearMonthTimePeriod(DateTimeOffset dateTime)
        {
            _timeOffset = dateTime;
        }

        public YearMonthTimePeriod(int year, int month)
        {
            // use date time to validate the integers
            _timeOffset = new DateTimeOffset(year, month, 1, 0, 0, 0, new TimeSpan());
        }

        public override string ToString()
        {
            if (_timeOffset.Offset.Ticks == 0)
                return _timeOffset.ToString("yyyy-MM");
            else
                return _timeOffset.ToString("yyyy-MMK");
        }

        public static YearMonthTimePeriod Parse(string input)
        {
            var match = Regex.Match(input, _pattern);
            if (!match.Success)
            {
                throw new SDMXException("Invalid year month value '{0}'.", input);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);            
            return new YearMonthTimePeriod(new DateTimeOffset(year, month, 1, 0, 0, 0, offset));
        }

        public static explicit operator DateTimeOffset(YearMonthTimePeriod input)
        {
            Contract.AssertNotNull(input, "input");
            return input._timeOffset;
        }

        public static explicit operator YearMonthTimePeriod(DateTimeOffset input)
        {
            return new YearMonthTimePeriod(input);
        }

        public static bool IsMatch(string value)
        {
            return Regex.IsMatch(value, _pattern);
        }

        internal static bool TryParse(string stringValue, out IValue value, out string reason)
        {
            throw new NotImplementedException();
        }
    }

    public class YearTimePeriod : ITimePeriod, IEquatable<YearTimePeriod>
    {        
        const string _pattern = @"(?<Sign>[-|+]?)(?<Year>\d{4})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?";
        DateTimeOffset _timeOffset;

        public int Year
        {
            get { return _timeOffset.Year; }
        }

        public YearTimePeriod(DateTimeOffset dateTime)
        {
            _timeOffset = dateTime;
        }

        public YearTimePeriod(int year)
        {
            _timeOffset = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan());
        }

        public override string ToString()
        {
            if (_timeOffset.Offset.Ticks == 0)
                return _timeOffset.ToString("yyyy");
            else
                return _timeOffset.ToString("yyyyK");
        }

        public static YearTimePeriod Parse(string input)
        {
            var match = Regex.Match(input, _pattern);
            if (!match.Success)
            {
                throw new SDMXException("Invalid year value '{0}'.", input);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            return new YearTimePeriod(new DateTimeOffset(year, 1, 1, 0, 0, 0, offset));
        }

        public static explicit operator DateTimeOffset(YearTimePeriod input)
        {
            Contract.AssertNotNull(input, "input");
            return input._timeOffset;
        }

        public static explicit operator YearTimePeriod(DateTimeOffset input)
        {
            return new YearTimePeriod(input);
        }

        public static explicit operator YearTimePeriod(int input)
        {
            return new YearTimePeriod(input);
        }

        public static bool IsMatch(string value)
        {
            return Regex.IsMatch(value, _pattern);
        }

        public static bool TryParse(string s, out IValue value, out string reason)
        {
            reason = null;
            value = null;
            int result;
            if (!int.TryParse(s, out result))
            {
                reason = "Could not parse string to YearTimePeriod: '{0}'.".F(s);
                return false;
            }
            value = new YearTimePeriod(result);
            return true;
        }

        #region IEquatable<DecimalValue> Members

        public override int GetHashCode()
        {
            return _timeOffset.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is YearTimePeriod)) return false;
            return Equals((YearTimePeriod)other);
        }

        public bool Equals(YearTimePeriod other)
        {
            return _timeOffset.Equals(other._timeOffset);
        }

        public static bool operator ==(IValue x, YearTimePeriod y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(IValue x, YearTimePeriod y)
        {
            return !x.Equals(y);
        }

        #endregion
    }

    public class QuarterlyTimePeriod : ITimePeriod
    {
        int _year;
        Quarterly _quarter;

        public int Year
        {
            get { return _year; }
        }

        public QuarterlyTimePeriod(int year, Quarterly quarter)
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

    public class WeeklyTimePeriod : ITimePeriod
    {
        int _year;
        Weekly _week;

        public int Year
        {
            get { return _year; }
        }

        public WeeklyTimePeriod(int year, Weekly week)
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

    public class BiannualTimePeriod : ITimePeriod
    {
        int _year;
        Biannual _annum;

        public int Year
        {
            get { return _year; }
        }

        public BiannualTimePeriod(int year, Biannual annum)
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

    public class TriannualTimePeriod : ITimePeriod
    {
        int _year;
        Triannual _annum;

        public int Year
        {
            get { return _year; }
        }

        public TriannualTimePeriod(int year, Triannual annum)
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

    internal static class TimePeriodUtility
    {
        public static TimeSpan ParseTimeOffset(Match match)
        {
            var offset = new TimeSpan();
            if (match.Groups["ZoneSign"].Success)
            {
                int hours = int.Parse(match.Groups["ZoneHour"].Value);
                int minutes = int.Parse(match.Groups["ZoneMinute"].Value);

                if (match.Groups["ZoneSign"].Value == "-")
                {
                    hours *= -1;
                }
                offset = new TimeSpan(hours, minutes, 0);
            }

            return offset;
        }
    }

    public enum Quarterly
    {
        Q1 = 1,
        Q2,
        Q3,
        Q4
    }

    public enum Weekly
    {
        W1 = 1,
        W2,
        W3,
        W4,
        W5,
        W6,
        W7,
        W8,
        W9,
        W10,
        W11,
        W12,
        W13,
        W14,
        W15,
        W16,
        W17,
        W18,
        W19,
        W20,
        W21,
        W22,
        W23,
        W24,
        W25,
        W26,
        W27,
        W28,
        W29,
        W30,
        W31,
        W32,
        W33,
        W34,
        W35,
        W36,
        W37,
        W38,
        W39,
        W40,
        W41,
        W42,
        W43,
        W44,
        W45,
        W46,
        W47,
        W48,
        W49,
        W50,
        W51,
        W52
    }

    public enum Triannual
    {
        T1 = 1,
        T2,
        T3
    }

    public enum Biannual
    {
        B1 = 1,
        B2
    }


}