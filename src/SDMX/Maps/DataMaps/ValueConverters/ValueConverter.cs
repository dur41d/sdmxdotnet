using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    //internal class ValueConverter
    //{
    //    static Dictionary<Type, IValueConverter> registry = new Dictionary<Type, IValueConverter>();

    //    static ValueConverter()
    //    {
    //        registry.Add(typeof(string), new StringValueConverter());
    //        registry.Add(typeof(decimal), new DecimalValueConverter());
    //        registry.Add(typeof(DateTimeValue), new DateTimeValueConverter());
    //        registry.Add(typeof(DateValue), new DateValueConverter());
    //        registry.Add(typeof(YearValue), new YearValueConverter());
    //        registry.Add(typeof(YearMonthValue), new YearMonthValueConverter());
    //        registry.Add(typeof(TimePeriod), new TimePeriodValueConverter());
    //    }

    //    public object Parse(Component component, string s, string startTime)
    //    {
    //        Type valueType = component.GetValueType();
    //        var converter = registry[valueType];
    //        return converter.Parse(s, startTime);
    //    }

    //    public string Serialize(Value value, out string startTime)
    //    {
    //        var converter = registry[value.GetType()];
    //        return converter.Serialize(value, out startTime);
    //    }
    //}

}
