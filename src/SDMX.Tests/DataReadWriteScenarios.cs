using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SDMX.Tests
{
    [TestFixture]
    public class DataReadWriteScenarios : TestBase
    {
        [Test]
        [Ignore]
        public void read_file_into_db_quit_if_file_not_valid()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            var keyFamily = structure.KeyFamilies[0];

            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                reader.Map("TIME", "TIME", i => i.ToString());

                using (var bulkCopy = new SqlBulkCopy(_connectionString))
                {
                    bulkCopy.DestinationTableName = "DestinationTableName";
                    bulkCopy.WriteToServer(reader);
                }
            }
        }

        [Test]
        [Ignore]
        public void read_file_into_db_insert_even_if_not_valid()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            var keyFamily = structure.KeyFamilies[0];

            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                reader.Map("TIME", "TIME", i => i.ToString());
                reader.ThrowExceptionIfNotValid = false;

                using (var bulkCopy = new SqlBulkCopy(_connectionString))
                {
                    bulkCopy.DestinationTableName = "DestinationTableName";
                    bulkCopy.WriteToServer(reader);
                }
            }
        }

        [Test]
        [Ignore]
        public void read_file_into_db_insert_only_valid()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            var keyFamily = structure.KeyFamilies[0];

            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                reader.Map("TIME", "TIME", i => i.ToString());
                reader.ThrowExceptionIfNotValid = false;

                var table = ((IDataReader)reader).GetSchemaTable();

                CreateTable(table);

                while (reader.Read())
                {
                    if (reader.IsValid)
                    { 
                        // insert
                    }
                }
            }
        }

        [Test]
        [Ignore]
        public void read_from_database_into_file_quit_if_notValid()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            var keyFamily = structure.KeyFamilies[0];

            IDataReader reader = null;
            using (reader)
            using (var writer = new CompactDataWriter("path", keyFamily, "uis", "ns"))
            {
                writer.WriteHeader(null);
                writer.Write(reader);
            }
        }

        [Test]
        [Ignore]
        public void read_from_database_into_file_stop_writing_but_continue_validating()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            var keyFamily = structure.KeyFamilies[0];

            IDataReader reader = null;
            bool valid = true;
            using (reader)
            using (var writer = new CompactDataWriter("path", keyFamily, "uis", "ns"))
            {
                writer.WriteHeader(null);

                while (reader.Read())
                { 
                    Dictionary<string, string> record = null;
                    var errors = writer.ValidateRecord(reader, out record);

                    if (errors.Count > 0)
                    {
                        valid = false;
                        // display errors
                    }

                    if (valid)
                    {
                        writer.WriteRecord(record);
                    }
                }
            }
        }
        
        [Test]
        [Ignore]
        public void convert_format_from_file_to_file()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            var keyFamily = structure.KeyFamilies[0];
                        
            using (var reader = DataReader.Create(dataPath, keyFamily))
            using (var writer = new CompactDataWriter("path", keyFamily, "uis", "ns"))
            {
                writer.Write(reader);
            }
        }

        TimePeriod ParseTimePeriod(string i)
        {
            string[] s = i.Split('-');
            int year = int.Parse(s[0]);
            int month = int.Parse(s[1]);
            var dateTime = new DateTimeOffset(year, month, 0, 0, 0, 0, TimeSpan.Zero);
            return TimePeriod.FromYearMonth(dateTime);
        }
    }
}
