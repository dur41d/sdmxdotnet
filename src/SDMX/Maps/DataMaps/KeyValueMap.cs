using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class KeyValueMap : ClassMap<KeyValue>
    {
        KeyValue _value = new KeyValue();

        public KeyValueMap()
        {
            Map(o => o.Concept).ToAttribute("concept", true)
                .Set(v => _value.Concept = v)
                .Converter(new IDConverter());

            Map(o => o.Value).ToAttribute("value", true)
                .Set(v => _value.Value = v)
                .Converter(new StringConverter());

            Map(o => o.StartTime).ToAttribute("startTime", false)
                .Set(v => _value.StartTime = v)
                .Converter(new StringConverter());
        }

        protected override KeyValue Return()
        {
            return _value;
        }
    }
}
