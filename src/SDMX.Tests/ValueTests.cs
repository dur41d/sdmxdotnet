using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class CodeValueTests
    {
        [Test]
        public void Create()
        {
            var x = CodeValue.Create("SomeId");
            x = (CodeValue)"SomeId2";
            var id = Id.Create("Id3");
            x = (CodeValue)id;
        }

        [Test]
        public void Equlity()
        {
            var x = (CodeValue)"Id1";
            var y = (CodeValue)"Id2";
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }

    [TestFixture]
    public class StringValueTests
    {
        [Test]
        public void Create()
        {
            StringValue x = new StringValue("SomeValue");
            StringValue y = (StringValue)"SomeID2";
        }

        [Test]
        public void Equlity()
        {
            var x = (StringValue)"x";
            var y = (StringValue)"y";
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }

    [TestFixture]
    public class DecimalValueTests
    {
        [Test]
        public void Create()
        {
            var x = new DecimalValue(3m);
            var y = (DecimalValue)434m;
        }

        [Test]
        public void Equlity()
        {
            var x = new DecimalValue(3m);
            var y = (DecimalValue)434m;
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }

    [TestFixture]
    public class IntegerValueTests
    {
        [Test]
        public void Create()
        {
            var x = new IntegerValue(3);
            var y = (IntegerValue)434;
        }

        [Test]
        public void Equlity()
        {
            var x = new IntegerValue(3);
            var y = (IntegerValue)434;
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }
}
