using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class GenericObsValueMap : ClassMap<IValue>
    {
        string s, startTime;
        ValueConverter converter = new ValueConverter();
        KeyFamily _keyFamily;

        public GenericObsValueMap(KeyFamily keyFamily)
        {
            _keyFamily = keyFamily;

            Map(o => converter.Serialize(o, out startTime)).ToAttribute("value", true)
                .Set(v => s = v)
                .Converter(new StringConverter());

            Map(o => startTime).ToAttribute("startTime", false)
                .Set(v => startTime = v)
                .Converter(new StringConverter());
        }

        protected override IValue Return()
        {
            var component = _keyFamily.PrimaryMeasure;
            IValue value = converter.Parse(component, s, startTime);
            return value;
        }
    }
}
