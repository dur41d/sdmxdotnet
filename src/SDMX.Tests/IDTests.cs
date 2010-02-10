using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class IDTests
    {
        [Test]
        public void Not_null()
        {
            Assert.Throws<SDMXException>(() => { ID id = null; });
        }

        [Test]
        public void Not_empty_string()
        {
            Assert.Throws<SDMXException>(() => { ID id = "   "; });
        }


        [Test]
        public void Test_equlity_with_null()
        {
            ID id1 = "D_S";

            Assert.IsFalse(id1 == null);
            Assert.IsTrue(id1 != null);
        }

        [Test]
        public void Test_equlity()
        {
            ID id1 = "D_S";
            ID id2 = "D_S";

            Assert.IsTrue(id1 == id2);
        }
    }
}
