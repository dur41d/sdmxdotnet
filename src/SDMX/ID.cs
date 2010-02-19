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
    public class ID
    {   
        private string _value;
        static Regex regex = new Regex("^([A-Z]|[a-z]|\\*|@|[0-9]|_|$|\\-)*$", RegexOptions.Compiled);

        private ID(string id)
        {           
            _value = id;
        }

        public static ID Create(string id)
        {            
            ID result = null;
            if (!ids.TryGetValue(id, out result))
            {
                Validate(id);
                result = new ID(id);
                ids.Add(id, result);                
            }
            return result;
        }

        static Dictionary<string, ID> ids = new Dictionary<string, ID>();


        public static bool IsValid(string id)
        {
            Contract.AssertNotNull(id, "id");
            return regex.IsMatch(id);
        }

        public static void Validate(string id)
        {
            if (!IsValid(id))
            {
                throw new SDMXException("Invalid ID value '{0}'".F(id));
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

        public static implicit operator ID(string id)
        {
            return Create(id);
        }       
    }
}
