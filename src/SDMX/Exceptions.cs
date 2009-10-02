using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class SDMXException : Exception
    {
        public SDMXException(string message)
            : base(message)
        {
        }
    }
}
