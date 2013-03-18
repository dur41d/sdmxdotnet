using System.IO;
using NUnit.Framework;
using System;

namespace SDMX.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void ValidateXml()
        {
            Action<string, System.Xml.Schema.XmlSchemaException> action = (m, e) => Console.WriteLine(m + e.LineNumber);
            
            string path = Utility.GetPath("lib\\StructureSample.xml");
            var fileStream = File.OpenRead(path);

            Assert.IsTrue(MessageValidator.ValidateXml(fileStream, action, action));

            path = Utility.GetPath("lib\\QuerySample.xml");
            fileStream = File.OpenRead(path);
            
            Assert.IsTrue(MessageValidator.ValidateXml(fileStream, action, action));
        }
    }
}
