using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace OXM.Tests
{
    public class t1
    {
        public List<string> t2 { get; private set; }
        public List<string> t3 { get; private set; }

        public t1()
        {
            t2 = new List<string>();
            t3 = new List<string>();
        }
    }

    public class t1Map : RootElementMap<t1>
    {
        t1 _t1 = new t1();

        public t1Map()
        {
            var container = MapContainer("container", true);

            container.MapCollection(o => o.t2).ToSimpleElement("t2", false)
                .Set(v => _t1.t2.Add(v))
                .Converter(new StringConverter());


            container.MapCollection(o => o.t2).ToSimpleElement("t3", false)
                .Set(v => _t1.t3.Add(v))
                .Converter(new StringConverter());

        }

        protected override t1 Return()
        {
            return _t1;
        }

        public override XName Name
        {
            get { return "t1"; }
        }
    }

    [TestFixture]
    public class t1Tests
    {
        [Test]
        public void t1_test()
        {
            var doc = XDocument.Parse(
@"<t1>
    <container>
        <t2>1</t2>
        <t2>2</t2>
        <t2>3</t2>
        <t3>1</t3>
        <t3>2</t3>
    </container>
</t1>");
            var map = new t1Map();

            t1 _t1;
            using (var reader = doc.CreateReader())
            {
                _t1 = map.ReadXml(reader);
            }

            Assert.AreEqual(3, _t1.t2.Count);
            Assert.AreEqual(2, _t1.t3.Count);
        }
    }
}
