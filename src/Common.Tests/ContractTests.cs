using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common;

namespace SDMX.Tests
{
    [TestFixture]
    public class ContractTests
    {
        [Test]
        public void AssertNotNull()
        {
            StringBuilder o = new StringBuilder("some object");

            Contract.AssertNotNull(() => o);

            o = null;

            Assert.Throws<ArgumentNullException>(
                    () => Contract.AssertNotNull(() => o)
                );

            o = new StringBuilder("other object");

            Contract.AssertNotNull(() => o);
        }

        [Test]
        public void AssrtNotNullOrEmpty()
        {
            string o = "   ";

            Assert.Throws<ArgumentException>(
                    () => Contract.AssertNotNullOrEmpty(() => o)
                );
            o = "some string";

            Contract.AssertNotNullOrEmpty(() => o);
        }
    }
}
