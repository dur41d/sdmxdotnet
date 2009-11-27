using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public enum Language
    {
        English
    }

    public struct InternationalString : IEquatable<InternationalString>
    {
        public readonly Language Language;
        public readonly string Value;

        public InternationalString(Language language, string value)
        {
            Language = language;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() * 37 + Language.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is InternationalString)) return false;
            return Equals((InternationalString)other);
        }

        public bool Equals(InternationalString other)
        {
            return Language.Equals(other.Language) &&
                Value.Equals(other.Value);
        }       

        public static implicit operator string(InternationalString iString)
        {           
            return iString.Value;
        }

        public static bool operator ==(InternationalString x, InternationalString y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(InternationalString x, InternationalString y)
        {
            return !x.Equals(y);
        }      
    }
}
