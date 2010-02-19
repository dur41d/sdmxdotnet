using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class ValueMap : ClassMap<KeyValuePair<ID, IValue>>
    {
        ID id;
        string s, startTime;
        ValueConverter converter = new ValueConverter();
        KeyFamily _keyFamily;

        public ValueMap(KeyFamily keyFamily)
        {
            _keyFamily = keyFamily;
                        
            Map(o => o.Key).ToAttribute("concept", true)
                .Set(v => id = v)
                .Converter(new IDConverter());

            Map(o => converter.Serialize(o.Value, out startTime)).ToAttribute("value", true)
                .Set(v => s = v)
                .Converter(new StringConverter());

            Map(o => startTime).ToAttribute("startTime", false)
                .Set(v => startTime = v)
                .Converter(new StringConverter());
        }

        protected override KeyValuePair<ID, IValue> Return()
        {
            var component = _keyFamily.GetComponent(id);
            IValue value = converter.Parse(component, s, startTime);
            return new KeyValuePair<ID, IValue>(id, value);
        }
    }
}
