using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    //public class TimePeriodValue : TimePeriod, IEquatable<TimePeriodValue>
    //{
    //    TimePeriod _value;

    //    public TimePeriodValue(TimePeriod value)
    //    {
    //        Contract.AssertNotNull(value, "value");
    //        _value = value;
    //    }

    //    public TimePeriodValue(DateValue value)
    //        : this((TimePeriod)value)
    //    {
    //    }

    //    public TimePeriodValue(YearMonthValue value)
    //        : this((TimePeriod)value)
    //    {
    //    }

    //    public TimePeriodValue(YearValue value)
    //        : this((TimePeriod)value)
    //    {
    //    }

    //    public TimePeriodValue(QuarterlyValue value)
    //        : this((TimePeriod)value)
    //    {
    //    }

    //    public TimePeriodValue(WeeklyValue value)
    //        : this((TimePeriod)value)
    //    {
    //    }

    //    public TimePeriodValue(BiannualValue value)
    //        : this((TimePeriod)value)
    //    {
    //    }

    //    public TimePeriodValue(TriannualValue value)
    //        : this((TimePeriod)value)
    //    {
    //    }

    //    public override string ToString()
    //    {
    //        return _value.ToString();
    //    }

    //    #region IEquatable<TimeValue> Members

    //    public override bool Equals(object obj)
    //    {
    //        return _value.Equals(obj);
    //    }

    //    public bool Equals(TimePeriodValue other)
    //    {
    //        return _value.Equals(other);
    //    }

    //    public override int GetHashCode()
    //    {
    //        return _value.GetHashCode();
    //    }

    //    #endregion
    //}
}
