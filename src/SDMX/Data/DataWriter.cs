using System;
using System.Xml;
using Common;
using System.IO;
using System.Text;
using System.Data;
using SDMX.Parsers;
using System.Collections.Generic;
using System.Linq;

namespace SDMX
{
    public abstract class DataWriter : IDisposable
    {
        bool _disposed = false;

        KeyValuePair<string, Dictionary<string, string>> prevGroup = new KeyValuePair<string, Dictionary<string, string>>();
        Dictionary<string, string> prevSeries = new Dictionary<string, string>();
        List<Attribute> _mandatoryAttributes;

        public DataValidator Validator { get; private set; }

        bool _wroteHeader = false;
        bool _wroteDataSet = false;
        bool _wroteRoot = false;
        bool _wroteGroup = false;
        bool _wroteSeries = false;

        public KeyFamily KeyFamily { get; protected set; }        

        public XmlWriter XmlWriter { get; protected set; }

        #region Constructors
        public DataWriter(string path, KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(path);
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(Stream stream, KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(stream);
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(XmlWriter writer, KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
            XmlWriter = writer;
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(StringBuilder stringBuilder, KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(stringBuilder);
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(TextWriter textWriter, KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(textWriter);
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(string path, KeyFamily keyFamily, XmlWriterSettings settings)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(path, settings);
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(Stream stream, KeyFamily keyFamily, XmlWriterSettings settings)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(stream, settings);
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(StringBuilder stringBuilder, KeyFamily keyFamily, XmlWriterSettings settings)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(stringBuilder, settings);
            Validator = new DataValidator(keyFamily);
        }

        public DataWriter(TextWriter textWriter, KeyFamily keyFamily, XmlWriterSettings settings)
        {
            KeyFamily = keyFamily;
            XmlWriter = XmlWriter.Create(textWriter, settings);
            Validator = new DataValidator(keyFamily);
        }


        #endregion Constructors
                        
        /// <summary>
        /// The line number at the current reader position.
        /// </summary>
        public int LineNumber      
        {
            get
            {
                return ((IXmlLineInfo)XmlWriter).LineNumber;
            }
        }

        /// <summary>
        /// The line position at the current reader position.
        /// </summary>
        public int LinePosition
        {
            get
            {
                return ((IXmlLineInfo)XmlWriter).LinePosition;
            }
        }

        public void WriteHeader(Header header)
        {
            CheckDisposed();

            if (!_wroteRoot)
            {
                WriteRoot();
                _wroteRoot = true;
            }

            if (header != null)
            {
                var map = new OXM.FragmentMap<Header>(Namespaces.Message + "Header", new HeaderMap());
                map.WriteXml(XmlWriter, header);
                _wroteHeader = true;
            }
        }

        //public void Write(DataReader reader)
        //{
        //    WriteHeader(reader.ReadHeader());
        //    Write((IDataReader)reader);            
        //}

        public void Write(IDataReader reader)
        {
            while (reader.Read())
            {
                WriteRecord(reader);
            }
        }

        /// <summary>
        /// Writes a record.
        /// </summary>
        /// <param name="record">The record.</param>
        public void WriteRecord(IDataRecord record)
        {
            var dict = GetDict(record);
            WriteRecord(dict);
        }
       
        /// <summary>
        /// Writes the record in the form of string, object dictionary.
        /// </summary>
        /// <param name="record">The record to write.</param>
        public void WriteRecord(Dictionary<string, object> record)
        {
            CheckDisposed();

            Dictionary<string, string> values = null;
            var errors = Validator.ValidateRecord(record, out values);

            if (errors.Count > 0)
            {
                throw SDMXValidationException.Create(errors, string.Format("Invalid record: {0}. See Errors property for details:", RecordToString(record)));
            }

            WriteRecord(values);
        }

        /// <summary>
        /// Writes a record that has been validated already.
        /// A record can be validated using the Validate method.
        /// </summary>
        /// <param name="validatedRecord">The validated record</param>
        public void WriteRecord(Dictionary<string, string> validatedRecord)
        {
            CheckDisposed();

            if (!_wroteHeader)
            {
                throw new SDMXException("Message header has not been written. Write header by calling WriteHeader method before writing any data.");
            }

            if (!_wroteRoot)
            {
                WriteRoot();
                _wroteRoot = true;
            }

            if (!_wroteDataSet)
            {
                var dataSetAttr = new Dictionary<string, string>();
                AddAttibutes(validatedRecord, dataSetAttr, a => a.AttachementLevel == AttachmentLevel.DataSet);
                WriteDataSet(dataSetAttr);
                _wroteDataSet = true;
            }

            // read groups
            foreach (var group in KeyFamily.Groups)
            {
                var groupKey = new Dictionary<string, string>();
                var groupAttr = new Dictionary<string, string>();

                foreach (var dim in group.Dimensions)
                {
                    string name = dim.Concept.Id;
                    groupKey.Add(name, validatedRecord[name]);
                }

                AddAttibutes(validatedRecord, groupAttr, a => a.AttachementLevel == AttachmentLevel.Group && a.AttachmentGroups.Contains(group.Id));

                if (HasValues(groupAttr))
                {
                    if (!_wroteGroup)
                    {
                        WriteGroup(group, groupKey, groupAttr);
                        SetPrevGroup(group.Id, groupKey);
                        _wroteGroup = true;
                    }
                    else
                    {
                        var prevGroup = GetPrevGroup();
                        if (!IsGroupEqual(prevGroup, group.Id, groupKey))
                        {
                            CloseGroup();
                            WriteGroup(group, groupKey, groupAttr);
                            SetPrevGroup(group.Id, groupKey);
                        }
                    }
                }
            }

            // read series
            var seriesKey = new Dictionary<string, string>();
            var seriesAttr = new Dictionary<string, string>();
            foreach (var dim in KeyFamily.Dimensions)
            {
                string name = dim.Concept.Id;
                seriesKey.Add(name, validatedRecord[name]);
            }

            AddAttibutes(validatedRecord, seriesAttr, a => a.AttachementLevel == AttachmentLevel.Series);

            if (!_wroteSeries)
            {
                WriteSeries(seriesKey, seriesAttr);
                SetSeries(seriesKey);
                _wroteSeries = true;
            }
            else
            {
                var previousSeries = GetPrevSeries();
                if (!IsDictEqual(previousSeries, seriesKey))
                {
                    CloseSeries();
                    WriteSeries(seriesKey, seriesAttr);
                    SetSeries(seriesKey);
                }
            }

            string id = KeyFamily.TimeDimension.Concept.Id;
            var time = new KeyValuePair<string, string>(id, validatedRecord[id]);
            id = KeyFamily.PrimaryMeasure.Concept.Id;
            var obs = new KeyValuePair<string, string>(id, validatedRecord[id]);
            var obsAttr = new Dictionary<string, string>();
            AddAttibutes(validatedRecord, obsAttr, a => a.AttachementLevel == AttachmentLevel.Observation);
            WriteObs(time, obs, obsAttr);
        }

        protected abstract void WriteRoot();
        protected abstract void WriteDataSet(Dictionary<string, string> attributes);
        protected abstract void CloseDataSet();
        protected abstract void WriteGroup(Group group, Dictionary<string, string> key, Dictionary<string, string> attributes);
        protected abstract void CloseGroup();
        protected abstract void WriteSeries(Dictionary<string, string> key, Dictionary<string, string> attributes);
        protected abstract void CloseSeries();
        protected abstract void WriteObs(KeyValuePair<string, string> time, KeyValuePair<string, string> obs, Dictionary<string, string> attributes);


        #region IDisposable
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

        ~DataWriter()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    CloseDataSet();
                    ((IDisposable)XmlWriter).Dispose();
                }
                catch { }
            }

            _disposed = true;
        }
        #endregion

        protected void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException("DataWriter");
        }

        KeyValuePair<string, Dictionary<string, string>> GetPrevGroup()
        {
            return prevGroup;
        }

        void SetPrevGroup(string groupName, Dictionary<string, string> values)
        {
            prevGroup = new KeyValuePair<string, Dictionary<string, string>>(groupName, values);
        }

        Dictionary<string, string> GetPrevSeries()
        {
            return prevSeries;
        }

        void SetSeries(Dictionary<string, string> values)
        {
            prevSeries = values;
        }

        bool IsGroupEqual(KeyValuePair<string, Dictionary<string, string>> group1, string groupName2, Dictionary<string, string> groupValues2)
        {
            string groupName1 = group1.Key;
            var groupValues1 = group1.Value;

            if (groupName1 != groupName2)
                return false;

            return IsDictEqual(groupValues1, groupValues2);
        }

        bool IsDictEqual(Dictionary<string, string> dict1, Dictionary<string, string> dict2)
        {
            if (dict1.Count != dict2.Count)
                return false;

            foreach (var item1 in dict1)
            {
                string value2 = null;
                if (!dict2.TryGetValue(item1.Key, out value2))
                    return false;

                if (!item1.Value.Equals(value2))
                    return false;
            }

            return true;
        }

        string RecordToString(Dictionary<string, object> record)
        {
            var list = new List<string>();
            record.ForEach(i => list.Add(string.Format("{0}='{1}'", i.Key, i.Value.ToString())));
            return string.Join(",", list.ToArray());
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

        void AddAttibutes(Dictionary<string, string> values, Dictionary<string, string> attributes, Func<Attribute, bool> predicate)
        {
            foreach (var attr in KeyFamily.Attributes.Where(predicate))
            {
                string name = attr.Concept.Id;
                attributes.Add(name, values[name]);
            }
        }

        Dictionary<string, object> GetDict(IDataRecord record)
        {
            var dict = new Dictionary<string, object>();

            for (int i = 0; i < record.FieldCount; i++)
            {
                dict.Add(record.GetName(i), record[i]);
            }
            return dict;
        }
        
        bool HasValues(Dictionary<string, string> dict)
        {
            foreach (var value in dict.Values)
            {
                if (value != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
