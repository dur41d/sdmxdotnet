using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{
    [Serializable]
    public class OXMException : Exception
    {
        public OXMException() { }
        public OXMException(string message) : base(message) { }
        public OXMException(string message, Exception inner) : base(message, inner) { }
        protected OXMException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public OXMException(string format, params object[] args) 
            : base(string.Format(format, args)) { }
    }
}
