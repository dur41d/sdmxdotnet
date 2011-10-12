using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]
    public class DateTimeValueTests
    {
        [Test]
        public void Create()
        {
            var x = new DateTimeValue(new DateTimeOffset(2000, 1, 1, 1, 1, 1, new TimeSpan()));
            var x2 = new DateTimeValue(new DateTimeOffset(1999, 1, 1, 1, 1, 1, new TimeSpan()));
        }

        [Test]
        public void Equlity()
        {
            var x = new DateTimeValue(new DateTimeOffset(2000, 1, 1, 1, 1, 1, new TimeSpan()));
            var y = new DateTimeValue(new DateTimeOffset(1999, 1, 1, 1, 1, 1, new TimeSpan()));
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }    

    [TestFixture]
    public class DateValueTests
    {
        [Test]
        public void Create()
        {
            var x = new Date(new DateTimeOffset(2000, 1, 1, 1, 1, 1, new TimeSpan()));
            var x2 = new Date(new DateTimeOffset(1999, 1, 1, 1, 1, 1, new TimeSpan()));           
        }

        [Test]
        public void Equlity()
        {
            var x = new Date(new DateTimeOffset(2000, 1, 1, 1, 1, 1, new TimeSpan()));
            var y = new Date(new DateTimeOffset(1999, 1, 1, 1, 1, 1, new TimeSpan()));           
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }    

    [TestFixture]
    public class YearMonthValueTests
    {
        [Test]
        public void Create()
        {
            var x = new YearMonth(1999, 3);
            var x2 = new YearMonth(new DateTimeOffset(1999, 1, 1, 1, 1, 1, new TimeSpan()));
            Assert.AreEqual(1999, x.Year);
            Assert.AreEqual(3, x.Month);
            Assert.AreEqual(1999, x2.Year);
            Assert.AreEqual(1, x2.Month);
        }

        [Test]
        public void Equlity()
        {
            var x = new YearMonth(1999, 3);
            var y = new YearMonth(2000, 3);
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
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
            ValueTestUtility.TestEquality((YearValue)1999, (YearValue)1999);
            ValueTestUtility.TestUnequlity((YearValue)1999, (YearValue)2000);
            ValueTestUtility.TestComarisonWithNull((YearValue)1999);
        }        
    }

    [TestFixture]
    public class QuarterlyValueTests
    {
        [Test]
        public void Create()
        {
            var x = new Quarterly(2000, Quarter.Q1);
            var y = new Quarterly(2000, Quarter.Q2);
            Assert.AreEqual(2000, x.Year);
            Assert.AreEqual(Quarter.Q1, x.Quarter);
            Assert.AreEqual(2000, y.Year);
            Assert.AreEqual(Quarter.Q2, y.Quarter);
        }

        [Test]
        public void Equlity()
        {
            var x = new Quarterly(2000, Quarter.Q1);
            var y = new Quarterly(2000, Quarter.Q2);
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }    

    [TestFixture]
    public class BiannualValueTests
    {
        [Test]
        public void Create()
        {
            var year = new Biannual(2000, Biannum.B1);
            var year2 = new Biannual(2001, Biannum.B1);
            Assert.AreEqual(2000, year.Year);
            Assert.AreEqual(Biannum.B1, year.Annum);
            Assert.AreEqual(2000, year.Year);
            Assert.AreEqual(Biannum.B1, year2.Annum);
        }

        [Test]
        public void Equlity()
        {
            var x = new Biannual(2000, Biannum.B1);
            var y = new Biannual(2001, Biannum.B1);
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }

    [TestFixture]
    public class TriannualValueTests
    {
        [Test]
        public void Create()
        {
            var year = new Triannual(2000, Triannum.T1);
            var year2 = new Triannual(2001, Triannum.T2);
            Assert.AreEqual(2000, year.Year);
            Assert.AreEqual(Triannum.T1, year.Annum);
            Assert.AreEqual(2001, year2.Year);
            Assert.AreEqual(Triannum.T2, year2.Annum);
        }

        [Test]
        public void Equlity()
        {
            var x = new Triannual(2000, Triannum.T1);
            var y = new Triannual(2001, Triannum.T2);
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }

    [TestFixture]
    public class WeeklyValueTests
    {
        [Test]
        public void Create()
        {
            var year = new Weekly(2000, Week.W1);
            var year2 = new Weekly(2001, Week.W32);
            Assert.AreEqual(2000, year.Year);
            Assert.AreEqual(Week.W1, year.Week);
            Assert.AreEqual(2001, year2.Year);
            Assert.AreEqual(Week.W32, year2.Week);
        }

        [Test]
        public void Equlity()
        {
            var x = new Weekly(2000, Week.W1);
            var y = new Weekly(2001, Week.W32);
            ValueTestUtility.TestEquality(x, x);
            ValueTestUtility.TestUnequlity(x, y);
            ValueTestUtility.TestComarisonWithNull(x);
        }
    }    
}