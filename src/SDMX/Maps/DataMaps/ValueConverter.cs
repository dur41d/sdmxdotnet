using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class ValueConverter
    {
        private Dictionary<Type, IValueConverter> registry = new Dictionary<Type, IValueConverter>();

        public ValueConverter()
        {
            registry.Add(typeof(Code), new CodeValueConverter());
            registry.Add(typeof(StringValue), new StringValueConverter());
            registry.Add(typeof(DecimalValue), new DecimalValueConverter());
            registry.Add(typeof(YearTimePeriod), new YearValueConverter());
            registry.Add(typeof(YearMonthTimePeriod), new YearMonthValueConverter());
        }

        public IValue Parse(Component component, string s, string startTime)
        {
            Type valueType = component.GetValueType();
            var converter = registry[valueType];
            return converter.Parse(component, s, startTime);
        }

        public string Serialize(IValue value, out string startTime)
        {
            var converter = registry[value.GetType()];
            return converter.Serialize(value, out startTime);
        }
    }

    internal interface IValueConverter
    {
        IValue Parse(Component component, string s, string startTime);
        string Serialize(IValue value, out string startTime);
    }

    internal class CodeValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            var code = component.CodeList.Get(s);
            if (code == null)
            {
                throw new SDMXException("Invalid value. '{0}' does not exist in the code list '{1}' for compoenent '{2}'",
                    s, component.CodeList, component);
            }
            return code;
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            return ((Code)value).ID.ToString();
        }
    }

    internal class StringValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            return new StringValue(s);
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            return ((StringValue)value).ToString();
        }
    }

    internal class DecimalValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            return new DecimalValue(decimal.Parse(s));
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            return ((DecimalValue)value).ToString();
        }
    }

    internal class YearValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            return new YearTimePeriod(int.Parse(s));
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            DateTimeOffset time = (DateTimeOffset)(YearTimePeriod)value;
            return time.Year.ToString();
        }
    }

    internal class YearMonthValueConverter : IValueConverter
    {
        static Regex pattern = new Regex(@"(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?", RegexOptions.Compiled);

        public IValue Parse(Component component, string s, string startTime)
        {
            var match = pattern.Match(s);
            if (!match.Success)
            {
                throw new SDMXException("Invalid year month value '{0}'.", s);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            return new YearMonthTimePeriod(year, month);
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            var yearMonth = (YearMonthTimePeriod)value;
            return "{0}-{1:00}".F(yearMonth.Year, yearMonth.Month);
        }
    }
}
