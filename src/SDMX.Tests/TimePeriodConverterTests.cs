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

namespace SDMX.Tests
{
    [TestFixture]
    public class TimePeriodConverterTests
    {
        [Test]
        public void DateTimeTimePeriod()
        {
            var converter = new TimePeriodConverter();

            string value = "2001-10-26T21:32:52+02:00";
            var time = converter.ToObj(value);
            Assert.IsTrue(time is DateTimeValue);

            value = "2001-10-26T21:32:52";
            time = converter.ToObj(value);
            Assert.IsTrue(time is DateTimeValue);

            value = "2001-10-26T19:32:52Z";
            time = converter.ToObj(value);
            Assert.IsTrue(time is DateTimeValue);


            value = "2001-10-26T19:32:52+00:00";
            time = converter.ToObj(value);
            Assert.IsTrue(time is DateTimeValue);

            value = "2001-10-26T21:32:52.12679";
            time = converter.ToObj(value);
            Assert.IsTrue(time is DateTimeValue);
        }

        [Test]
        public void DateTimePeriod()
        {
            var converter = new TimePeriodConverter();

            string value = "2001-10-16+02:00";
            var time = converter.ToObj(value);
            Assert.IsTrue(time is DateValue);
            Assert.AreEqual("2001-10-16+02:00", time.ToString());

            value = "2001-10-26Z";
            time = converter.ToObj(value);
            Assert.IsTrue(time is DateValue);
            Assert.AreEqual("2001-10-26", time.ToString());


            value = "2001-10-26+00:00";
            time = converter.ToObj(value);
            Assert.IsTrue(time is DateValue);
            Assert.AreEqual("2001-10-26", time.ToString());

            value = "2001-10-26";
            time = converter.ToObj(value);
            Assert.IsTrue(time is DateValue);
            Assert.AreEqual("2001-10-26", time.ToString());
        }

        [Test]
        public void YearMonthTimePeriod()
        {
            var converter = new TimePeriodConverter();

            string value = "2001-10+02:00";
            var time = converter.ToObj(value);
            Assert.IsTrue(time is YearMonthValue);
            Assert.AreEqual("2001-10+02:00", time.ToString());

            value = "2001-10Z";
            time = converter.ToObj(value);
            Assert.IsTrue(time is YearMonthValue);
            Assert.AreEqual("2001-10", time.ToString());

            value = "2001-10+00:00";
            time = converter.ToObj(value);
            Assert.IsTrue(time is YearMonthValue);
            Assert.AreEqual("2001-10", time.ToString());

            value = "2001-10";
            time = converter.ToObj(value);
            Assert.IsTrue(time is YearMonthValue);
            Assert.AreEqual("2001-10", time.ToString());
        }

        [Test]
        public void YearTimePeriod()
        {
            var converter = new TimePeriodConverter();

            string value = "2001+02:00";            
            var time = converter.ToObj(value);
            Assert.IsTrue(time is YearValue);
            Assert.AreEqual(value, time.ToString());

            value = "2001Z";
            time = converter.ToObj(value);
            Assert.IsTrue(time is YearValue);
            Assert.AreEqual("2001", time.ToString());

            value = "2001+00:00";
            time = converter.ToObj(value);
            Assert.IsTrue(time is YearValue);
            Assert.AreEqual("2001", time.ToString());

            value = "2001";
            time = converter.ToObj(value);
            Assert.IsTrue(time is YearValue);
            Assert.AreEqual("2001", time.ToString());
        }
    }
}
