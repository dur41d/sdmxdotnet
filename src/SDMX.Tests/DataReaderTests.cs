using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using System.Xml.Linq;
using System.Xml;
using SDMX.Parsers;

namespace SDMX.Tests
{
    [TestFixture]
    public class DataReaderTests
    {
        [Test]
        public void ReadCompact()
        {
            string dataPath = Utility.GetPath("lib\\CompactSample.xml");
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                while (reader.Read())
                {
                    if (reader.Value is Header)
                    {
                        var header = (Header)reader.Value;
                        Console.WriteLine(header.ID);
                    }
                    else
                    {
                        foreach (var item in reader.Items)
                            Console.Write("{0}={1},", item.Key, item.Value);
                        Console.WriteLine();
                    }
                }
            }
        }

        [Test]
        public void ReadGeneric()
        {
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                while (reader.Read())
                {
                    if (reader.Value is Header)
                    {
                        var header = (Header)reader.Value;
                        Console.WriteLine(header.ID);
                    }
                    else
                    {
                        foreach (var item in reader.Items)
                            Console.Write("{0}={1},", item.Key, item.Value);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
