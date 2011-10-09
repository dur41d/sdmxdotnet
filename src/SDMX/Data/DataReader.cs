using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;
using System.Xml;
using System.IO;
using System.Data;

namespace SDMX
{
    public abstract class DataReader : IDisposable, IEnumerable<KeyValuePair<string, object>>, IDataReader
    {
        protected XmlReader _xmlReader;
        protected Dictionary<string, object> _record = new Dictionary<string, object>();        
        
        public KeyFamily KeyFamily { get; protected set; }

        List<string> _obsAttributes = new List<string>();
        Dictionary<string, Dictionary<string, object>> _groupValues = new Dictionary<string, Dictionary<string, object>>();

        bool isDisposed = false;

        public DataReader(XmlReader reader, KeyFamily keyFamily)
        {
            _xmlReader = reader;
            KeyFamily = keyFamily;
        }

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

        public static DataReader Create(string fileName, KeyFamily keyFamily)
        {
            Contract.AssertNotNullOrEmpty(fileName, "fileName");
            return Create(XmlReader.Create(fileName), keyFamily);
        }

        public static DataReader Create(Stream stream, KeyFamily keyFamily)
        {
            Contract.AssertNotNull(stream, "stream");
            return Create(XmlReader.Create(stream), keyFamily);
        }

        public static DataReader Create(XmlReader reader, KeyFamily keyFamily)
        {
            Contract.AssertNotNull(reader, "reader");

            if (reader.ReadState == ReadState.Initial)
            {
                reader.Read();
            }

            if (reader.NodeType != XmlNodeType.Element)
            {
                reader.ReadNextElement();
            }

            if (reader.LocalName == "CompactData")
                return new CompactDataReader(reader, keyFamily);
            else if (reader.LocalName == "GenericData")
                return new GenericDataReader(reader, keyFamily);
            else
                throw new SDMXException("Unsupported root element ({0}) for data file.", reader.LocalName);
        }

        public abstract bool Read();

        public Header ReadHeader()
        {
            CheckDisposed();

            while (_xmlReader.Read() && !_xmlReader.IsStartElement() && _xmlReader.LocalName != "DataSet")
                continue;

            if (_xmlReader.LocalName == "Header")
            {
                var map = new OXM.FragmentMap<Header>(Namespaces.Message + "Header", new HeaderMap());
                return map.ReadXml(_xmlReader);
            }

            return null;
        }

        protected void CheckDisposed()
        {
            if (isDisposed) throw new ObjectDisposedException("DataReader"); 
        }

        public void Dispose()
        {
            _obsAttributes.Clear();
            _record.Clear();
            _groupValues.Clear();
            _xmlReader.Close();
            isDisposed = true;
        }

        protected void ClearObsAttributes()
        {
            if (_obsAttributes.Count == 0)
            {                
                foreach (var attribute in KeyFamily.Attributes.Where(a => a.AttachementLevel == AttachmentLevel.Observation))
                    _obsAttributes.Add(attribute.Concept.Id.ToString());
            }

            foreach (string key in _obsAttributes)
                _record.Remove(key);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var item in _record)
                yield return item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void ReadGroupValues(GroupDescriptor group, Dictionary<string, object> dict)
        {
            var keyList = new List<string>();
            var dimList = group.Dimensions.Select(i => i.Concept.Id).ToList();

            foreach (var id in dimList)
            {
                object value = null;
                if (!dict.TryGetValue(id, out value))
                    throw new SDMXException("Group '{0}' is missing value for dimension '{1}'.", group.Id, id);

                keyList.Add(string.Format("{0}={1}", id, value));
            }

            // build key from group dimension values
            string key = string.Join(",", keyList.ToArray());

            var values = new Dictionary<string, object>();
            foreach (var item in dict)
                if (!dimList.Contains(item.Key))
                    values.Add(item.Key, item.Value);

            _groupValues.Add(key, values);
        }

        protected void SetGroupValues()
        {
            foreach (var group in KeyFamily.Groups)
            {
                var keyList = new List<string>();

                foreach (var id in group.Dimensions.Select(i => i.Concept.Id))
                {
                    object value = null;

                    if (!_record.TryGetValue(id, out value))
                        throw new SDMXException("Value for Dimension '{0}' is missin.", id);

                    keyList.Add(string.Format("{0}={1}", id, value));
                }

                // build key from group dimension values
                string key = string.Join(",", keyList.ToArray());

                Dictionary<string, object> _values;
                _groupValues.TryGetValue(key, out _values);

                if (_values == null)
                    throw new SDMXException("Group '{0}' was not found for values '{1}'. Groups must be placed before their respective Series in the file.", group.Id, key);

                foreach (var value in _values)
                    _record[value.Key] = value.Value;
            }
        }

        #region IDataReader

        void IDataReader.Close()
        {
            Dispose();
        }

        int IDataReader.Depth
        {
            get { throw new NotImplementedException(); }
        }

        DataTable IDataReader.GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        bool IDataReader.IsClosed
        {
            get { return isDisposed; }
        }

        bool IDataReader.NextResult()
        {
            throw new NotImplementedException();
        }

        bool IDataReader.Read()
        {
            return Read();
        }

        int IDataReader.RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        int IDataRecord.FieldCount
        {
            get { return _record.Count; }
        }

        bool IDataRecord.GetBoolean(int i)
        {
            return (bool)_record.ElementAt(i).Value;
        }

        byte IDataRecord.GetByte(int i)
        {
            return (byte)_record.ElementAt(i).Value;
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        char IDataRecord.GetChar(int i)
        {
            return (char)_record.ElementAt(i).Value;
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        IDataReader IDataRecord.GetData(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            return _record.ElementAt(i).Value.GetType().Name;
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            return (DateTime)_record.ElementAt(i).Value;
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            return (decimal)_record.ElementAt(i).Value;
        }

        double IDataRecord.GetDouble(int i)
        {
            return (double)_record.ElementAt(i).Value;
        }

        Type IDataRecord.GetFieldType(int i)
        {
            return _record.ElementAt(i).GetType();
        }

        float IDataRecord.GetFloat(int i)
        {
            return (float)_record.ElementAt(i).Value;
        }

        Guid IDataRecord.GetGuid(int i)
        {
            return new Guid((string)_record.ElementAt(i).Value);
        }

        short IDataRecord.GetInt16(int i)
        {
            return (short)_record.ElementAt(i).Value;
        }

        int IDataRecord.GetInt32(int i)
        {
            return (int)_record.ElementAt(i).Value;
        }

        long IDataRecord.GetInt64(int i)
        {
            return (long)_record.ElementAt(i).Value;
        }

        string IDataRecord.GetName(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetString(int i)
        {
            return (string)_record.ElementAt(i).Value;
        }

        object IDataRecord.GetValue(int i)
        {
            return _record.ElementAt(i).Value;
        }

        int IDataRecord.GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        bool IDataRecord.IsDBNull(int i)
        {
            return false;
        }

        object IDataRecord.this[string name]
        {
            get { return this[name]; }
        }

        object IDataRecord.this[int i]
        {
            get { return _record.ElementAt(i).Value; }
        }

        #endregion
    }
}
