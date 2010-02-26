using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using System.Xml.Linq;
using System.Xml;

namespace SDMX.Tests
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void Create_new_dataset()
        {
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);
        }

        [Test]
        public void Create_from_datatable()
        {
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);            

            var dataTable = GetDataTable();

            foreach (DataRow row in dataTable.Rows)
            {
                var key = dataSet.NewKey();                
                key["FREQ"] = row["freq"].ToString();
                key["JD_TYPE"] = row["jdtype"];
                key["JD_CATEGORY"] = row["jdcat"];
                key["VIS_CTY"] = row["city"];

                var time = new YearValue((int)row["time"]);
                var value = new DecimalValue((decimal)row["value"]);

                var series = dataSet.Series[key];
                bool add = false;
                if (series == null)
                {
                    series = dataSet.Series.Create(key);
                    series.Attributes["TIME_FORMAT"] = "P1Y";
                    series.Attributes["COLLECTION"] = "A";
                    add = true;
                }

                var obs = series.Create(time);               

                obs.Value = value;
                obs.Attributes["OBS_STATUS"] = "A";

                series.Add(obs);

                if (add)
                {
                    dataSet.Series.Add(series);
                }
            }

            Assert.AreEqual(1, dataSet.Series.Count);
            var s = dataSet.Series.ElementAt(0);
            Assert.AreEqual(2, s.Attributes.Count);
            Assert.AreEqual(2, s.Count);
            var obs2 = s[(YearValue)1999];
            Assert.IsTrue(obs2.Value == (DecimalValue)3.3m);
            obs2 = s[(YearValue)2000];
            Assert.IsTrue(obs2.Value == (DecimalValue)4.4m);

            PrintDataSet(dataSet);
        }

        [Test]
        public void Generic_read_write()
        {
            string dataPath = Utility.GetPath("lib\\GenericSample2.xml");
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            var message = DataMessage.LoadGeneric(dataPath, keyFamily);

            var doc = new XDocument();            
            using (var writer = doc.CreateWriter())
            {
                message.WriteGeneric(writer);
            }

            Assert.IsTrue(Utility.IsValidMessage(doc));
        }

        [Test]
        public void Compact_read_write()
        {
            string dataPath = Utility.GetPath("lib\\CompactSampleNoGroups.xml");
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            string ns = "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=BIS:EXT_DEBT:compact";

            var message = DataMessage.LoadCompact(dataPath, keyFamily, ns);

            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                message.WriteCompact(writer, "uis", ns);
            }

            var schema = Utility.GetComapctSchema("lib\\StructureSample.xml", ns);
            Assert.IsTrue(Utility.IsValidMessage(doc, schema));
        }

        [Test]
        public void Convert_from_generic_to_compact_and_back()
        {
            string dataPath = Utility.GetPath("lib\\GenericSample2.xml");
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            var message = DataMessage.LoadGeneric(dataPath, keyFamily);
            message.SaveGeneric(Utility.GetPath("lib\\GenericSample4.xml"));

            string ns = "urn:sdmx:org.sdmx.infomodel.keyfamily.KeyFamily=BIS:EXT_DEBT:compact";

            var compact = new XDocument();
            using (var writer = compact.CreateWriter())
            {
                message.WriteCompact(writer, "uis", ns);
            }

            DataMessage message2 = null;
            using (var reader = compact.CreateReader())
            {
                message2 = DataMessage.ReadCompact(reader, keyFamily, ns);
            }

            message2.SaveGeneric(Utility.GetPath("lib\\GenericSample3.xml"));

            Utility.AssertDataMessageEqual(message, message2);
        }

        private void PrintDataSet(DataSet dataSet)
        {
            PrintAttributes(dataSet.Attributes);
            foreach (var series in dataSet.Series)
            {
                Console.Write("Series: {0}, Att: ", series.Key);
                PrintAttributes(series.Attributes);
                foreach (var obs in series)
                {
                    Console.Write("Obs: {0}={1}, Att: ", obs.Time, obs.Value);
                    PrintAttributes(obs.Attributes);
                }
            }
        }

        private void PrintAttributes(AttributeValueCollection atts)
        {
            foreach (var att in atts)
            {
                Console.Write("{0}={1} ", att.Key, att.Value);
            }
            Console.WriteLine();
        }

        private DataTable GetDataTable()
        {
            var table = new DataTable();
            table.Columns.Add("freq", typeof(string));
            table.Columns.Add("jdtype", typeof(string));
            table.Columns.Add("jdcat", typeof(string));
            table.Columns.Add("city", typeof(string));
            table.Columns.Add("time", typeof(int));
            table.Columns.Add("value", typeof(decimal));

            var row = table.NewRow();
            row["freq"] = "A";
            row["jdtype"] = "P";
            row["jdcat"] = "B";
            row["city"] = "DE";
            row["time"] = 1999;
            row["value"] = 3.3m;

            table.Rows.Add(row);

            row = table.NewRow();
            row["freq"] = "A";
            row["jdtype"] = "P";
            row["jdcat"] = "B";
            row["city"] = "DE";
            row["time"] = 2000;
            row["value"] = 4.4m;

            table.Rows.Add(row);

            return table;
        }
    }
}
