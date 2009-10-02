using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SDMX
{
    /// <summary>
    /// A class to restrict data to this pattern: ([A-Z]|[a-z]|\*|@|[0-9]|_|$|\-)*
    /// </summary>
    public class ID
    {   
        private string value;
        private const string _pattern = "^([A-Z]|[a-z]|\\*|@|[0-9]|_|$|\\-)*$";      

        public ID(string id)
        {
            if (!Regex.IsMatch(id, _pattern))
            {
                throw new SDMXException("Invalid ID value '{0}'".F(id));
            }

            this.value = id;
        }

        public override string  ToString()
        {
             return value;
        }

        public override bool Equals(object other)
        {
            return Equals(other as ID);
        }

        public bool Equals(ID other)
        {
            return value.Equals(other.value);
        }    

        public static implicit operator ID(string id)
        {
            return new ID(id);
        }

        public static implicit operator string(ID id)
        {
            return id.value;
        }

        public static bool operator ==(ID x, ID y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(ID x, ID y)
        {
            return !Equals(x, y);
        }      
    }
}
