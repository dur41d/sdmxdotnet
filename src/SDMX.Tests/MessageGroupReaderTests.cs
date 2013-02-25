using System;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class MessageGroupReaderTests
    {
        [Test]
        public void Read()
        {
            string dataPath = Utility.GetPath("lib\\MessageGroupSample.xml");

            string dsdPath = Utility.GetPath("lib\\EduDSD.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            int counter = 0;           
            using (var reader = new MessageGroupReader(dataPath, keyFamily))
            {
                var header = reader.ReadHeader();

                Assert.IsNotNull(header);

                while (reader.Read())
                {
                    Assert.AreEqual(9, reader.Count());
                    //foreach (var item in reader)
                    //    Console.Write("{0}={1},", item.Key, item.Value);
                    //Console.WriteLine();
                    counter++;
                }
            }

            Assert.AreEqual(8, counter);
        }

        [Test]
        public void Read2()
        {
            string dataPath = Utility.GetPath("lib\\MessageGroupSample2.xml");

            string dsdPath = Utility.GetPath("lib\\EduDSD.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            int counter = 0;
            using (var reader = new MessageGroupReader(dataPath, keyFamily))
            {
                var header = reader.ReadHeader();

                Assert.IsNotNull(header);

                while (reader.Read())
                {
                    Assert.AreEqual(9, reader.Count());
                    //foreach (var item in reader)
                    //    Console.Write("{0}={1},", item.Key, item.Value);
                    //Console.WriteLine();
                    counter++;
                }
            }

            Assert.AreEqual(16, counter);
        }

        [Test]
        public void Read3()
        {
            string dataPath = Utility.GetPath("lib\\MessageGroupSample3.xml");

            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            int counter = 0;
            using (var reader = new MessageGroupReader(dataPath, keyFamily))
            {
                var header = reader.ReadHeader();

                Assert.IsNotNull(header);

                while (reader.Read())
                {
                    Assert.AreEqual(17, reader.Count());
                    //foreach (var item in reader)
                    //    Console.Write("{0}={1},", item.Key, item.Value);
                    //Console.WriteLine();
                    counter++;
                }
            }

            Assert.AreEqual(13, counter);
        }
    }
}
