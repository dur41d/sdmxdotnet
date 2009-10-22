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
    public class PersonMap : RoolElementMap<Person>
    {
        Person person = new Person();
        XNamespace ns = "www.duraid.com";
        XNamespace addressNS = "www.address.com";

        public PersonMap()
        {
            //Map(o => o.Name).ToAttribute("name", true)
            //    .Set(v => person.Name = v)
            //    .Converter(new StringConverter());

            //Map(o => o.Age).ToSimpleElement("age", true)
            //    .Set(v => person.Age = v)
            //    .Converter(new Int32Converter());

            Map(o => new KeyValuePair<string, int>(o.Name, o.Age)).ToAttributeGroup("nameage")
                .Set(v => { person.Name = v.Key; person.Age = v.Value; })
                .GroupTypeMap(new NameAgeMap());

            Map(o => o.Address).ToElement(addressNS + "Address", true)
                .Set(v => person.Address = v)
                .ClassMap(new AddressMap());
        }

        protected override Person Return()
        {
            return person;
        }

        public override XName Name
        {
            get { return ns + "Person"; }
        }
    }
}
