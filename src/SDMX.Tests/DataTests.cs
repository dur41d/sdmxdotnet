using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void Create_dataset()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);
        }

        [Test]
        public void Add_new_series()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);

            var key = new Key();
            key["FREQ"] = "A";            
            key["JD_TYPE"] = "P";
            key["JD_CATEGORY"] = "B";
            key["VIS_CTY"] = "DE";
            var time = new YearTimePeriod(1999);
            dataSet.Series[key][time].Value = new DecimalValue(3.239482m);
        }
    }
}
