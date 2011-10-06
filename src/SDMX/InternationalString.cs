using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class InternationalString : IEquatable<InternationalString>
    {
        public string Language { get; private set; }
        public string Value { get; private set; }

        public InternationalString(string lang, string value)
        {
            Language = lang;
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
            return Equals(other as InternationalString);
        }

        public bool Equals(InternationalString other)
        {
            return this.Equals(other, () =>
            {
                return Language.Equals(other.Language)
                    && Value.Equals(other.Value);
            });
        }       

        public static implicit operator string(InternationalString iString)
        {           
            return iString.Value;
        }

        public static bool operator ==(InternationalString x, InternationalString y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(InternationalString x, InternationalString y)
        {
            return !(x == y);
        }      
    }
}
