using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Common
{
    public static class Contract
    {       
        public static void AssertNotNull(object o, string message)
        {           
            if (o == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void AssertNotNullOrEmpty(string s, string message)
        {        
            if (s == null)
            {
                throw new ArgumentNullException(string.Format("String is null: '{0}'", message));
            }
            if (s.Trim() == "")
            {
                throw new ArgumentException(string.Format("String is empty: '{0}'", message));
            }
        }
    }
}
