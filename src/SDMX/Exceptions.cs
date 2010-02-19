using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    [global::System.Serializable]
    public class SDMXException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public SDMXException() { }
        public SDMXException(string message) : base(message) { }
        public SDMXException(string message, Exception inner) : base(message, inner) { }
        protected SDMXException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public SDMXException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

    }
}
