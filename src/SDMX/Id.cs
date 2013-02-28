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
    public class Id : IEquatable<Id>, IEquatable<string>
    {   
        private string _value;
        static Regex regex = new Regex("^([A-Z]|[a-z]|\\*|@|[0-9]|_|$|\\-)*$", RegexOptions.Compiled);

        private Id(string id)
        {           
            _value = id;
        }

        static Dictionary<string, Id> ids = new Dictionary<string, Id>();

        public static bool TryParse(string id, out Id result)
        {
            id = id.Trim();
            result = null;
            if (!ids.TryGetValue(id, out result))
            {
                if (regex.IsMatch(id))
                {
                    result = new Id(id);
                    ids.Add(id, result);
                }
            }
            return result != null;
        }

        public override string  ToString()
        {
             return _value;
        }

        public override int GetHashCode()
        {            
            return _value.GetHashCode();
        }

        public static implicit operator Id(string id)
        {
            Id result = null;
            if (TryParse(id, out result))
            {
                return result;
            }

            throw new SDMXException("Invalid id value '{0}'.", id);
        }

        public static implicit operator string(Id id)
        {
            return id._value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Id);
        }

        public bool Equals(string other)
        {
            return _value == other;
        }

        public bool Equals(Id other)
        {
            if (other == null) return false;

            return _value == other._value;
        }

        public static bool operator ==(Id x, Id y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(Id x, Id y)
        {
            return !(x == y);
        }
    }
}
