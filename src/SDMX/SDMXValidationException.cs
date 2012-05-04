using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX
{
    [Serializable]
    public class SDMXValidationException : Exception
    {
        public SDMXValidationException() { }
        public SDMXValidationException(string message) : base(message) { }
        public SDMXValidationException(string message, Exception inner) : base(message, inner) { }
        protected SDMXValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public IEnumerable<Error> Errors { get; private set; }

        public static SDMXValidationException Create(List<Error> errors, string message)
        {
            var builder = new StringBuilder();
            builder.AppendLine(message);
            foreach (var error in errors)
            {
                builder.AppendLine(error.Message);
            }

            var ex = new SDMXValidationException(builder.ToString());
            ex.Errors = errors;

            return ex;
        }
    }
}
