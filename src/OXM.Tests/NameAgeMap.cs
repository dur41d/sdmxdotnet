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
    public class NameAgeMap : AttributeGroupTypeMap<KeyValuePair<string, int>>
    {
        string _name;
        int _age;

        public NameAgeMap()
        {
            MapAttribute(o => o.Key, "name", true)
                .Set(v => _name = v)
                .Converter(new StringConverter());

            MapAttribute(o => o.Value, "age", true)
                .Set(v => _age = v)
                .Converter(new Int32Converter());
        }

        protected override KeyValuePair<string, int> Return()
        {
            return new KeyValuePair<string, int>(_name, _age);
        }
    }
}
