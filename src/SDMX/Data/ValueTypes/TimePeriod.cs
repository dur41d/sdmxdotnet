using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public abstract class TimePeriod : Value
    {
        public static bool operator ==(TimePeriod x, object y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(TimePeriod x, object y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

}