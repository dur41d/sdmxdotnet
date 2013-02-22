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
            string path = Utility.GetPath("lib\\StructureSample.xml");
            var fileStream = File.OpenRead(path);
            Assert.IsTrue(MessageValidator.ValidateXml(fileStream, null, null));

            path = Utility.GetPath("lib\\QuerySample.xml");
            fileStream = File.OpenRead(path);
            Assert.IsTrue(MessageValidator.ValidateXml(fileStream, w => Console.WriteLine(w), e => Console.WriteLine(e)));
        }
    }
}
