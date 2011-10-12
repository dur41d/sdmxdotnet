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

        public abstract int Year { get; }
        public abstract int Month { get; }
        public abstract int Day { get; }
        public abstract int Hour { get; }
        public abstract int Minute { get; }
        public abstract int Second { get; }
        public abstract int Millisecond { get; }
        public abstract TimeSpan Offset { get; }
    }

}