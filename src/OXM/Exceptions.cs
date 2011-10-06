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

        public static void Throw(XmlReader reader, Type type, string format, params object[] args)
        {
            var xml = reader as IXmlLineInfo;
            string message = string.Format("Parsing error at ({0},{1}) Type='{2}', {3}",
                xml.LineNumber, xml.LinePosition, type.Name, string.Format(format, args));
            throw new ParseException(message);
        }

        public static void Throw(XmlReader reader, string format, params object[] args)
        {
            var xml = reader as IXmlLineInfo;
            string message = string.Format("Parsing error at ({0},{1}), {2}", 
                xml.LineNumber, xml.LinePosition, string.Format(format, args));
            throw new ParseException(message);
        }
    }
}
