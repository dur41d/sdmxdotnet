using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    /// <summary>
    /// A structure to restrict data to this pattern: ([A-Z]|[a-z]|\*|@|[0-9]|_|$|\-)*
    /// </summary>
    public struct ID : IEquatable<ID>
    {
        public static readonly ID Empty;
        private string _value;

        private const string _pattern = "^([A-Z]|[a-z]|\\*|@|[0-9]|_|$|\\-)*$";

        static ID()
        {
            Empty = default(ID);
        }

        public ID(string id)
        {
            if (id == null)
            {
                throw new InvalidCastException("Cannot convert a null string to ID.");
            }
            if (!Regex.IsMatch(id, _pattern))
            {
                throw new SDMXException("Invalid ID value '{0}'".F(id));
            }           

            _value = id;
        }

        public bool IsEmpty()
        {
            return _value == null;
        }

        public override string  ToString()
        {
             return _value;
        }

        public override int GetHashCode()
        {
            if (_value == null) return 0;
            return _value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is ID)) return false;
            return Equals((ID)other);
        }

        public bool Equals(ID other)
        {            
            return Equals(_value, other._value);
        }
       
        public static explicit operator ID(string id)
        {
            return new ID(id);
        }   

        public static bool operator ==(ID x, ID y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ID x, ID y)
        {
            return !x.Equals(y);
        }
    }
}
