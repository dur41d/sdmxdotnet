using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;

namespace SDMX.Tests
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void Create_new_dataset()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);
        }

        [Test]
        public void Create_from_datatable()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);

            var dataTable = GetDataTable();

            foreach (DataRow row in dataTable.Rows)
            {
                var key = dataSet.NewKey();
                key["FREQ"] = (ID)(row["freq"] as string);
                key["JD_TYPE"] = (ID)(row["jdtype"] as string);
                key["JD_CATEGORY"] = (ID)(row["jdcat"] as string);
                key["VIS_CTY"] = (ID)(row["city"] as string);
                var time = new YearTimePeriod((int)row["time"]);
                var value = new DecimalValue((decimal)row["value"]);

                var series = dataSet.Series.Get(key);
                var obs = series.Get(time);

                if (series.Attributes["TIME_FORMAT"] == null)
                {
                    series.Attributes["TIME_FORMAT"] = (ID)"P1Y";
                    series.Attributes["COLLECTION"] = (ID)"A";
                }

                obs.Value = value;
                obs.Attributes["OBS_STATUS"] = (ID)"A";

                series.Add(obs);
                dataSet.Series.Add(series);
            }

            Assert.IsTrue(dataSet.Series.Count == 1);
            var series2 = dataSet.Series.ElementAt(0);
            Assert.IsTrue(series2.Attributes.Count == 2);
            Assert.IsTrue(series2.Count == 2);
            var obs2 = series2.Get((YearTimePeriod)1999);
            Assert.IsTrue(obs2.Value == (DecimalValue)3.3m);
            obs2 = series2.Get((YearTimePeriod)2000);
            Assert.IsTrue(obs2.Value == (DecimalValue)4.4m);

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
