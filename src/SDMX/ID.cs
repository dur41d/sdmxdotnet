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
        private string _value;
        static Regex regex = new Regex("^([A-Z]|[a-z]|\\*|@|[0-9]|_|$|\\-)*$", RegexOptions.Compiled);

        public ID(string id)
        {
            Validate(id);
            _value = id;
        }

        public static bool IsValid(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return regex.IsMatch(id);
        }

        public static void Validate(string id)
        {
            if (!IsValid(id))
            {
                throw new SDMXException("Invalid ID value '{0}'".F(id));
            }    
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

        public static implicit operator ID(string id)
        {
            return new ID(id);
        }      

        public static bool operator ==(ID x, object y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ID x, object y)
        {
            return !x.Equals(y);
        }

        public static bool operator ==(ID? x, object y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ID? x, object y)
        {
            return !x.Equals(y);
        }
    }
}
