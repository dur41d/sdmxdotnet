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
        public void can_assign_string()
        {
            ID id = "someID";
            Assert.IsTrue(id == "someID");
            Assert.IsFalse(id == "otherID");
            Assert.IsFalse(null == id);
            Assert.IsFalse(id == null);
        }

        [Test]
        public void restriced_to_pattern()
        {
            Assert.Throws<SDMXException>(delegate()
                { 
                    ID id = "Has.Dot"; 
                });
        }
    }
}
