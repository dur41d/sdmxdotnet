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
                key["FREQ"] = row["freq"];
                key["JD_TYPE"] = row["jdtype"];
                key["JD_CATEGORY"] = row["jdcat"];
                key["VIS_CTY"] = row["city"];

                var time = new YearTimePeriod((int)row["time"]);
                var value = new DecimalValue((decimal)row["value"]);

                var series = dataSet.Series.Get(key);
                var obs = series.Get(time);

                if (series.Attributes["TIME_FORMAT"] == null)
                {
                    series.Attributes["TIME_FORMAT"] = "P1Y";
                    series.Attributes["COLLECTION"] = "A";
                }

                obs.Value = value;
                obs.Attributes["OBS_STATUS"] = "A";

                series.Add(obs);
                dataSet.Series.Add(series);
            }

            //Assert.AreEqual(2, dataSet.Series.Count);
            //var series2 = dataSet.Series.ElementAt(0);
            //Assert.AreEqual(2, series2.Attributes.Count);
            //Assert.AreEqual(1 , series2.Count);
            //var obs2 = series2.Get((YearTimePeriod)1999);
            //Assert.IsTrue(obs2.Value == (DecimalValue)3.3m);
            //obs2 = series2.Get((YearTimePeriod)2000);
            //Assert.IsTrue(obs2.Value == (DecimalValue)4.4m);

            PrintDataSet(dataSet);
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


        [Test]
        public void build_full_dataset()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);

            var list = new List<long>();
            var list2 = new List<long>();
            
            foreach (var freq in keyFamily.Dimensions.Get("FREQ").CodeList)
            {
                foreach (var jdtype in keyFamily.Dimensions.Get("JD_TYPE").CodeList)
                {
                    foreach (var jdcat in keyFamily.Dimensions.Get("JD_CATEGORY").CodeList)
                    {
                        foreach (var city in keyFamily.Dimensions.Get("VIS_CTY").CodeList)
                        {
                            var key = dataSet.NewKey();
                            key["FREQ"] = freq;
                            key["JD_TYPE"] = jdtype;
                            key["JD_CATEGORY"] = jdcat;
                            key["VIS_CTY"] = city;
                            var timer1 = DateTime.Now;
                            var series = dataSet.Series.Get(key);
                            list.Add((DateTime.Now - timer1).Ticks);

                            for (int i = 1999; i < 2009; i++)
                            {                               
                                var obs = series.Get(new YearTimePeriod(i));

                                series.Attributes["TIME_FORMAT"] = "P1Y";
                                series.Attributes["COLLECTION"] = "A";

                                obs.Value = new DecimalValue(3.3m);
                                obs.Attributes["OBS_STATUS"] = "A";
                                
                                timer1 = DateTime.Now;
                                series.Add(obs);
                                list2.Add((DateTime.Now - timer1).Ticks);
                            }

                            dataSet.Series.Add(series);
                        }
                    }
                }
            }

            Console.WriteLine("Min: {0} Max: {1} Avg: {2}",
                new TimeSpan(list.Min()), new TimeSpan(list.Max()), new TimeSpan((long)list.Average()));

            Console.WriteLine("Min: {0} Max: {1} Avg: {2}",
                new TimeSpan(list2.Min()), new TimeSpan(list2.Max()), new TimeSpan((long)list2.Average()));
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
