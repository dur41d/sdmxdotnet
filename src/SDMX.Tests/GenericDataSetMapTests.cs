using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SDMX.Parsers;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Xml.Xsl;

namespace SDMX.Tests
{
    [TestFixture]
    public class GenericDataSetMapTests
    {
        [Test]
        public void Can_read_SDMX()
        {
            string dataPath = Utility.GetPath("lib\\GenericSample2.xml");
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            var message = DataMessage.LoadGeneric(dataPath, keyFamily);

            var sb = new StringBuilder();
                        
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                message.WriteGeneric(writer);
            }

            var doc = XDocument.Parse(sb.ToString());            
            Assert.IsTrue(Utility.IsValidMessage(doc));
        }

        [Test]
        public void Can_read_compact_data()
        {
            string dataPath = Utility.GetPath("lib\\CompactSampleNoGroups.xml");
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            string targetNamespace = "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=BIS:EXT_DEBT:compact";

            var message = DataMessage.LoadCompact(dataPath, keyFamily, targetNamespace);

            var doc = new XDocument();            
            using (var writer = doc.CreateWriter())
            {
                message.WriteCompact(writer, "uis", targetNamespace);
            }
            
            var schema = Utility.GetComapctSchema("lib\\StructureSample.xml", targetNamespace);
            Assert.IsTrue(Utility.IsValidMessage(doc, schema));
        }
    }
}
