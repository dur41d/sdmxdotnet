using System;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SDMX
{
    public class CompactDataWriter : DataWriter
    {
        const string _compactPrefix = "c";
        const string _compactNameSpace = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/compact";
        string _prefix = null;
        string _targetNamespace = null;

        #region Constructors
        public CompactDataWriter(string path, KeyFamily keyFamily, string prefix, string targetNamespace)
            : base(path, keyFamily)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(Stream stream, KeyFamily keyFamily, string prefix, string targetNamespace)
            : base(stream, keyFamily)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(XmlWriter writer, KeyFamily keyFamily, string prefix, string targetNamespace)
            : base(writer, keyFamily)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(StringBuilder stringBuilder, KeyFamily keyFamily, string prefix, string targetNamespace)
            : base(stringBuilder, keyFamily)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(TextWriter textWriter, KeyFamily keyFamily, string prefix, string targetNamespace)
            : base(textWriter, keyFamily)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(string path, KeyFamily keyFamily, XmlWriterSettings settings, string prefix, string targetNamespace)
            : base(path, keyFamily, settings)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(Stream stream, KeyFamily keyFamily, XmlWriterSettings settings, string prefix, string targetNamespace)
            : base(stream, keyFamily, settings)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(StringBuilder stringBuilder, KeyFamily keyFamily, XmlWriterSettings settings, string prefix, string targetNamespace)
            : base(stringBuilder, keyFamily, settings)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }

        public CompactDataWriter(TextWriter textWriter, KeyFamily keyFamily, XmlWriterSettings settings, string prefix, string targetNamespace)
            : base(textWriter, keyFamily, settings)
        {
            _prefix = prefix;
            _targetNamespace = targetNamespace;
        }
        #endregion Constructors
        
        protected override void WriteRoot()
        {
            XmlWriter.WriteStartDocument();
            XmlWriter.WriteStartElement("CompactData", "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message");            
            XmlWriter.WriteAttributeString("xmlns", _compactPrefix, null, _compactNameSpace);            
            XmlWriter.WriteAttributeString("xmlns", _prefix, null, _targetNamespace);
            XmlWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
        }

        protected override void WriteDataSet(System.Collections.Generic.Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement(_prefix, "DataSet", _targetNamespace);
            WriteAttributes(attributes);
        }

        protected override void CloseDataSet()
        {
            XmlWriter.WriteEndDocument();
        }

        protected override void WriteGroup(Group group, Dictionary<string, string> key, Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement(_prefix, group.Id, _targetNamespace);
            WriteAttributes(key);
            WriteAttributes(attributes);
            XmlWriter.WriteEndElement();
        }

        protected override void CloseGroup()
        { }

        protected override void WriteSeries(Dictionary<string, string> key, Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement(_prefix, "Series", _targetNamespace);
            WriteAttributes(key);
            WriteAttributes(attributes);
        }

        protected override void CloseSeries()
        {
            XmlWriter.WriteEndElement();
        }

        protected override void WriteObs(KeyValuePair<string, string> time, KeyValuePair<string, string> obs, Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement(_prefix, "Obs", _targetNamespace);
            WriteAttribute(time);
            WriteAttribute(obs);
            WriteAttributes(attributes);
            XmlWriter.WriteEndElement();
        }

        string GetStartTimeName(string name)
        {
            return string.Format("{0}:StartTime", name);
        }

        void WriteAttributes(Dictionary<string, string> dict)
        {
            foreach (var item in dict)
            {
                WriteAttribute(item);
            }
        }

        void WriteAttribute(KeyValuePair<string, string> item)
        {
            if (item.Value != null)
            {
                XmlWriter.WriteAttributeString(item.Key, item.Value);
            }
        }
    }
}
