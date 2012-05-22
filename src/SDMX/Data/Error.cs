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

        public Error(string format, params object[] args)
        {
            Message = string.Format(format, args);
        }
    }

    public class ValidationError : Error
    { 
        public ValidationError(string message)
            : base(message)
        { }

        public ValidationError(string format, params object[] args)
            : base(format, args)
        { }
    }

    public class ParseError : Error
    {
        public ParseError(string message)
            : base(message)
        { }

        public ParseError(string format, params object[] args)
            : base(format, args)
        { }
    }

    public class SerializationError : Error
    {
        public SerializationError(string message)
            : base(message)
        { }

        public SerializationError(string format, params object[] args)
            : base(format, args)
        { }
    }

    public class DuplicateKeyError : Error
    {
        public DuplicateKeyError(string message)
            : base(message)
        { }

        public DuplicateKeyError(string format, params object[] args)
            : base(format, args)
        { }
    }

    public class MandatoryComponentMissing : Error
    {
        public MandatoryComponentMissing(string message)
            : base(message)
        { }

        public MandatoryComponentMissing(string format, params object[] args)
            : base(format, args)
        { }
    }
}
