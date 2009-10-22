using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace OXM.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestPersonMap()
        {
            var person = new Person() { Name = "duraid", Age = 35 };
            person.Address = new Address() { Street = "Decelles", City = "Montreal" };
            var map = new PersonMap();

            var sb = new StringBuilder();

            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                map.WriteXml(writer, person);
            }

            Person person2;
            using (var reader = XmlReader.Create(new StringReader(sb.ToString())))
            {
                person2 = map.ReadXml(reader);
            }

            Assert.AreEqual(person.Name, person2.Name);
            Assert.AreEqual(person.Age, person2.Age);
            Assert.AreEqual(person.Address.City, person2.Address.City);
            Assert.AreEqual(person.Address.Street, person2.Address.Street);
        }
    }
}