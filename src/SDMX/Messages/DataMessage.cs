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

        public static DataMessage ReadXml(XmlReader reader, KeyFamily keyFamily, DataFormat format)
        {
            Contract.AssertNotNull(reader, "reader");
            Contract.AssertNotNull(keyFamily, "keyFamily");

            var map = GetMessageMap(format, keyFamily);

            return map.ReadXml(reader);
        }

        public static DataMessage Load(string fileName, KeyFamily keyFamily, DataFormat format)
        {
            DataMessage message;
            using (var reader = XmlReader.Create(fileName))
            {
                message = ReadXml(reader, keyFamily, format);
            }

            return message;
        }

        public void WriteXml(XmlWriter writer, DataFormat format)
        {
            Contract.AssertNotNull(writer, "writer");

            var keyFamily = DataSet == null ? null : DataSet.KeyFamily;
            var map = GetMessageMap(format, keyFamily);
            map.WriteXml(writer, this);
        }

        public void Save(string fileName, DataFormat format)
        {
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(fileName, settings))
            {
                WriteXml(writer, format);
            }
        }

        private static RoolElementMap<DataMessage> GetMessageMap(DataFormat format, KeyFamily keyFamily)
        {
            switch (format)
            {
                case DataFormat.Generic:
                    return new DataMessageMap()
                     {
                         KeyFamily = keyFamily
                     };
                case DataFormat.Compact:
                    return new CompactDataMessageMap("bisc", "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=BIS:EXT_DEBT:compact")
                        {
                            KeyFamily = keyFamily
                        };
                default:
                    throw new SDMXException("Invalid data format: {0}.", format);
            }
        }
    }
}
