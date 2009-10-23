using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class InternationalString
    {
        public Language Language { get; private set; }
        public string Value { get; private set; }

        public InternationalString(Language language, string value)
        {
            Language = language;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object other)
        {
            return Equals(other as InternationalString);
        }

        public bool Equals(InternationalString other)
        {
            return Language.Equals(other.Language) &&
                Value.Equals(other.Value);
        }

        public static implicit operator InternationalString(string value)
        {
            return new InternationalString(Language.English, value);
        }

        public static implicit operator string(InternationalString iString)
        {
            if (iString == null)
            {
                return null;
            }
            return iString.Value;
        }

        public static bool operator ==(InternationalString x, InternationalString y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(InternationalString x, InternationalString y)
        {
            return !Equals(x, y);
        }      
    }
}
