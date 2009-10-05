using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class TextFormat
    {
        public TextType TextType { get; set; }
        public bool IsSequence { get; internal set; }
        public int MinLength { get; internal set; }
        public int MaxLenght { get; internal set; }
        public double StartValue { get; internal set; }
        public double EndValue { get; internal set; }
        public double Interval { get; internal set; }
        public TimeSpan TimeInterval { get; internal set; }
        public int Decimal { get; internal set; }
        public Regex Pattern { get; internal set; }
    }

    public enum TextType
    { 
        String,
        BigInteger,
        Integer,
        Long,
        Short,
        Decimal,
        Float,
        Double,
        Boolean,
        DateTime,
        Date,
        Time,
        Year,
        Month,
        Day,
        MonthDay,
        YearMonth,
        Duration,
        URI,
        Timespan,
        Count,
        InclusiveValueRange,
        ExclusiveValueRange,
        Incremental,
        ObservationalTimePeriod
    }
}
