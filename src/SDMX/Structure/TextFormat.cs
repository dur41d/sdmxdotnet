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
        //void Serialize(IValue value, out string stringValue, out string startTime);
        //bool TryParse(string s, string startTime, out IValue value, out string reason);

        Type GetValueType();
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

        public Type GetValueType()
        {
            return typeof(StringValue);
        }
      
        //public bool TryParse(string stringValue, string startTime, out IValue value, out string reason)
        //{
        //    return StringValue.TryParse(stringValue, out value, out reason);
        //}


        //public void Serialize(IValue value, out string stringValue, out string startTime)
        //{
        //    stringValue = ((StringValue)value).ToString();
        //    startTime = null;
        //}
    }

    public class DecimalTextFormat : ITextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is DecimalValue;
        }

        public Type GetValueType()
        {
            return typeof(DecimalValue);
        }
      

        //public bool TryParse(string stringValue, string startTime, out IValue value, out string reason)
        //{
        //    return DecimalValue.TryParse(stringValue, out value, out reason);
        //}

        //public void Serialize(IValue value, out string stringValue, out string startTime)
        //{
        //    stringValue = ((DecimalValue)value).ToString();
        //    startTime = null;
        //}
    }

    public class YearTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is YearTimePeriod;
        }

        public Type GetValueType()
        {
            return typeof(YearTimePeriod);
        }
      

        //public bool TryParse(string stringValue, string startTime, out IValue value, out string reason)
        //{
        //    return YearTimePeriod.TryParse(stringValue, out value, out reason);
        //}

        //public void Serialize(IValue value, out string stringValue, out string startTime)
        //{
        //    stringValue = ((YearTimePeriod)value).ToString();
        //    startTime = null;
        //}
    }

    public class YearMonthTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is YearMonthTimePeriod;
        }

        public Type GetValueType()
        {
            return typeof(YearMonthTimePeriod);
        }

        //public bool TryParse(string stringValue, string startTime, out IValue value, out string reason)
        //{
        //    return YearMonthTimePeriod.TryParse(stringValue, out value, out reason);
        //}

        //public void Serialize(IValue value, out string stringValue, out string startTime)
        //{
        //    stringValue = ((YearMonthTimePeriod)value).ToString();
        //    startTime = null;
        //}
    }

    public class DateTimeTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is DateTimeTimePeriod;
        }

        public Type GetValueType()
        {
            return typeof(DateTimeTimePeriod);
        }

        //public bool TryParse(string stringValue, string startTime, out IValue value, out string reason)
        //{
        //    return DateTimeTimePeriod.TryParse(stringValue, out value, out reason);
        //}

        //public void Serialize(IValue value, out string stringValue, out string startTime)
        //{
        //    stringValue = ((DateTimeTimePeriod)value).ToString();
        //    startTime = null;
        //}
    }

    public class DateTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(IValue value)
        {
            return value is DateTimePeriod;
        }

        public Type GetValueType()
        {
            return typeof(DateTimePeriod);
        }

        //public bool TryParse(string stringValue, string startTime, out IValue value, out string reason)
        //{
        //    return DateTimePeriod.TryParse(stringValue, out value, out reason);
        //}

        //public void Serialize(IValue value, out string stringValue, out string startTime)
        //{
        //    stringValue = ((DateTimePeriod)value).ToString();
        //    startTime = null;
        //}
    }
}
