using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public interface ITextFormat
    {
        bool IsValid(IValue value);
        IValue Parse(string value, string startTime);
        void Serialize(IValue value, out string stringValue, out string startTime);
    }

    public interface ITimePeriodTextFormat : ITextFormat
    { 
    
    }

    public class StringTextFormat : ITextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is StringValue;
        }

        public IValue Parse(string value, string startTime)
        {
            return new StringValue(value);
        }

        public void Serialize(IValue value, out string stringValue, out string startTime)
        {
            stringValue = ((StringValue)value).ToString();
            startTime = null;
        }
    }

    public class DecimalTextFormat : ITextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is DecimalValue;
        }

        public IValue Parse(string value, string startTime)
        {
            return new DecimalValue(decimal.Parse(value));
        }

        public void Serialize(IValue value, out string stringValue, out string startTime)
        {
            stringValue = ((DecimalValue)value).ToString();
            startTime = null;
        }
    }

    public class YearTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is YearTimePeriod;
        }

        public IValue Parse(string value, string startTime)
        {
            return YearTimePeriod.Parse(value);
        }

        public void Serialize(IValue value, out string stringValue, out string startTime)
        {
            stringValue = ((YearTimePeriod)value).ToString();
            startTime = null;
        }
    }

    public class YearMonthTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is YearMonthTimePeriod;
        }

        public IValue Parse(string value, string startTime)
        {
            return YearMonthTimePeriod.Parse(value);
        }

        public void Serialize(IValue value, out string stringValue, out string startTime)
        {
            stringValue = ((YearMonthTimePeriod)value).ToString();
            startTime = null;
        }
    }

    public class DateTimeTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is DateTimeTimePeriod;
        }

        public IValue Parse(string value, string startTime)
        {
            return DateTimeTimePeriod.Parse(value);
        }

        public void Serialize(IValue value, out string stringValue, out string startTime)
        {
            stringValue = ((DateTimeTimePeriod)value).ToString();
            startTime = null;
        }
    }

    public class DateTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is DateTimePeriod;
        }

        public IValue Parse(string value, string startTime)
        {
            return DateTimePeriod.Parse(value);
        }

        public void Serialize(IValue value, out string stringValue, out string startTime)
        {
            stringValue = ((DateTimePeriod)value).ToString();
            startTime = null;
        }
    }
}
