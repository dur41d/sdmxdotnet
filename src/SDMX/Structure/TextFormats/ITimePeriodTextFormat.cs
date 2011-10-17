using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public abstract class TimePeriodTextFormatBase : TextFormat
    {
        public override bool IsValid(object obj)
        {
            return obj is DateTime || obj is DateTimeOffset;
        }

        public override Type GetValueType()
        {
            return typeof(DateTimeOffset);
        }
    }
}
