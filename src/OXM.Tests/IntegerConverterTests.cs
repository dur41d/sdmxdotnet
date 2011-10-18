using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace OXM.Tests
{
    [TestFixture]
    public class IntegerConverterTests
    {
        [Test]
        public void Int32Converter()
        {
            var converter = new Int32Converter();

            string value = "2001";
            var time = converter.ToObj(value);
            Assert.AreEqual(value, converter.ToXml(time));
        }

        [Test]
        public void Int32Converter_object()
        {
            ISimpleTypeConverter converter = new Int32Converter();

            string value = "2001";
            var obj = converter.ToObj(value);
            Assert.AreEqual(value, converter.ToXml(obj));
        }

        [Test]
        public void Int32Converter_Nullable()
        {
            ISimpleTypeConverter converter = new Int32Converter();

            int? obj = 1;
            string xml = converter.ToXml(obj);
            Assert.AreEqual("1", converter.ToXml(obj));
        }

        [Test]
        [ExpectedException(typeof(ParseException))]
        public void Int32Converter_Null()
        {
            ISimpleTypeConverter converter = new Int32Converter();

            int? obj = null;
            string xml = converter.ToXml(obj);
        }

        [Test]
        public void NullableInt32Converter()
        {
            var converter = new NullableInt32Converter();

            int? obj = 1;
            string xml = converter.ToXml(obj);
            Assert.AreEqual("1", converter.ToXml(obj));
        }

        [Test]
        public void NullableInt32Converter_Null()
        {
            var converter = new NullableInt32Converter();

            int? obj = null;
            string xml = converter.ToXml(obj);
            Assert.AreEqual(null, converter.ToXml(obj));
        }


        [Test]
        public void NullableInt32Converter_object()
        {
            ISimpleTypeConverter converter = new NullableInt32Converter();

            int? obj = 1;
            string xml = converter.ToXml(obj);
            Assert.AreEqual("1", converter.ToXml(obj));
        }

        [Test]
        public void NullableInt32Converter_Null_object()
        {
            ISimpleTypeConverter converter = new NullableInt32Converter();

            int? obj = null;
            string xml = converter.ToXml(obj);
            Assert.AreEqual(null, converter.ToXml(obj));
        }
    }
}
