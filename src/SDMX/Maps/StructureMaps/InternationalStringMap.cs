using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    public class InternationalStringMap : ClassMap<InternationalString>
    {        
        string lang;
        string value;

        public InternationalStringMap(int count)
        {
            Map(o => o.Language).ToAttribute(XNamespace.Xml + "lang", false, "en", count > 1)
                .Set(v => lang = v)
                .Converter(new StringConverter());

            Map(o => o.Value).ToContent()
                .Set(v => value = v)
                .Converter(new StringConverter());
        }


        protected override InternationalString Return()
        {
            return new InternationalString(lang, value);
        }
    }
}
