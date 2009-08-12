using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SDMX_ML.Framework.Common
{
    public class IDType
    {
        /// <summary>
        /// A class to restrict data to this pattern: ([A-Z]|[a-z]|\*|@|[0-9]|_|$|\-)*
        /// </summary>
        private string _value;
        private string _pattern = "([A-Z]|[a-z]|\\*|@|[0-9]|_|$|\\-)*";

        public string Value
        {
            get { return _value; }
            set 
            {
                if(TestValue(value))
                    _value = value;
                else
                    throw new Exception("Value " + _value + " does not match the pattern " + _pattern);
            }
        }

        public IDType(string id)
        {
            if(TestValue(id))
                _value = id;
            else
                throw new Exception("Value " + _value + " does not match the pattern " + _pattern);
        }

        public IDType()
        {
        }

        private bool TestValue(string id)
        {
            Regex r = new Regex(_pattern);

            Match match = r.Match(id);

            return match.Success;
        }

    }
}
