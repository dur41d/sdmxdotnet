using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class InternationalTextTests
    {
        [Test]
        public void Equals()
        {
            var x = new InternationalString("en", "text");
            var y = new InternationalString("en", "text");
            var z = new InternationalString("en", "text");

            Assert.IsTrue(x.Equals(x));
            Assert.IsTrue(x.Equals(y));
            Assert.IsTrue(y.Equals(x));
            Assert.IsTrue(y.Equals(z));
            Assert.IsTrue(x.Equals(z));
            Assert.IsFalse(x.Equals(null));

            // Assert.IsTrue(x == x);
            Assert.IsTrue(x == y);
            Assert.IsTrue(y == x);
            Assert.IsTrue(y == z);
            Assert.IsTrue(x == z);
            Assert.IsFalse(x == null);
            Assert.IsFalse(null == x);
        }

        [Test]
        public void NotEquals()
        {
            var x = new InternationalString("en", "text");
            var y = new InternationalString("en", "text2");
            var z = new InternationalString("es", "text");

            Assert.IsTrue(x != y);
            Assert.IsTrue(y != x);
            Assert.IsTrue(y != z);
            Assert.IsTrue(x != z);
            Assert.IsTrue(x != null);
            Assert.IsTrue(null != x);
        }
    }
}
