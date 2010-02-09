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
                var key = new Key();
                key["FREQ"] = (string)row["freq"];
                key["JD_TYPE"] = (string)row["jdtype"];
                key["JD_CATEGORY"] = (string)row["jdcat"];
                key["VIS_CTY"] = (string)row["city"];
                var time = new YearTimePeriod((int)row["time"]);
                var value = new DecimalValue(2332);

                dataSet.Series[key][time].Value = value;
            }

            Assert.IsTrue(dataSet.Series.Count == 1);
            var series = dataSet.Series.ElementAt(0);
            Assert.IsTrue(series.Count == 1);
            Assert.IsTrue(series.ElementAt(0).Value == ((DecimalValue)2332));
            Assert.IsTrue(series.ElementAt(0).Time == (YearTimePeriod)1999);
            
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
            row["value"] = 3.3223m;

            table.Rows.Add(row);

            return table;
        }
    }
}
