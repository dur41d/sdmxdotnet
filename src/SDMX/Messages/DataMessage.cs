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
            var map = new DataMessageMap();
            map.KeyFamily = keyFamily;
            return Read(reader, keyFamily, map);
        }

        public static DataMessage LoadGeneric(string fileName, KeyFamily keyFamily)
        {
            return ExecuteReader(fileName, reader => ReadGeneric(reader, keyFamily));
        }
      
        public static DataMessage ReadCompact(XmlReader reader, KeyFamily keyFamily, string targetNamespace)
        {
            var map = new CompactDataMessageMap("s", targetNamespace);
            map.KeyFamily = keyFamily;            
            return Read(reader, keyFamily, map);
        }

        public static DataMessage LoadCompact(string fileName, KeyFamily keyFamily, string targetNamespace)
        {
            return ExecuteReader(fileName, reader => ReadCompact(reader, keyFamily, targetNamespace));
        }      

        public void WriteGeneric(XmlWriter writer)
        {
            var map = new DataMessageMap();
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
            ExecuteWriter(fileName, writer => WriteGeneric(writer));
        }

        public void SaveCompact(string fileName, string prefix, string targetNamespace)
        {
            ExecuteWriter(fileName, writer => WriteCompact(writer, prefix, targetNamespace));
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

        private static DataMessage ExecuteReader(string fileName, Func<XmlReader, DataMessage> read)
        {
            Contract.AssertNotNullOrEmpty(fileName, "fileName");
            DataMessage message;
            using (var reader = XmlReader.Create(fileName))
            {
                message = read(reader);
            }
            return message;
        }

        private void ExecuteWriter(string fileName, Action<XmlWriter> write)
        {
            Contract.AssertNotNullOrEmpty(fileName, "fileName");
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(fileName, settings))
            {
                write(writer);
            }
        }

    }
}
