using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Common;
using OXM;
using SDMX.Parsers;

namespace SDMX
{
    /// <summary>
    /// Base class to provide shared functionality to message types.
    /// It was preferable to use extension methods to provide shared functionality but
    /// because there are static methods this was not possible.
    /// </summary>
    public abstract class MessageBase<T> : IMessage where T : class
    { 
        public Header Header { get; set; }

        private static Dictionary<Type, Func<object>> factory = new Dictionary<Type, Func<object>>()
            {
                { typeof(StructureMessage), () => new StructureMessageMap() },
                { typeof(QueryMessage), () => new QueryMessageMap() },
            };

        public override string ToString()
        {
            return Header.ToString();
        }

        public static T Read(string input)
        {
            Contract.AssertNotNullOrEmpty(input, "input");
            using (var reader = XmlReader.Create(new StringReader(input)))
            {
                return Read(reader);
            }
        }

        public static T Read(Stream stream)
        {
            Contract.AssertNotNull(stream, "stream");
            using (var reader = XmlReader.Create(stream))
            {
                return Read(reader);
            }
        }

        public static T Read(XmlReader reader)
        {
            Contract.AssertNotNull(reader, "reader");
            var map = factory[typeof(T)]() as RootElementMap<T>;

            return map.ReadXml(reader);
        }

        public static T Load(string fileName)
        {
            T message;
            using (var reader = XmlReader.Create(fileName))
            {
                message = Read(reader);
            }

            return message;
        }

        public void Write(StringBuilder sb)
        {
            Contract.AssertNotNull(sb, "sb");
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                Write(writer);
            }
        }

        public void Write(Stream stream)
        {
            Contract.AssertNotNull(stream, "stream");
            using (var writer = XmlWriter.Create(stream))
            {
                Write(writer);
            }
        }

        public void Write(XmlWriter writer)
        {
            Contract.AssertNotNull(writer, "writer");
            var map = factory[typeof(T)]() as RootElementMap<T>;
            map.WriteXml(writer, this as T);
        }

        public void Save(string fileName)
        {
            Contract.AssertNotNullOrEmpty(fileName, "fileName");

            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(fileName, settings))
            {
                Write(writer);
            }
        }
    }
}
