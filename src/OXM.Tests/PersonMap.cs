using System.Xml.Linq;

namespace OXM.Tests
{
    public class PersonMap : RootElementMap<Person>
    {
        Person person = new Person();
        XNamespace ns = "www.duraid.com";
        XNamespace addressNS = "www.address.com";

        public PersonMap()
        {
            Map(o => o.Name).ToAttribute("name", true)
                .Set(v => person.Name = v)
                .Converter(new StringConverter());

            Map(o => o.Age).ToSimpleElement("age", true)
                .Set(v => person.Age = v)
                .Converter(new Int32Converter());

            //Map(o => new KeyValuePair<string, int>(o.Name, o.Age)).ToAttributeGroup("nameage")
            //    .Set(v => { person.Name = v.Key; person.Age = v.Value; })
            //    .GroupTypeMap(new NameAgeMap());

            MapCollection(o => o.Addresses).ToElement("Address", true)
                .Set(v => person.Addresses.Add(v))
                .ClassMap(() => new AddressMap());
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
