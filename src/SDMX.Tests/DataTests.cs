﻿using System;
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
                key["FREQ"] = row["freq"];
                key["JD_TYPE"] = row["jdtype"];
                key["JD_CATEGORY"] = row["jdcat"];
                key["VIS_CTY"] = row["city"];

                var time = new YearTimePeriod((int)row["time"]);
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
            var obs2 = s[(YearTimePeriod)1999];
            Assert.IsTrue(obs2.Value == (DecimalValue)3.3m);
            obs2 = s[(YearTimePeriod)2000];
            Assert.IsTrue(obs2.Value == (DecimalValue)4.4m);

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
