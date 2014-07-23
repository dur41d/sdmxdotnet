using System;
using System.Data;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

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

        [Test]
        public void compact_read_write_read()
        {
            string targetNameSpace = "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=BIS:EXT_DEBT:compact";
            Func<KeyFamily, XmlWriter, DataWriter> createWriter = (keyFamily, xmlWriter) => new CompactDataWriter(xmlWriter, keyFamily, "bisc", targetNameSpace);
            test_read_write_read("lib\\CompactSample.xml", createWriter);
        }

        [Test]
        public void generic_read_write_read()
        {
            Func<KeyFamily, XmlWriter, DataWriter> createWriter = (keyFamily, xmlWriter) => new GenericDataWriter(xmlWriter, keyFamily);
            test_read_write_read("lib\\GenericSample.xml", createWriter);
        }

        [Test]
        public void compact_load_test()
        {
            string targetNameSpace = "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=BIS:EXT_DEBT:compact";
            Func<KeyFamily, XmlWriter, DataWriter> createWriter = (keyFamily, xmlWriter) => new CompactDataWriter(xmlWriter, keyFamily, "bisc", targetNameSpace);
            LoadTest(createWriter);
        }

        [Test]
        public void generic_load_test()
        {
            Func<KeyFamily, XmlWriter, DataWriter> createWriter = (keyFamily, xmlWriter) => new GenericDataWriter(xmlWriter, keyFamily);
            LoadTest(createWriter);
        }

        public void LoadTest(Func<KeyFamily, XmlWriter, DataWriter> createWriter)
        {
            var list = new List<string>() 
                { 
                    //"aei_ps_alt.sdmx.zip",
                    //"apri_ap_him.sdmx.zip",
                    //"apro_cpb_cerea.sdmx.zip", // has errors
                    //"apro_cpp_crop.sdmx.zip",  // has errors
                    //"avia_ac_fatal.sdmx.zip",
                    //"avia_ac_number.sdmx.zip",
                    //"avia_ec_enterp.sdmx.zip",
                    //"avia_paexcc.sdmx.zip", // takes long time
                    //"avia_tf_alc.sdmx.zip"
                };

            ZipFile zf = null;

            Stream dsdStream = null;
            Stream dataStream = null;
            foreach (var item in list)
            {
                using (FileStream fs = File.OpenRead(Utility.GetPath("lib\\" + item)))
                {
                    zf = new ZipFile(fs);

                    foreach (ZipEntry zipEntry in zf)
                    {
                        if (zipEntry.Name.Contains("dsd"))
                            dsdStream = zf.GetInputStream(zipEntry);
                        else
                            dataStream = zf.GetInputStream(zipEntry);
                    }

                    Console.WriteLine(item);
                    Insert(dataStream, dsdStream, createWriter);
                }
            }
        }

        void Insert(Stream dataStream, Stream dsdStream, Func<KeyFamily, XmlWriter, DataWriter> createWriter)
        {
            var doc = XDocument.Load(dsdStream);
            doc.Descendants().Where(i => i.Name.LocalName == "TextFormat" && i.Parent.Name.LocalName == "TimeDimension").Single().Remove();

            var mngr = new XmlNamespaceManager(new NameTable());
            mngr.AddNamespace("structure", "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/structure");
            mngr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instanc");
            mngr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");

            XElement xElementParse = ParseElement("<structure:Concept id='TIME_FORMAT'><structure:Name xml:lang='en'>Time format</structure:Name></structure:Concept>", mngr);
            doc.Descendants().Where(i => i.Name.LocalName == "ConceptScheme").Single().Add(xElementParse);

            using (var xReader = doc.CreateReader())
            {
                var structure = StructureMessage.Read(xReader);
                var keyFamily = structure.KeyFamilies[0];

                var data1 = new Dictionary<string, string>();
                var data2 = new Dictionary<string, string>();

                var settings = new XmlWriterSettings() { Indent = true };

                var dataDoc = new XDocument();
                using (var reader = DataReader.Create(dataStream, keyFamily))
                using (var writer = createWriter(keyFamily, dataDoc.CreateWriter()))
                //using (var writer = new CompactDataWriter(Utility.GetPath("lib\\aei_ps_alt.sdmx\\aei_ps_alt.sdmx2.xml"), keyFamily, settings, "data", "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=EUROSTAT:aei_ps_alt_DSD:compact"))
                {
                    writer.WriteHeader(GetHeader());
                    while (reader.Read())
                    {
                        data1.Add(GetData(reader), null);
                        writer.WriteRecord(reader);
                    }
                }

                using (var reader = DataReader.Create(dataDoc.CreateReader(), keyFamily))
                {
                    while (reader.Read())
                    {
                        data2.Add(GetData(reader), null);
                    }
                }

                Assert.IsTrue(CompareData(data1, data2));
            }
        }

        public static XElement ParseElement(string strXml, XmlNamespaceManager mngr)
        {
            XmlParserContext parserContext = new XmlParserContext(null, mngr, null, XmlSpace.None);
            XmlTextReader txtReader = new XmlTextReader(strXml, XmlNodeType.Element, parserContext);
            return XElement.Load(txtReader);
        }

        void test_read_write_read(string dataFile, Func<KeyFamily, XmlWriter, DataWriter> createWriter)
        {
            var dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var structure = StructureMessage.Load(dsdPath);
            var keyFamily = structure.KeyFamilies[0];
            string dataPath = Utility.GetPath(dataFile);

            var data1 = new Dictionary<string, string>();
            var data2 = new Dictionary<string, string>();

            var doc = new XDocument();
            using (var reader = DataReader.Create(dataPath, keyFamily))
            using (var writer = createWriter(keyFamily, doc.CreateWriter()))
            {
                writer.WriteHeader(GetHeader());
                while (reader.Read())
                {
                    data1.Add(GetData(reader), null);
                    writer.WriteRecord(reader);
                }
            }

            using (var reader = DataReader.Create(doc.CreateReader(), keyFamily))
            {
                while (reader.Read())
                {
                    data2.Add(GetData(reader), null);
                }
            }

            Assert.IsTrue(CompareData(data1, data2));
        }

        string GetData(DataReader reader)
        {
            var list = new SortedSet<string>();
            foreach (var item in reader)
            {
                list.Add(string.Format("{0}={1}", item.Key, item.Value));
            }
            return string.Join(",", list.ToArray());
        }

        bool CompareData(Dictionary<string, string> data1, Dictionary<string, string> data2)
        {
            if (data1.Count != data2.Count)
            {
                return false;
            }

            foreach (var item in data1)
            {
                if (!data2.ContainsKey(item.Key))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
