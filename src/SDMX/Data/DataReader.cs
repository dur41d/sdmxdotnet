using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;
using System.Xml;

namespace SDMX
{
    public abstract class DataReader : IDisposable
    {
        protected static XmlReader _xmlReader;
        protected Dictionary<string, object> _record = new Dictionary<string, object>();
        internal ValueConverter _converter = new ValueConverter();
        protected List<string> _obsKeys = new List<string>();
        public KeyFamily KeyFamily { get; protected set; }

        public object Value { get; protected set; }        

        public object this[string name]
        {
            get
            {
                Contract.AssertNotNullOrEmpty(name, "name");

                if (!_record.ContainsKey(name))
                    throw new SDMXException("{0} not found.", name);

                return _record[name];
            }
        }

        public IEnumerable<KeyValuePair<string, object>> Items
        {
            get
            {
                foreach (var item in _record)
                    yield return item;
            }
        }


        public static DataReader Create(string fileName, KeyFamily keyFamily)
        {
            Contract.AssertNotNullOrEmpty(fileName, "fileName");

            _xmlReader = XmlReader.Create(fileName);            

            if (_xmlReader.ReadState == ReadState.Initial)
            {
                _xmlReader.Read();
            }

            if (_xmlReader.NodeType != XmlNodeType.Element)
            {
                _xmlReader.ReadNextElement();
            }

            if (_xmlReader.LocalName == "CompactData")
                return new CompactDataReader(_xmlReader, keyFamily);
            else if (_xmlReader.LocalName == "GenericData")
                return new GenericDataReader(_xmlReader, keyFamily);
            else
                throw new SDMXException("Invalid root element for data file {0}.", fileName);
        }

        public abstract bool Read();

        public void Dispose()
        {
            _xmlReader.Close();
        }

        protected void ClearObsValues()
        {
            if (_obsKeys == null)
            {
                _obsKeys.Add(KeyFamily.TimeDimension.Concept.ID.ToString());
                _obsKeys.Add(KeyFamily.PrimaryMeasure.Concept.ID.ToString());
                foreach (var attribute in KeyFamily.Attributes.Where(a => a.AttachementLevel == AttachmentLevel.Observation))
                    _obsKeys.Add(attribute.Concept.ID.ToString());
            }

            foreach (string key in _obsKeys)
                _record.Remove(key);
        }
    }
}
