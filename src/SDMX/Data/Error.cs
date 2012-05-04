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

    public class ParseError : Error
    {
        public ParseError(string message)
            : base(message)
        { }

        public ParseError(string format, params object[] args)
            : base(format, args)
        { }
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
}
