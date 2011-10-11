using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public abstract class TextFormat
    {
        public abstract bool IsValid(object value);

        internal abstract IValueConverter Converter { get; }

        public object Parse(string s, string startTime)
        {
            return Converter.Parse(s, startTime);
        }
    }
}