using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

namespace SDMX
{
    public class GenericDataWriter : DataWriter
    {
        const string _genericPrefix = "g";
        const string _genericNameSpace = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/generic";
        #region Constructors
        public GenericDataWriter(string path, KeyFamily keyFamily)
            : base(path, keyFamily)
        { }

        public GenericDataWriter(Stream stream, KeyFamily keyFamily)
            : base(stream, keyFamily)
        { }

        public GenericDataWriter(XmlWriter writer, KeyFamily keyFamily)
            : base(writer, keyFamily)
        { }

        public GenericDataWriter(StringBuilder stringBuilder, KeyFamily keyFamily)
            : base(stringBuilder, keyFamily)
        { }

        public GenericDataWriter(TextWriter textWriter, KeyFamily keyFamily)
            : base(textWriter, keyFamily)
        { }

        public GenericDataWriter(string path, KeyFamily keyFamily, XmlWriterSettings settings)
            : base(path, keyFamily, settings)
        { }

        public GenericDataWriter(Stream stream, KeyFamily keyFamily, XmlWriterSettings settings)
            : base(stream, keyFamily, settings)
        { }

        public GenericDataWriter(StringBuilder stringBuilder, KeyFamily keyFamily, XmlWriterSettings settings)
            : base(stringBuilder, keyFamily, settings)
        { }

        public GenericDataWriter(TextWriter textWriter, KeyFamily keyFamily, XmlWriterSettings settings)
            : base(textWriter, keyFamily, settings)
        { }
        #endregion Constructors
        

        protected override void WriteRoot()
        {
            XmlWriter.WriteStartDocument();
            XmlWriter.WriteStartElement("GenericData", "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message");
            XmlWriter.WriteAttributeString("xmlns", _genericPrefix, null, _genericNameSpace);
            XmlWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
        }

        protected override void WriteDataSet(Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement("DataSet");
            XmlWriter.WriteStartElement(_genericPrefix, "KeyFamilyRef", _genericNameSpace);
            XmlWriter.WriteString(KeyFamily.Id);
            XmlWriter.WriteEndElement();
            WriteValues(attributes, "Attributes");
        }

        protected override void CloseDataSet()
        {
            XmlWriter.WriteEndDocument();
        }

        protected override void WriteGroup(Group group, Dictionary<string, string> key, Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement(_genericPrefix, "Group", _genericNameSpace);
            XmlWriter.WriteAttributeString("type", group.Id);
            WriteValues(key, "GroupKey");
            WriteValues(attributes, "Attributes");
        }

        protected override void CloseGroup()
        {
            XmlWriter.WriteEndElement();
        }

        protected override void WriteSeries(Dictionary<string, string> key, Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement(_genericPrefix, "Series", _genericNameSpace);
            WriteValues(key, "SeriesKey");
            WriteValues(attributes, "Attributes");
        }

        protected override void CloseSeries()
        {
            XmlWriter.WriteEndElement();
        }

        protected override void WriteObs(KeyValuePair<string, string> time, KeyValuePair<string, string> obs, Dictionary<string, string> attributes)
        {
            XmlWriter.WriteStartElement(_genericPrefix, "Obs", _genericNameSpace);

            XmlWriter.WriteStartElement(_genericPrefix, "Time", _genericNameSpace);
            XmlWriter.WriteString(time.Value);
            XmlWriter.WriteEndElement();

            XmlWriter.WriteStartElement(_genericPrefix, "ObsValue", _genericNameSpace);
            XmlWriter.WriteAttributeString("value", obs.Value);
            XmlWriter.WriteEndElement();

            WriteValues(attributes, "Attributes");

            XmlWriter.WriteEndElement();
        }

        void WriteValues(Dictionary<string, string> dict, string name)
        {
            if (dict.Count == 0)
                return;

            XmlWriter.WriteStartElement(_genericPrefix, name, _genericNameSpace);
            foreach (var item in dict)
            {
                if (item.Value != null)
                {
                    XmlWriter.WriteStartElement(_genericPrefix, "Value", _genericNameSpace);
                    XmlWriter.WriteAttributeString("concept", item.Key);
                    XmlWriter.WriteAttributeString("value", item.Value);
                    XmlWriter.WriteEndElement();
                }
            }
            XmlWriter.WriteEndElement();
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
