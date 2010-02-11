using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SDMX.Parsers;
using System.Xml;
using System.Xml.Linq;

namespace SDMX.Tests
{
    [TestFixture]
    public class GenericDataSetMapTests
    {
        [Test]
        public void Can_read_SDMX()
        {
            string dataPath = Utility.GetPathFromProjectBase("lib\\GenericSample2.xml");
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            var map = new DataMessageMap(keyFamily);

            DataMessage message;
            using (var reader = XmlReader.Create(dataPath))
            {
                message = map.ReadXml(reader);
            }

            var output = new StringBuilder();
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(output, settings))
            {
                map.WriteXml(writer, message);
            }

            var doc = XDocument.Parse(output.ToString());
            doc.Save(Utility.GetPathFromProjectBase("lib\\GenericSample2Test.xml"));
            Assert.IsTrue(Utility.ValidateMessage(doc));
        }
    }
}
