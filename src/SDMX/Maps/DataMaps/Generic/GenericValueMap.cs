using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class GenericValueMap : ClassMap<IDValuePair>
    {
        ID id;
        string s, startTime;
        ValueConverter converter = new ValueConverter();
        KeyFamily _keyFamily;

        public GenericValueMap(KeyFamily keyFamily)
        {
            _keyFamily = keyFamily;
                        
            Map(o => o.ID).ToAttribute("concept", true)
                .Set(v => id = v)
                .Converter(new IDConverter());

            Map(o => converter.Serialize(o.Value, out startTime)).ToAttribute("value", true)
                .Set(v => s = v)
                .Converter(new StringConverter());

            Map(o => startTime).ToAttribute("startTime", false)
                .Set(v => startTime = v)
                .Converter(new StringConverter());
        }

        protected override IDValuePair Return()
        {
            var component = _keyFamily.GetComponent(id);
            Value value = converter.Parse(component, s, startTime);
            return new IDValuePair(id, value);
        }
    }
}
