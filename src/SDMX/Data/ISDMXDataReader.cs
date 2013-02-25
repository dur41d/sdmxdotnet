using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Collections.ObjectModel;
using OXM;

namespace SDMX
{
    public interface ISDMXDataReader : IDataReader, IDisposable, IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// The key family used by the current reader
        /// </summary>
        KeyFamily KeyFamily { get; }

        /// <summary>
        /// Throw an exception if the reader encounters an error; otherwise, don't throw an exception
        /// and add the error the the Errors property and set the IsValid to false.
        /// The default is true.
        /// </summary>
        bool ThrowExceptionIfNotValid { get; set; }

        /// <summary>
        /// If set to true, the reader will check for duplicate keys.
        /// Important: setting it to true can have an impact on performace and memory foot print
        /// because the data reader needs to keep track of all the keys in the data set.
        /// </summary>
        bool DetectDuplicateKeys { get; set; }

        /// <summary>
        /// The inner xml reader used by the reader.
        /// </summary>
        XmlReader XmlReader { get; }

        /// <summary>
        /// The line number at the current reader position.
        /// </summary>
        int LineNumber { get; }

        /// <summary>
        /// The line position at the current reader position.
        /// </summary>
        int LinePosition { get; }

        ReadOnlyCollection<Error> Errors { get; }

        string ErrorString { get; }

        bool IsValid { get; }

        /// <summary>
        /// Read the head of a Data Message. This should be done first before calling the Read method.
        /// </summary>
        /// <returns>The header instance.</returns>
        Header ReadHeader();

        /// <summary>
        /// Read the head of a Data Message. This should be done first before calling the Read method.
        /// </summary>
        /// <returns>The header instance.</returns>
        Header ReadHeader(Action<ValidationMessage> validationAction);
    }
}
