using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class DateTextFormat : TimePeriodTextFormatBase
    {
        static ISimpleTypeConverter _converter = new DateConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        public override bool Equals(TextFormat other)
        {
            return other is DateTextFormat;
        }
    }
}
