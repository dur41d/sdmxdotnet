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
    public enum SeverityType
    {      
        Error = 0,
        Warning = 1,
    }

    public class ValidationMessage
    {
        public string Message { get; private set; }
        public int LineNumber { get; private set; }
        public int LinePosition { get; private set; }
        public SeverityType Severity { get; private set; }
        public Exception Exception { get; private set; }

        public ValidationMessage(string message, SeverityType severity, int lineNumber, int linePosition)
            : this(message, severity, lineNumber, linePosition, null)
        { }

        public ValidationMessage(string message, SeverityType severity, int lineNumber, int linePosition, Exception exception)
        {
            Message = message;
            Severity = severity;
            LineNumber = lineNumber;
            LinePosition = linePosition;
            Exception = exception;
        }
        
    }
    
    [Serializable]
    public class ParseException : Exception
    {
        public int LineNumber { get; internal set; }
        public int LinePosition { get; internal set; }
        public ParseException(string message) : base(message) { }
        public ParseException(string format, params object[] args) 
            : base(string.Format(format, args)) { }

        public ParseException(string message, Exception innerException)
            : base(message, innerException) { }

        public ParseException(string message, Exception innerException, int lineNumber, int linePosition)
            : base(message, innerException) 
        { 
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }
    }

    [Serializable]
    public class SerializationException : Exception
    {
        public SerializationException(string message) : base(message) { }
        public SerializationException(string format, params object[] args)
            : base(string.Format(format, args)) { }
        public SerializationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    [Serializable]
    public class MappingException : Exception
    {
        public MappingException(string message) : base(message) { }
        public MappingException(string format, params object[] args)
            : base(string.Format(format, args)) { }
        public MappingException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class Helper
    {
        public static void Notify(Action<ValidationMessage> validationAction, XmlReader reader, Type type, string format, params object[] args)
        {
            var xml = reader as IXmlLineInfo;
            string message = string.Format(@"Parse Error: {3} 
Line: {0}, Position: {1}  
Type: ClassMap<{2}>.",
                xml.LineNumber, xml.LinePosition, type.Name, string.Format(format, args));

            if (validationAction == null)
            {
                throw new ParseException(message) { LineNumber = xml.LineNumber, LinePosition = xml.LinePosition };
            }
            else
            { 
                validationAction(new ValidationMessage(message, SeverityType.Error, xml.LineNumber, xml.LinePosition));
            }
        }

        public static void NotifyWarning(Action<ValidationMessage> validationAction, XmlReader reader, Type type, string format, params object[] args)
        {
            var xml = reader as IXmlLineInfo;
            string message = string.Format(@"Parse Warning: {3} 
Line: {0}, Position: {1}  
Type: ClassMap<{2}>.",
                xml.LineNumber, xml.LinePosition, type.Name, string.Format(format, args));

            if (validationAction == null)
            {
                System.Diagnostics.Debug.WriteLine(message, "Warning");
            }
            else
            {
                validationAction(new ValidationMessage(message, SeverityType.Warning, xml.LineNumber, xml.LinePosition));
            }
        }


    }
}
