using System.Collections.Generic;

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
