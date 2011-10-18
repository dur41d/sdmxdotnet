using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using Common;
using SDMX.Parsers;
using System.Reflection;

namespace SDMX
{
    public abstract partial class DataReader : IDisposable, IEnumerable<KeyValuePair<string, object>>
    {
        public KeyFamily KeyFamily { get; protected set; }
        
        protected XmlReader _xmlReader;

        Dictionary<string, object> _obs = new Dictionary<string, object>();
        Dictionary<string, object> _series = new Dictionary<string, object>();
        Dictionary<string, Dictionary<string, object>> _groups = new Dictionary<string, Dictionary<string, object>>();
        Dictionary<string, object> _record = new Dictionary<string, object>();
 
        List<string> _optionalAttributes = null;
        List<Attribute> _mandatoryAttributes = null;
        DataTable _table = null;

        Dictionary<string, object> _casts = new Dictionary<string, object>();

        bool _disposed = false;

        public void Dispose()
        {
            if (_disposed)
                return;

            if (_optionalAttributes != null) _optionalAttributes.Clear();
            if (_mandatoryAttributes != null) _mandatoryAttributes.Clear();
            if (_table != null) _table.Clear();

            _optionalAttributes = null;
            _mandatoryAttributes = null;
            _table = null;
            
            _obs.Clear();
            _series.Clear();
            _groups.Clear();
            _record.Clear();
            _casts.Clear();
            ((IDisposable)_xmlReader).Dispose();
            _disposed = true;
        }


        public DataReader(XmlReader reader, KeyFamily keyFamily)
        {
            _xmlReader = reader;
            KeyFamily = keyFamily;
        }

        public void Cast<T>(string name, Func<object, T> castAction)
        {
            Contract.AssertNotNullOrEmpty(name, "name");
            Contract.AssertNotNull(castAction, "castAction");

            _casts.Add(name, castAction);
        }

        protected void ClearObs()
        {
            _obs.Clear();
        }

        protected void ClearSeries()
        {
            _series.Clear();
        }

        protected void SetSeries(string name, object value)
        {
            value = Cast(name, value);
            _series.Add(name, value);
        }

        protected void SetObs(string name, object value)
        {
            value = Cast(name, value);
            _obs.Add(name, value);
        }

        DataTable GetTable()
        {
            if (_table == null)
                BuildTable();

            return _table;
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

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var item in _record)
                yield return item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void SetGroup(Group group, Dictionary<string, object> dict)
        {
            var keyList = new List<string>();
            var dimList = group.Dimensions.Select(i => i.Concept.Id).ToList();

            var castedDict = new Dictionary<string, object>();

            foreach (var item in dict)
                castedDict.Add(item.Key, Cast(item.Key, item.Value));

            string key = BuildGroupKey(group, castedDict);

            var values = new Dictionary<string, object>();
            foreach (var item in castedDict.Where(i => !dimList.Contains(i.Key)))
                values.Add(item.Key, item.Value);

            _groups.Add(key, values);
        }

        protected void SetRecord()
        {
            _record.Clear();

            Action<string, object> set = (name, value) =>
                {
                    if (_record.ContainsKey(name))
                    {
                        if (!_record[name].Equals(value))
                            SDMXException.ThrowParseError(_xmlReader, "There are two differnt values for key: {0}, value1={1}, value2={2}.", name, value, _record[name]);
                    }
                    else
                    {
                        _record.Add(name, value);
                    }
                };

            foreach (var item in _series)
                set(item.Key, item.Value);

            foreach (var item in _obs)
                set(item.Key, item.Value);

            foreach (var group in KeyFamily.Groups)
            {
                string key = BuildGroupKey(group, _record);

                Dictionary<string, object> groupValues;
                if (!_groups.TryGetValue(key, out groupValues))
                    SDMXException.ThrowParseError(_xmlReader, "Group '{0}' was not found for values '{1}'. Groups must be placed before their respective Series in the file.", group.Id, key);

                foreach (var item in groupValues)
                    set(item.Key, item.Value);
            }

            foreach (var attr in GeOptionalAttributes())
                if (!_record.ContainsKey(attr))
                    _record.Add(attr, null);

            ValidateRecord();
        }

        void ValidateRecord()
        {
            Action<Component, string> validate = (com, name) =>
                {
                    if (!_record.ContainsKey(com.Concept.Id))
                        SDMXException.ThrowParseError(_xmlReader, "{0} '{1}' is missing from record.", name, com.Concept.Id);
                    else if (_record[com.Concept.Id] == null)
                        SDMXException.ThrowParseError(_xmlReader, "{0} '{1}' value  is null.", name, com.Concept.Id);
                };

            foreach (var dim in KeyFamily.Dimensions)
                validate(dim, "Dimension");

            validate(KeyFamily.TimeDimension, "TimeDimension");
            validate(KeyFamily.PrimaryMeasure, "PrimaryMeasure");

            foreach (var attr in GetMandatoryAttributes())
                validate(attr, "Attibute");
        }

        string BuildGroupKey(Group group, Dictionary<string, object> values)
        {
            var keyList = new List<string>();

            foreach (var id in group.Dimensions.Select(i => i.Concept.Id))
            {
                object value = null;

                if (!values.TryGetValue(id, out value))
                    SDMXException.ThrowParseError(_xmlReader, "Value for Dimension '{0}' is missing.", id);

                keyList.Add(string.Format("{0}={1}", id, value));
            }

            // build key from group dimension values
            string key = string.Join(",", keyList.ToArray());

            return key;
        }

        List<Attribute> GetMandatoryAttributes()
        {
            if (_mandatoryAttributes == null)
            {
                _mandatoryAttributes = new List<Attribute>();
                foreach (var attr in KeyFamily.Attributes.Where(i => i.AssignmentStatus == AssignmentStatus.Mandatory))
                    _mandatoryAttributes.Add(attr);
            }

            return _mandatoryAttributes;
        }

        List<string> GeOptionalAttributes()
        {
            if (_optionalAttributes == null)
            {
                _optionalAttributes = new List<string>();
                foreach (var attr in KeyFamily.Attributes.Where(i => i.AssignmentStatus == AssignmentStatus.Conditional))
                    _optionalAttributes.Add(attr.Concept.Id);
            }

            return _optionalAttributes;
        }

        void BuildTable()
        {
            Func<string, Type, bool, DataColumn> col = (name, type, isNull) =>
            {
                if (_casts.ContainsKey(name))
                    type = ((Delegate)_casts[name]).Method.ReturnType;
                var c = new DataColumn(name, type);
                c.AllowDBNull = isNull;
                return c;
            };

            _table = new DataTable();
            _table.TableName = KeyFamily.Id;

            foreach (var dim in KeyFamily.Dimensions)
                _table.Columns.Add(col(dim.Concept.Id, dim.GetValueType(), false));

            _table.Columns.Add(
                col(KeyFamily.TimeDimension.Concept.Id, KeyFamily.TimeDimension.GetValueType(), false));

            _table.Columns.Add(
                col(KeyFamily.PrimaryMeasure.Concept.Id, KeyFamily.PrimaryMeasure.GetValueType(), false));

            foreach (var attr in KeyFamily.Attributes)
                _table.Columns.Add(col(attr.Concept.Id, attr.GetValueType(),
                    attr.AssignmentStatus == AssignmentStatus.Conditional));
        }

        object Cast(string name, object value)
        {
            if (_casts.ContainsKey(name))
            {
                try
                {
                    return ((Delegate)_casts[name]).DynamicInvoke(value);
                }
                catch (Exception ex)
                {
                    if (ex is TargetInvocationException && ex.InnerException != null)
                        ex = ex.InnerException;

                    SDMXException.ThrowParseError(_xmlReader, ex, "Exception occured in at Cast (see inner exception for details): {0}", ex.Message);
                    return null;
                }
            }
            else
            {
                return value;
            }
        }
    }
}
