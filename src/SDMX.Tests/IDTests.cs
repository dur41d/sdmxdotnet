using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class IdTests
    {
        [Test]
        public void Not_null()
        {
            Assert.Throws<ArgumentNullException>(() => 
            {
                string s = null;
                Id id = s; 
            });
        }

        [Test]
        public void Not_empty_string()
        {
            Assert.Throws<SDMXException>(() => { Id id = "   "; });
        }
        
        [Test]
        public void Test_equlity_with_null()
        {
            Id id1 = "D_S";

            Assert.IsFalse(id1 == null);
            Assert.IsTrue(id1 != null);
        }

        [Test]
        public void Test_equlity()
        {
            Id id1 = "D_S";
            Id id2 = "D_S";

            Assert.IsTrue(id1 == id2);
        }
    }
}
