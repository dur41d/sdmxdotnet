using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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

        public static void ThrowParseError(XmlReader reader, string format, params object[] args)
        {
            string message = string.Format(format, args);
            var xml = reader as IXmlLineInfo;
            throw new SDMXException("Parse error at ({0},{1}): {2}", xml.LineNumber, xml.LinePosition, message);
        }

        public static void ThrowParseError(XmlReader reader, Exception inner, string format, params object[] args)
        {
            string message = string.Format(format, args);
            var xml = reader as IXmlLineInfo;
            string message2 = string.Format("Parse error at ({0},{1}): {2}", xml.LineNumber, xml.LinePosition, message);
            throw new SDMXException(message2, inner);
        }
    }
}
