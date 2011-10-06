using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    [global::System.Serializable]
    public class SDMXException : Exception
    {
        public SDMXException() { }
        public SDMXException(string message) : base(message) { }
        public SDMXException(string message, Exception inner) : base(message, inner) { }
        protected SDMXException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public SDMXException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public static void Throw(string format, params object[] args)
        {
            throw new SDMXException(format, args);
        }

    }
}
