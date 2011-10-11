using System;

namespace SDMX.Parsers
{
    internal interface ITimePeriodConverter : IValueConverter
    {
        bool IsValid(string s);
    }
}
