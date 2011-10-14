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

        public static Id Create(string id)
        {            
            Id result = null;
            if (!ids.TryGetValue(id, out result))
            {
                Validate(id);
                result = new Id(id);
                ids.Add(id, result);                
            }
            return result;
        }

        static Dictionary<string, Id> ids = new Dictionary<string, Id>();

        public static bool IsValid(string id)
        {
            Contract.AssertNotNull(id, "id");
            return regex.IsMatch(id);
        }

        public static void Validate(string id)
        {
            if (!IsValid(id))
            {
                throw new SDMXException("Invalid Id value '{0}'".F(id));
            }    
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
            return Create(id);
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
