using System;
using System.Collections.Generic;
using System.Linq;

namespace SDMX
{
    public abstract class Error
    {
        public string Message { get; private set; }

        public Error(string message)
        {
            Message = message;
        }
    }

    public class ValidationError : Error
    {
        public int LineNumber { get; private set; }
        public int LinePosition { get; private set; }

        public ValidationError(int lineNumber, int linePosition, string message)
            : base(message)
        {
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }
    }

    public class ParseError : Error
    {
        public int LineNumber { get; private set; }
        public int LinePosition { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ParseError(string name, string value, int lineNumber, int linePosition, string message)
            : base(message)
        {
            Name = name;
            Value = value;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }
    }

    public class SerializationError : Error
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public SerializationError(string name, object value, string message)
            : base(message)
        {
            Name = name;
            Value = value;
        }
    }

    public class DuplicateKeyError : Error
    {
        public string Key { get; private set; }
        public int LineNumber { get; private set; }
        public int LinePosition { get; private set; }

        public DuplicateKeyError(string key, int lineNumber, int linePosition, string message)
            : base(message)
        {
            Key = key;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }
    }

    public class MandatoryComponentMissing : Error
    {
        public string Name { get; private set; }

        public MandatoryComponentMissing(string name, string message)
            : base(message)
        {
            Name = name;
        }
    }
}
