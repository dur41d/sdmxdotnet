using System;
using System.Data;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;

namespace SDMX.Tests
{
    [TestFixture]
    public class DataWriterTests
    {
        const string _connectionString = @"Server=.;Database=sdmx;Integrated Security=False; User id=dev;Password=dev";

        [Test]
        [Ignore]
        public void WriteCompact_FromDB()
        {
            var dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var structure = StructureMessage.Load(dsdPath);
            var keyFamily = structure.KeyFamilies[0];
            
            string targetNameSpace = "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=BIS:EXT_DEBT:compact";

            string dataPath = Utility.GetPath("lib\\CompactSample22.xml");
            using (var reader = GetReader())
            {
                var settings = new XmlWriterSettings() { Indent = true };
                
                using (var writer = new CompactDataWriter(dataPath, keyFamily, settings, "bisc", targetNameSpace))
                {
                    writer.WriteHeader(GetHeader());
                    writer.Write(reader);
                }
            }

            string compactSchema = Utility.GetPath("lib\\BIS_JOINT_DEBT_Compact.xsd");
            Assert.IsTrue(Utility.IsValidMessage(XDocument.Load(dataPath), Utility.GetComapctSchema(dsdPath, targetNameSpace)));
        }

        [Test]
        [Ignore]
        public void WriteGeneric_FromDB()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            var keyFamily = structure.KeyFamilies[0];

            string path = Utility.GetPath("lib\\GenericSample22.xml");
            using (var reader = GetReader())
            {
                var settings = new XmlWriterSettings() { Indent = true };
                using (var writer = new GenericDataWriter(path, keyFamily, settings))
                {
                    writer.WriteHeader(GetHeader());
                    writer.Write(reader);
                }
            }

            Assert.IsTrue(MessageValidator.ValidateXml(path, s => Debug.WriteLine(s), s => Debug.WriteLine(s)));
        }


        IDataReader GetReader()
        {
            var connection = new SqlConnection(_connectionString);
            var com = new SqlCommand("select * from BIS_JOINT_DEBT", connection);            
            connection.Open();
            return com.ExecuteReader(CommandBehavior.CloseConnection);
        }

        Header GetHeader()
        {
            return new Header("JD014", new Party("BIS"), DateTime.Now);
        }
    }
}
