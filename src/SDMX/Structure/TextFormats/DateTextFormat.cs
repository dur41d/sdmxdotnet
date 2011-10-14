using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public class DateTextFormat : TimePeriodTextFormatBase
    {
        static IValueConverter _converter = new DateValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            return obj is Date;
        }

        public override bool Equals(TextFormat other)
        {
            return other is DateTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(Date);
        }
    }
}
