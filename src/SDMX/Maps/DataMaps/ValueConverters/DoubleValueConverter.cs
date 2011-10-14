using System;

namespace SDMX.Parsers
{
    internal class DoubleValueConverter : IValueConverter
    {
        public object Parse(string s, string startTime)
        {
            return double.Parse(s);
        }

        public string Serialize(object obj, out string startTime)
        {
            double value = (double)obj;
            startTime = null;
            return value.ToString();
        }
    }
}
