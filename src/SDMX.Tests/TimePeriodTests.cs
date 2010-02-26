using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class YearMonthValueTests
    {
        [Test]
        public void Create()
        {
            var x = new YearMonthValue(1999, 3);
            var x2 = new YearMonthValue(new DateTimeOffset(1999, 1, 1, 1, 1, 1, new TimeSpan()));
            Assert.AreEqual(1999, x.Year);
            Assert.AreEqual(3, x.Month);
            Assert.AreEqual(1999, x2.Year);
            Assert.AreEqual(1, x2.Month);
        }

        [Test]
        public void Equlity()
        {
            var x = new YearMonthValue(1999, 3);
            var y = new YearMonthValue(2000, 3);
            ValueTestUtil.TestEquality(x, x);
            ValueTestUtil.TestUnequlity(x, y);
            ValueTestUtil.TestComarisonWithNull(x);
        }
    }    

    [TestFixture]
    public class YearValueTests
    {
        [Test]
        public void Create()
        {
            var year = new YearValue(1999);
            var year2 = new YearValue(new DateTimeOffset(1999, 1, 1, 1, 1, 1, new TimeSpan()));
            Assert.AreEqual(1999, year.Year);
            Assert.AreEqual(1999, year2.Year);
        }

        [Test]
        public void Equlity()
        {
            ValueTestUtil.TestEquality((YearValue)1999, (YearValue)1999);
            ValueTestUtil.TestUnequlity((YearValue)1999, (YearValue)2000);
            ValueTestUtil.TestComarisonWithNull((YearValue)1999);
        }        
    }    
    
    [TestFixture]
    public class CodeValueTests
    {
        [Test]
        public void Create()
        {
            var x = CodeValue.Create("SomeID");
            x = (CodeValue)"SomeID2";
            var id = ID.Create("ID3");
            x = (CodeValue)id;
        }

        [Test]
        public void Equlity()
        {
            ValueTestUtil.TestEquality((CodeValue)"ID1", (CodeValue)"ID1");
            ValueTestUtil.TestUnequlity((CodeValue)"ID1", (CodeValue)"ID2");
            ValueTestUtil.TestComarisonWithNull((CodeValue)"ID1");
        }
    }

    public static class ValueTestUtil
    {
        public static void TestEquality<T>(T a, T b) where T : Value
        {
            T x = a;
            T y = b;
            object oy = y;
            object ox = x;
            Value ix = x;
            Value iy = y;

            Assert.IsTrue(x.Equals(y));
            Assert.IsTrue(x.Equals(oy));
            Assert.IsTrue(x.Equals(iy));
            Assert.IsTrue(ox.Equals(y));
            Assert.IsTrue(ox.Equals(oy));
            Assert.IsTrue(ox.Equals(iy));
            Assert.IsTrue(ix.Equals(y));
            Assert.IsTrue(ix.Equals(oy));
            Assert.IsTrue(ix.Equals(iy));

            Assert.IsTrue(x == y);
            Assert.IsTrue(x == oy);
            Assert.IsTrue(x == iy);
            Assert.IsTrue(ix == y);
            Assert.IsTrue(ix == oy);
            Assert.IsTrue(ix == iy);
        }

        public static void TestUnequlity<T>(T a, T b) where T : Value
        {
            T x = a;
            T y = b;
            object oy = y;
            object ox = x;
            Value ix = x;
            Value iy = y;

            Assert.IsFalse(x.Equals(y));
            Assert.IsFalse(x.Equals(oy));
            Assert.IsFalse(x.Equals(iy));
            Assert.IsFalse(ox.Equals(y));
            Assert.IsFalse(ox.Equals(oy));
            Assert.IsFalse(ox.Equals(iy));
            Assert.IsFalse(ix.Equals(y));
            Assert.IsFalse(ix.Equals(oy));
            Assert.IsFalse(ix.Equals(iy));

            Assert.IsTrue(x != y);
            Assert.IsTrue(x != oy);
            Assert.IsTrue(x != iy);
            Assert.IsTrue(ix != y);
            Assert.IsTrue(ix != oy);
            Assert.IsTrue(ix != iy);
        }

        public static void TestComarisonWithNull<T>(T a) where T : Value
        {
            T x = a;
            T y = null;
            object oy = y;
            object ox = x;
            Value ix = x;
            Value iy = y;

            Assert.IsFalse(x.Equals(y));
            Assert.IsFalse(x.Equals(oy));
            Assert.IsFalse(x.Equals(iy));
            Assert.IsFalse(ox.Equals(y));
            Assert.IsFalse(ox.Equals(oy));
            Assert.IsFalse(ox.Equals(iy));
            Assert.IsFalse(ix.Equals(y));
            Assert.IsFalse(ix.Equals(oy));
            Assert.IsFalse(ix.Equals(iy));

            Assert.IsTrue(x != y);
            Assert.IsTrue(x != oy);
            Assert.IsTrue(x != iy);
            Assert.IsTrue(ix != y);
            Assert.IsTrue(ix != oy);
            Assert.IsTrue(ix != iy);

            x = null;
            y = a;

            oy = y;
            ox = x;
            ix = x;
            iy = y;

            Assert.IsTrue(x != y);
            Assert.IsTrue(x != oy);
            Assert.IsTrue(x != iy);
            Assert.IsTrue(ix != y);
            Assert.IsTrue(ix != oy);
            Assert.IsTrue(ix != iy);
        }
    }
}
