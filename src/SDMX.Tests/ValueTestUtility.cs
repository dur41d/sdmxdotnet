using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    public static class ValueTestUtility
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

            bool s = x == y;

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
