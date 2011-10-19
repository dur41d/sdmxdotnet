using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using SDMX.Parsers;
using System.Xml;
using System.IO;
using OXM;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace SDMX.Tests
{
    [TestFixture]
    public class TextFormatTests
    {
        [Test]
        public void StringTextFormat()
        {
            var textFormat = new StringTextFormat();

            string s = "foo";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is string);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("foo", s2);
        }

        [Test]
        public void StringTextFormat_NullString()
        {
            var textFormat = new StringTextFormat();

            string obj = null;
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual(null, s2);
        }

        [Test]
        public void DecimalTextFormat()
        {
            var textFormat = new DecimalTextFormat();

            string s = "1.7";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is decimal);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("1.7", s2);
        }

        [Test]
        public void IntegerTextFormat()
        {
            var textFormat = new IntegerTextFormat();

            string s = "1";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is int);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("1", s2);
        }


        [Test]
        public void Integer_SerializeNullable()
        {
            var textFormat = new IntegerTextFormat();

            int? obj = 1;
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("1", s2);
        }

        [Test]
        [ExpectedException(typeof(ParseException))]
        public void Integer_SerializeNull()
        {
            var textFormat = new IntegerTextFormat();

            int? obj = null;
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("1", s2);
        }

        [Test]
        public void DecimalTextFormat_IsValid()
        {
            var textFormat = new DecimalTextFormat();
            decimal value = 1.7m;
            Assert.IsTrue(textFormat.IsValid(value));
            decimal? value2 = 1.6m;
            Assert.IsTrue(textFormat.IsValid(value2));
        }

        [Test]
        public void TimePeriodTextFormat()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008-11";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008-11", s2);
        }

        [Test]
        public void TimePeriodTextFormat_Weekly()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008-W2";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008-W2", s2);
        }

        [Test]
        public void TimePeriodTextFormat_Year()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008", s2);
        }

        [Test]
        public void TimePeriodTextFormat_Date()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008-10-10";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008-10-10", s2);
        }

        [Test]
        public void TimePeriodTextFormat_DateTime()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008-10-10T15:05:05";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008-10-10T15:05:05", s2);
        }

        [Test]
        public void TimePeriodTextFormat_Quarterly()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008-Q2";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008-Q2", s2);
        }

        [Test]
        public void TimePeriodTextFormat_Binannual()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008-B1";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008-B1", s2);
        }

        [Test]
        public void TimePeriodTextFormat_Triannual()
        {
            var textFormat = new TimePeriodTextFormat();

            string s = "2008-T3";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is TimePeriod);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008-T3", s2);
        }

        [Test]
        public void YearTextFormat()
        {
            var textFormat = new YearTextFormat();

            string s = "2008";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2008", s2);
        }

        [Test]
        public void YearMonthTextFormat()
        {
            var textFormat = new YearMonthTextFormat();

            string s = "2009-1";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2009-01", s2);
        }

        [Test]
        public void DateTextFormat()
        {
            var textFormat = new DateTextFormat();

            string s = "2009-1-1";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2009-01-01", s2);
        }

        [Test]
        public void DateTimeTextFormat()
        {
            var textFormat = new DateTimeTextFormat();

            string s = "2009-1-1T01:01:01";
            object obj = textFormat.Parse(s, null);
            Assert.IsNotNull(obj);
            string startTime = null;
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2009-01-01T01:01:01", s2);
        }

        [Test]
        public void DateTimeTextFormat_SerializeDateTime()
        {
            var textFormat = new DateTimeTextFormat();

            string startTime = null;
            DateTime obj = new DateTime(2009, 1, 1, 1, 1, 1);
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2009-01-01T01:01:01", s2);
        }

        [Test]
        public void DateTimeTextFormat_SerializeDateTimeNullable()
        {
            var textFormat = new DateTimeTextFormat();

            string startTime = null;
            DateTime? obj = new DateTime(2009, 1, 1, 1, 1, 1);
            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2009-01-01T01:01:01", s2);
        }

        [Test]
        public void DateTimeTextFormat_SerializeDateTimeOffsetNullable()
        {
            var textFormat = new DateTimeTextFormat();

            string startTime = null;
            DateTimeOffset? obj = new DateTimeOffset(2009, 1, 1, 1, 1, 1, TimeSpan.Zero);

            string s2 = textFormat.Serialize(obj, out startTime);
            Assert.IsNull(startTime);
            Assert.AreEqual("2009-01-01T01:01:01", s2);
        }
    }
}
