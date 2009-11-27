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
        Language lang;
        string value;

        public InternationalStringMap()
        {
            Map(o => o.Language).ToAttribute(XNamespace.Xml + "lang", false, "en")
                .Set(v => lang = v)
                .Converter(new LanguageConverter());

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
