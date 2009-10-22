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
    public class AddressMap : ClassMap<Address>
    {
        Address address = new Address();

        public AddressMap()
        {
            Map(o => o.Street).ToSimpleElement("Street", true)
                .Set(v => address.Street = v)
                .Converter(new StringConverter());

            Map(o => o.City).ToSimpleElement("City", true)
                .Set(v => address.City = v)
                .Converter(new StringConverter());
        }

        protected override Address Return()
        {
            return address;
        }
    }
}
