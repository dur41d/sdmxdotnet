using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace OXM.Tests
{
    [TestFixture]
    public class PersonMapTests
    {   
        [Test]
        public void ReadWrite()
        {
            var person = new Person() { Name = "duraid", Age = 35 };
            person.Addresses.Add(new Address() { Street = "Decelles", City = "Montreal" });
            person.Addresses.Add(new Address() { Street = "Lincoln", City = "Montreal" });
            var map = new PersonMap();

            var doc = new XDocument();
            
            using (var writer = doc.CreateWriter())
            {
                map.WriteXml(writer, person);
            }

            Person person2;
            using (var reader = doc.CreateReader())
            {
                person2 = map.ReadXml(reader);
            }

            Assert.AreEqual(person.Name, person2.Name);
            Assert.AreEqual(person.Age, person2.Age);
            Assert.AreEqual(2, person.Addresses.Count);
            person.Addresses.Where(a => a.Street == "Decelles" && a.City == "Montreal").Single();
            person.Addresses.Where(a => a.Street == "Lincoln" && a.City == "Montreal").Single();
        }


        [Test]
        public void Warnings()
        {
            var doc = XDocument.Parse("<?xml version='1.0' encoding='utf-16'?><Customer xmlns='uis.org'><Name>John</Name><Age>32</Age><Occupation/></Customer>");
            var map = new CustomerMap();

            Customer customer;
            using (var reader = doc.CreateReader())
            {
                customer = map.ReadXml(reader, e => Console.WriteLine(e.Message));
            }

            Assert.AreEqual("John", customer.Name);
            Assert.AreEqual(32, customer.Age);
        }

        [Test]
        public void Conversion_error()
        {
            var doc = XDocument.Parse("<?xml version='1.0' encoding='utf-16'?><Customer xmlns='uis.org'><Name>John</Name><Age>32s</Age></Customer>");
            var map = new CustomerMap();

            Customer customer;
            using (var reader = doc.CreateReader())
            {
                customer = map.ReadXml(reader, e => Console.WriteLine(e.Message));
            }

            Assert.AreEqual("John", customer.Name);
            Assert.AreEqual(0, customer.Age);
        }

        [Test]
        public void required_notfound()
        {
            var doc = XDocument.Parse("<?xml version='1.0' encoding='utf-16'?><Customer xmlns='uis.org'><Name></Name><Age>32</Age></Customer>");
            var map = new CustomerMap();

            Customer customer;
            using (var reader = doc.CreateReader())
            {
                customer = map.ReadXml(reader, e => Console.WriteLine(e.Message));
            }

            Assert.AreEqual("", customer.Name);
            Assert.AreEqual(32, customer.Age);
        }
    }
}