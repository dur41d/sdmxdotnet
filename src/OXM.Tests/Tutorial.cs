﻿using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;

namespace OXM.Tests
{
    //<?xml version="1.0" encoding="utf-16" ?> 
    //<Customer >
    //    <Name>John</Name>
    //    <Age>32</Age>
    //</Customer>

    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class CustomerMap : RootElementMap<Customer>
    {
        Customer cust = new Customer();
        
        public CustomerMap()
        {
            Map(c => c.Name).ToSimpleElement("Name", true)
                .Set(name => cust.Name = name)
                .Converter(new StringConverter());

            Map(c => c.Age).ToSimpleElement("Age", false)
                .Set(age => cust.Age = age)
                .Converter(new Int32Converter());
        }

        public override XName Name
        {
            get 
            {
                XNamespace ns = "uis.org";
                return ns + "Customer"; 
            }
        }

        protected override Customer Return()
        {
            return cust;
        }
    }

    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void WriteCustomer()
        {
            var customer = new Customer()
                {
                    Name = "John S.",
                    Age = 22
                };

            var map = new CustomerMap();

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                map.WriteXml(writer, customer);
            }
        }

        [Test]
        public void ReadCustmer()
        {           
            var map = new CustomerMap();

            var xml = new StringReader(@"<?xml version='1.0' encoding='utf-16' ?><Customer xmlns='uis.org'><Name>John</Name><Age>32</Age></Customer>");
            Customer customer;
            using (var reader = XmlReader.Create(xml))
            {
                customer = map.ReadXml(reader);
            }

            Assert.AreEqual("John", customer.Name);
            Assert.AreEqual(32, customer.Age);
        }
    }
}
