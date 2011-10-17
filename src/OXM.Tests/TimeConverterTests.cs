using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace OXM.Tests
{
    [TestFixture]
    public class TimeConverterTests
    {
        [Test]
        public void DateTimeConverter()
        {
            var converter = new DateTimeConverter();

            string value = "2001-10-26T21:32:52+02:00";
            var time = converter.ToObj(value);
            Assert.AreEqual(value, converter.ToXml(time));

            value = "2001-10-26T21:32:52";
            time = converter.ToObj(value);
            Assert.AreEqual(value, converter.ToXml(time));

            value = "2001-10-26T19:32:52Z";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10-26T19:32:52", converter.ToXml(time));

            value = "2001-10-26T19:32:52+00:00";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10-26T19:32:52", converter.ToXml(time));

            value = "2001-10-26T21:32:52.126";
            time = converter.ToObj(value);
            Assert.AreEqual(value, converter.ToXml(time));
        }

        [Test]
        public void DateConverter()
        {
            var converter = new DateConverter();

            string value = "2001-10-16+02:00";
            var time = converter.ToObj(value);
            Assert.AreEqual("2001-10-16+02:00", converter.ToXml(time));

            value = "2001-10-26Z";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10-26", converter.ToXml(time));


            value = "2001-10-26+00:00";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10-26", converter.ToXml(time));

            value = "2001-10-26";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10-26", converter.ToXml(time));
        }

        [Test]
        public void YearMonthConverter()
        {
            var converter = new YearMonthConverter();

            string value = "2001-10+02:00";
            var time = converter.ToObj(value);
            Assert.AreEqual("2001-10+02:00", converter.ToXml(time));

            value = "2001-10Z";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10", converter.ToXml(time));

            value = "2001-10+00:00";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10", converter.ToXml(time));

            value = "2001-10";
            time = converter.ToObj(value);
            Assert.AreEqual("2001-10", converter.ToXml(time));
        }

        [Test]
        public void YearConverter()
        {
            var converter = new YearConverter();

            string value = "2001+02:00";
            var time = converter.ToObj(value);
            Assert.AreEqual(value, converter.ToXml(time));

            value = "2001Z";
            time = converter.ToObj(value);
            Assert.AreEqual("2001", converter.ToXml(time));

            value = "2001+00:00";
            time = converter.ToObj(value);
            Assert.AreEqual("2001", converter.ToXml(time));

            value = "2001";
            time = converter.ToObj(value);
            Assert.AreEqual("2001", converter.ToXml(time));
        }
    }
}
