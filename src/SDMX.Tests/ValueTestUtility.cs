using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    public static class ValueTestUtility
    {
        public static void TestEquality<T>(T a, T b)
        {
            T x = a;
            T y = b;
            object oy = y;
            object ox = x;

            Assert.IsTrue(x.Equals(y));
            Assert.IsTrue(x.Equals(oy));
            Assert.IsTrue(ox.Equals(y));
            Assert.IsTrue(ox.Equals(oy));
        }

        public static void TestUnequlity<T>(T a, T b)
        {
            T x = a;
            T y = b;
            object oy = y;
            object ox = x;

            Assert.IsFalse(x.Equals(y));
            Assert.IsFalse(x.Equals(oy));
            Assert.IsFalse(ox.Equals(y));
            Assert.IsFalse(ox.Equals(oy));
        }

        public static void TestComarisonWithNull<T>(T a)
        {
            T x = a;
            T y = default(T);
            object oy = y;
            object ox = x;

            Assert.IsFalse(x.Equals(y));
            Assert.IsFalse(x.Equals(oy));
            Assert.IsFalse(ox.Equals(y));
            Assert.IsFalse(ox.Equals(oy));

            x = default(T);
            y = a;

            oy = y;
            ox = x;

            Assert.IsFalse(y.Equals(x));
            Assert.IsFalse(oy.Equals(x));
            Assert.IsFalse(y.Equals(ox));
            Assert.IsFalse(oy.Equals(ox));
        }
    }
}
