using System;
using System.Data;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SDMX.Tests
{
    [TestFixture]
    public class DataWriterTests
    {
        [Test]
        [Ignore]
        public void DataWriterDesign()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            var keyFamily = structure.KeyFamilies[0];

            // Get IDataReader instance from the database or anywhere else
            using (IDataReader reader = GetReader())
            {
                using (var writer = new CompactDataWriter("destination_filename.xml", keyFamily))
                {
                    // Cast back from DateTime (if its strored in the database like that) to TimePeriod.Year
                    writer.Cast("TIME", i => TimePeriod.FromYear((DateTime)i));

                    writer.Write(reader);
                }
            }
        }

        IDataReader GetReader()
        {
            // get reader form database or sdmx file or anywhere
            throw new NotImplementedException();
        }
    }

    class CompactDataWriter : IDisposable
    {
        public CompactDataWriter(string fileName, KeyFamily keyFamily)
        { }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void Cast(string name, Func<object, object> cast)
        {
            throw new NotImplementedException();
        }

        public void Write(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
