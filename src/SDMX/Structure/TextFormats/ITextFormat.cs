using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public interface ITextFormat
    {
        bool IsValid(Value value);
        Type GetValueType();
        bool TryParse(string s, string startTime, out object value);
    }
}