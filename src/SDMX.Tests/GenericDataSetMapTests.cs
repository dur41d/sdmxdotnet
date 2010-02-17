using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SDMX.Parsers;
using System.Xml;
using System.Xml.Linq;
using System.IO;

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

            var message = DataMessage.Load(dataPath, keyFamily, DataFormat.Generic);

            var sb = new StringBuilder();
                        
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                message.WriteXml(writer, DataFormat.Generic);
            }

            var doc = XDocument.Parse(sb.ToString());            
            Assert.IsTrue(Utility.IsValidMessage(doc));
        }
    }
}
