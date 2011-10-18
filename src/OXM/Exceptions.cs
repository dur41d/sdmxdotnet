using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Xml;

namespace OXM
{
    [Serializable]
    public class ParseException : Exception
    {        
        public ParseException(string message) : base(message) { }

        public ParseException(string format, params object[] args) 
            : base(string.Format(format, args)) { }

        public ParseException(string message, Exception innerException)
            : base(message, innerException) { }

        public static void Throw(XmlReader reader, Type type, string format, params object[] args)
        {
            var xml = reader as IXmlLineInfo;
            string message = string.Format("Parsing error at ({0},{1}): {3} Type=ClassMap<{2}>.",
                xml.LineNumber, xml.LinePosition, type.Name, string.Format(format, args));
            throw new ParseException(message);
        }

        public static void Throw(XmlReader reader, Type type, Exception inner, string format, params object[] args)
        {
            var xml = reader as IXmlLineInfo;
            string message = string.Format("Parsing error at ({0},{1}): {3} Type=ClassMap<{2}>.",
                xml.LineNumber, xml.LinePosition, type.Name, string.Format(format, args));
            throw new ParseException(message, inner);
        }
    }
}
