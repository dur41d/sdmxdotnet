using System;

namespace SDMX
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

        public static Action<OXM.ValidationMessage> CastDelegate(Action<SDMX.ValidationMessage> sdmxAction)
        {
            if (sdmxAction == null)
                return null;

            return delegate(OXM.ValidationMessage oxm)
            {
                var sdmx = new SDMX.ValidationMessage(oxm.Message, CastSeverity(oxm.Severity), oxm.LineNumber, oxm.LinePosition, oxm.Exception);
                sdmxAction(sdmx);
            };
        }

        private static SDMX.SeverityType CastSeverity(OXM.SeverityType oxm)
        {
            if (oxm == OXM.SeverityType.Error)
                return SDMX.SeverityType.Error;
            else
                return SDMX.SeverityType.Warning;
        }
    }
}
