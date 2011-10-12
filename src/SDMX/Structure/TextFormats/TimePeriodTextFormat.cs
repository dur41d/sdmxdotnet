using System;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class TimePeriodTextFormat : TimePeriodTextFormatBase
    {
        static IValueConverter _converter = new TimePeriodValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }


        public override bool IsValid(object obj)
        {
            return obj is TimePeriod;

        }

        //public override object Parse(string s, string startTime)
        //{
        //    var value = _converter.Parse(s, startTime);

        //    if (value is DateTimeValue) return (DateTimeOffset)(DateTimeValue)value;
        //    else if (value is DateValue) return (DateTimeOffset)(DateValue)value;
        //    else if (value is YearMonthValue) return (DateTimeOffset)(YearMonthValue)value;
        //    else if (value is YearValue) return (DateTimeOffset)(YearValue)value;
        //    else if (value is WeeklyValue
        //        || value is QuarterlyValue
        //        || value is BiannualValue
        //        || value is TriannualValue)
        //    {
        //        return value.ToString();
        //    }
        //    else
        //    {
        //        throw new SDMXException("Cannot parse string: {0} star.", s);
        //    }

        //}

        //public override string Serialize(object obj, out string startTime)
        //{
        //    Contract.AssertNotNull(obj, "obj");

        //    if (obj is DateTime?)
        //        obj = ((DateTime?)obj).Value;
            
        //    if (obj is DateTime)
        //    { 
        //        var dt = (DateTime)obj;
        //        obj = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, TimeSpan.FromTicks(0));
        //    }

        //    if (obj is DateTimeOffset?)
        //        obj = ((DateTimeOffset?)obj).Value;
            
        //    if (obj is DateTimeOffset)
        //    {
        //        var dt = (DateTimeOffset)obj;

        //        if (dt.Hour > 0)
        //            obj = new DateTimeValue(dt);
        //        else
        //            obj = new DateValue(dt);
        //    } 

        //    return _converter.Serialize(obj, out startTime);
        //}
    }
}
