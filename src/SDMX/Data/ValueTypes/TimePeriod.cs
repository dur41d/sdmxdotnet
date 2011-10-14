using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public abstract class TimePeriod : IEquatable<TimePeriod>
    {
        public static bool operator ==(TimePeriod x, TimePeriod y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(TimePeriod x, TimePeriod y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TimePeriod);
        }

        public abstract bool Equals(TimePeriod other);
    }

}