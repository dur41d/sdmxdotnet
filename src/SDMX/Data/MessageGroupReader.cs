using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDMX.Parsers;
using System.Xml;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;
using System.IO;
using OXM;

namespace SDMX
{
    /// <summary>
    /// Reads the data sets in MessageGroup message.
    /// </summary>
    public partial class MessageGroupReader : ISDMXDataReader
    {
        DataReader _reader;

        public KeyFamily KeyFamily { get; private set; }
               
        public bool ThrowExceptionIfNotValid { get; set; }

        public bool DetectDuplicateKeys { get; set; }

        public MessageGroupReader(string fileName, KeyFamily keyFamily)
            : this(XmlReader.Create(fileName), keyFamily)
        { }

        public MessageGroupReader(Stream stream, KeyFamily keyFamily)
            : this(XmlReader.Create(stream), keyFamily)
        { } 

        public MessageGroupReader(XmlReader xmlReader, KeyFamily keyFamily)
        {
            XmlReader = xmlReader;
            KeyFamily = keyFamily;
            ThrowExceptionIfNotValid = true;
        }

        public XmlReader XmlReader { get; private set; }
                
        public int LineNumber
        {
            get
            {
                return ((IXmlLineInfo)XmlReader).LineNumber;
            }
        }
        
        public int LinePosition
        {
            get
            {
                return ((IXmlLineInfo)XmlReader).LinePosition;
            }
        }

        public ReadOnlyCollection<Error> Errors
        {
            get
            {
                return _reader.Errors;
            }
        }

        public string ErrorString { get; protected set; }

        public bool IsValid
        {
            get
            {
                return _reader.IsValid;
            }
        }

        public object this[string name]
        {
            get
            {
                return _reader[name];
            }
        }

        /// <summary>
        /// Read the head of a Data Message. This should be done first before calling the Read method.
        /// </summary>
        /// <returns>The header instance.</returns>
        public Header ReadHeader()
        {
            return ReadHeader(null);
        }

        /// <summary>
        /// Read the head of a Data Message. This should be done first before calling the Read method.
        /// </summary>
        /// <returns>The header instance.</returns>
        public Header ReadHeader(Action<ValidationMessage> validationAction)
        {
            CheckDisposed();

            while (XmlReader.Read() && XmlReader.LocalName != "Header" && XmlReader.LocalName != "DataSet")
                continue;

            if (XmlReader.LocalName == "Header")
            {
                var map = new OXM.FragmentMap<Header>(Namespaces.Message + "Header", new HeaderMap());
                return map.ReadXml(XmlReader, ValidationMessage.CastDelegate(validationAction));
            }

            return null;
        }

        public bool Read()
        {
            CheckDisposed();

            if (_reader == null)
            {
                _reader = GetNextReader();
            }

            while (_reader != null)
            {
                bool dispose = false;               

                try
                {
                    if (_reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        dispose = true;
                    }
                }
                catch
                {
                    dispose = true;
                    throw;
                }
                finally
                {
                    if (dispose)
                    {
                        _reader.CleanUpResources();
                    }
                }

                _reader = GetNextReader();
            }

            return false;
        }

        DataReader GetNextReader()
        {   
            while (XmlReader.Read())
            {
                if (XmlReader.LocalName == "DataSet" && XmlReader.IsStartElement())
                {
                    if (XmlReader.NamespaceURI == Namespaces.Generic)
                    {
                        return new GenericDataReader(XmlReader, KeyFamily) 
                        { 
                            ThrowExceptionIfNotValid = ThrowExceptionIfNotValid,
                            DetectDuplicateKeys = DetectDuplicateKeys
                        };
                    }
                    else if (XmlReader.NamespaceURI == Namespaces.Compact)
                    {
                        return new CompactDataReader(XmlReader, KeyFamily)
                        {
                            ThrowExceptionIfNotValid = ThrowExceptionIfNotValid,
                            DetectDuplicateKeys = DetectDuplicateKeys
                        };
                    }
                }
            }

            return null;
        }

        public virtual IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _reader.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _reader.GetEnumerator();
        }

        #region IDisposable

        bool _disposed = false;

        /// <summary>
        /// Dispose the reader.
        /// </summary>        
        public void Dispose()
        {            
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        ~MessageGroupReader()
        {
            Dispose(false);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                ((IDisposable)XmlReader).Dispose();
            }

            _disposed = true;
        }

        #endregion        

        protected void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException("MessageGroupReader");
        }
    }
}
