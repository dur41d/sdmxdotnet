using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using SDMX.Parsers;
using System.Xml;
using System.IO;

namespace SDMX
{
    public class QueryMessage : Message
    {
        public Query Query { get; set; }

        public static QueryMessage ReadXml(XmlReader reader)
        {
            var map = new QueryMessageMap();

            return map.ReadXml(reader);
        }

        public static QueryMessage Load(string fileName)
        {
            QueryMessage message;
            using (var reader = XmlReader.Create(fileName))
            {
                message = ReadXml(reader);
            }

            return message;
        }

        public void WriteXml(XmlWriter writer)
        {
            Contract.AssertNotNull(writer, "writer");
            var map = new QueryMessageMap();
            map.WriteXml(writer, this);
        }

        public void Save(string fileName)
        {
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(fileName, settings))
            {
                WriteXml(writer);
            }
        }
    }
}
