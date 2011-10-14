using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public partial class DataReader : IDataReader
    {

        public void Close()
        {
            throw new NotImplementedException();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            return _table.Clone();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public int FieldCount
        {
            get { return _record.Count; }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            return _table.Columns[name].Ordinal;
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            string name = _table.Columns[i].ColumnName;
            return _record[name];
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }
    }

    public abstract partial class DataReader : IDisposable, IEnumerable<KeyValuePair<string, object>>
    {
        protected XmlReader _xmlReader;
        Dictionary<string, object> _record = new Dictionary<string, object>();       
        
        public KeyFamily KeyFamily { get; protected set; }

        List<string> _obsAttributes = null;
        
        List<string> _optionalAttributes = null;
        Dictionary<string, Dictionary<string, object>> _groupValues = new Dictionary<string, Dictionary<string, object>>();
        Dictionary<string, Func<object, object>> _casts = new Dictionary<string, Func<object, object>>();

        DataTable _table = new DataTable();

        bool _disposed = false;

        public DataReader(XmlReader reader, KeyFamily keyFamily)
        {
            _xmlReader = reader;
            KeyFamily = keyFamily;

            BuildTable();
        }

        public void Cast(string name, Func<object, object> castAction)
        {
            Contract.AssertNotNullOrEmpty(name, "name");
            Contract.AssertNotNull(castAction, "castAction");

            _casts.Add(name, castAction);
        }

        protected void ClearRecord()
        {
            _record.Clear();
        }

        protected void SetRecord(string name, object value)
        {
            if (_casts.ContainsKey(name))
                value = _casts[name](value);

            _record[name] = value;
        }

        void BuildTable()
        {
            _table.TableName = KeyFamily.Name.First().ToString();

            DataColumn col = null;
            foreach (var dim in KeyFamily.Dimensions)
            {
                col = new DataColumn(dim.Concept.Id, dim.GetValueType());
                col.AllowDBNull = false;
                _table.Columns.Add(col);
            }

            col = new DataColumn(KeyFamily.TimeDimension.Concept.Id, KeyFamily.TimeDimension.GetValueType());
            col.AllowDBNull = false;
            _table.Columns.Add(col);

            col = new DataColumn(KeyFamily.PrimaryMeasure.Concept.Id, KeyFamily.PrimaryMeasure.GetValueType());
            col.AllowDBNull = false;
            _table.Columns.Add(col);

            foreach (var attr in KeyFamily.Attributes)
            {
                col = new DataColumn(attr.Concept.Id, attr.GetValueType());
                col.AllowDBNull = attr.AssignmentStatus == AssignmentStatus.Conditional;
                _table.Columns.Add(col);
            }
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
            if (_disposed) 
                throw new ObjectDisposedException("DataReader"); 
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            if (_obsAttributes != null) _obsAttributes.Clear();
            if (_optionalAttributes != null) _optionalAttributes.Clear();

            _record.Clear();
            _groupValues.Clear();
            ((IDisposable)_xmlReader).Dispose();
            _disposed = true;
        }

        protected void ClearObsAttributes()
        {
            if (_obsAttributes == null)
            {
                _obsAttributes = new List<string>();
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
                        throw new SDMXException("Value for Dimension '{0}' is missing.", id);

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

        protected void FillMissingAttributes()
        {
            if (_optionalAttributes == null)
            {
                _optionalAttributes = new List<string>();
                foreach (var attr in KeyFamily.Attributes.Where(i => i.AssignmentStatus == AssignmentStatus.Conditional))
                    _optionalAttributes.Add(attr.Concept.Id);
            }

            foreach (var attr in _optionalAttributes)
                if (!_record.ContainsKey(attr))
                    _record.Add(attr, null);
        }

        //#region IDataReader

        //void IDataReader.Close()
        //{
        //    Dispose();
        //}

        //int IDataReader.Depth
        //{
        //    get { return 0; }
        //}

        //DataTable IDataReader.GetSchemaTable()
        //{
        //    throw new NotImplementedException();
        //}

        //bool IDataReader.IsClosed
        //{
        //    get { return _disposed; }
        //}

        //bool IDataReader.NextResult()
        //{
        //    return false;
        //}

        //bool IDataReader.Read()
        //{
        //    return Read();
        //}

        //int IDataReader.RecordsAffected
        //{
        //    get { return -1; }
        //}

        //void IDisposable.Dispose()
        //{
        //    Dispose();
        //}

        //int IDataRecord.FieldCount
        //{
        //    get { return _record.Count; }
        //}

        //bool IDataRecord.GetBoolean(int i)
        //{
        //    return (bool)_record.ElementAt(i).Value;
        //}

        //byte IDataRecord.GetByte(int i)
        //{
        //    return (byte)_record.ElementAt(i).Value;
        //}

        //long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        //{
        //    throw new NotImplementedException();
        //}

        //char IDataRecord.GetChar(int i)
        //{
        //    return (char)_record.ElementAt(i).Value;
        //}

        //long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        //{
        //    throw new NotImplementedException();
        //}

        //IDataReader IDataRecord.GetData(int i)
        //{
        //    return this;
        //}

        //string IDataRecord.GetDataTypeName(int i)
        //{
        //    return _record.ElementAt(i).Value.GetType().Name;
        //}

        //DateTime IDataRecord.GetDateTime(int i)
        //{
        //    return (DateTime)_record.ElementAt(i).Value;
        //}

        //decimal IDataRecord.GetDecimal(int i)
        //{
        //    return (decimal)_record.ElementAt(i).Value;
        //}

        //double IDataRecord.GetDouble(int i)
        //{
        //    return (double)_record.ElementAt(i).Value;
        //}

        //Type IDataRecord.GetFieldType(int i)
        //{
        //    return _record.ElementAt(i).GetType();
        //}

        //float IDataRecord.GetFloat(int i)
        //{
        //    return (float)_record.ElementAt(i).Value;
        //}

        //Guid IDataRecord.GetGuid(int i)
        //{
        //    return new Guid((string)_record.ElementAt(i).Value);
        //}

        //short IDataRecord.GetInt16(int i)
        //{
        //    return (short)_record.ElementAt(i).Value;
        //}

        //int IDataRecord.GetInt32(int i)
        //{
        //    return (int)_record.ElementAt(i).Value;
        //}

        //long IDataRecord.GetInt64(int i)
        //{
        //    return (long)_record.ElementAt(i).Value;
        //}

        //string IDataRecord.GetName(int i)
        //{
        //    return _record.ElementAt(i).Key;
        //}

        //int IDataRecord.GetOrdinal(string name)
        //{
        //    // case-sensitive search first and then case-insensitive
        //    // as per IDataRecord.GetOrdinal specs
        //    int i = 0;
        //    foreach (var key in _record.Keys)
        //        if (string.Compare(key, name, false) == 0)
        //            return i;
        //        else
        //            i++;

        //    i = 0;
        //    foreach (var key in _record.Keys)
        //        if (string.Compare(key, name, true) == 0)
        //            return i;
        //        else
        //            i++;

        //    throw new IndexOutOfRangeException();
        //}       

        //string IDataRecord.GetString(int i)
        //{
        //    return (string)_record.ElementAt(i).Value;
        //}

        //object IDataRecord.GetValue(int i)
        //{
        //    return _record.ElementAt(i).Value;
        //}

        //int IDataRecord.GetValues(object[] values)
        //{   
        //    int count = values.Length < _record.Count ? values.Length : _record.Count;

        //    int counter = 0;
        //    for (int i = 0; i < count; i++)
        //    {
        //        values[i] = _record.ElementAt(i).Value;
        //        counter++;
        //    }

        //    return counter;
        //}

        //bool IDataRecord.IsDBNull(int i)
        //{
        //    return false;
        //}

        //object IDataRecord.this[string name]
        //{
        //    get { return this[name]; }
        //}

        //object IDataRecord.this[int i]
        //{
        //    get { return _record.ElementAt(i).Value; }
        //}

        //#endregion
    }
}
