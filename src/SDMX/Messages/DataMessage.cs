using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SDMX.Parsers;
using Common;
using OXM;

namespace SDMX
{
    public enum DataFormat
    {
        Generic,
        Compact,
        Utility
    }

    public class DataMessage : Message
    {
        public DataSet DataSet { get; set; }

        public static DataMessage ReadGeneric(XmlReader reader, KeyFamily keyFamily)
        {
            var map = new GenericDataMessageMap();
            map.KeyFamily = keyFamily;
            return Read(reader, keyFamily, map);
        }

        public static DataMessage LoadGeneric(string fileName, KeyFamily keyFamily)
        {            
            DataMessage message = null;
            CreateReader(fileName).Do(reader => message = ReadGeneric(reader, keyFamily));
            return message;
        }
      
        public static DataMessage ReadCompact(XmlReader reader, KeyFamily keyFamily, string targetNamespace)
        {
            var map = new CompactDataMessageMap("s", targetNamespace);
            map.KeyFamily = keyFamily;            
            return Read(reader, keyFamily, map);
        }

        public static DataMessage LoadCompact(string fileName, KeyFamily keyFamily, string targetNamespace)
        {
            DataMessage message = null;
            CreateReader(fileName).Do(reader => message = ReadCompact(reader, keyFamily, targetNamespace));
            return message;
        }      

        public void WriteGeneric(XmlWriter writer)
        {
            var map = new GenericDataMessageMap();
            if (DataSet != null)
            {
                map.KeyFamily = DataSet.KeyFamily;
            }
            Write(writer, map);
        }

        public void WriteCompact(XmlWriter writer, string prefix, string targetNamespace)
        {
            Contract.AssertNotNullOrEmpty(prefix, "prefix");
            Contract.AssertNotNullOrEmpty(targetNamespace, "targetNamespace");
            var map = new CompactDataMessageMap(prefix, targetNamespace);
            if (DataSet != null)
            {
                map.KeyFamily = DataSet.KeyFamily;
            }
            Write(writer, map);
        }

        public void SaveGeneric(string fileName)
        {
            CreateWriter(fileName).Do(writer => WriteGeneric(writer));
        }

        public void SaveCompact(string fileName, string prefix, string targetNamespace)
        {
            CreateWriter(fileName).Do(writer => WriteCompact(writer, prefix, targetNamespace));
        }

        private static DataMessage Read(XmlReader reader, KeyFamily keyFamily, RoolElementMap<DataMessage> map)
        {
            Contract.AssertNotNull(reader, "reader");
            Contract.AssertNotNull(keyFamily, "keyFamily");

            return map.ReadXml(reader);
        }

        private void Write(XmlWriter writer, RoolElementMap<DataMessage> map)
        {
            Contract.AssertNotNull(writer, "writer");
            map.WriteXml(writer, this);
        }

        private static IEnumerable<XmlReader> CreateReader(string fileName)
        {
            Contract.AssertNotNullOrEmpty(fileName, "fileName");            
            using (var reader = XmlReader.Create(fileName))
            {
                yield return reader;
            }
        }

        private IEnumerable<XmlWriter> CreateWriter(string fileName)
        {
            Contract.AssertNotNullOrEmpty(fileName, "fileName");
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(fileName, settings))
            {
                yield return writer;
            }
        }

    }
}
