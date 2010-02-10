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
                var key = new Key();
                key[(ID)"FREQ"] = keyFamily.Dimensions.Get((ID)"FREQ").CodeList.Get((ID)((string)row["freq"]));
                key[(ID)"JD_TYPE"] = keyFamily.Dimensions.Get((ID)"JD_TYPE").CodeList.Get((ID)((string)row["jdtype"]));
                key[(ID)"JD_CATEGORY"] = keyFamily.Dimensions.Get((ID)"JD_CATEGORY").CodeList.Get((ID)((string)row["jdcat"]));
                key[(ID)"VIS_CTY"] = keyFamily.Dimensions.Get((ID)"VIS_CTY").CodeList.Get((ID)((string)row["city"]));
                var time = new YearTimePeriod((int)row["time"]);
                var value = new DecimalValue((decimal)row["value"]);

                var series = dataSet.Series.Get(key);
                var obs = series.Get(time);

                if (series.Attributes[(ID)"TIME_FORMAT"] == null)
                {
                    series.Attributes[(ID)"TIME_FORMAT"] = keyFamily.Attributes.Get((ID)"TIME_FORMAT").CodeList.Get((ID)"P1Y");
                    series.Attributes[(ID)"COLLECTION"] = keyFamily.Attributes.Get((ID)"COLLECTION").CodeList.Get((ID)"A");
                }
                
                obs.Value = value;
                obs.Attributes[(ID)"OBS_STATUS"] = keyFamily.Attributes.Get((ID)"OBS_STATUS").CodeList.Get((ID)"A");
                
                series.Add(obs);
                dataSet.Series.Add(series);
            }

            Assert.IsTrue(dataSet.Series.Count == 1);
            var series2 = dataSet.Series.ElementAt(0);
            Assert.IsTrue(series2.Count == 2);
            var obs2 = series2.Get((YearTimePeriod)1999);
            Assert.IsTrue(obs2.Value == (DecimalValue)3.3m);
            obs2 = series2.Get((YearTimePeriod)2000);
            Assert.IsTrue(obs2.Value == (DecimalValue)4.4m);
            
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
