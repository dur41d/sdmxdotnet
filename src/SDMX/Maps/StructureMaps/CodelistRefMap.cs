using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class CodeListRefMap : ClassMap<CodeListRef>
    {
        CodeListRef codeListRef = new CodeListRef();

        public CodeListRefMap()
        {
            Map<Uri>(o => null).ToSimpleElement("URN", false)
                .Converter(new UriConverter());

            Map(o => o.AgencyId).ToSimpleElement("AgencyID", false)
                .Set(v => codeListRef.AgencyId = v)
                .Converter(new IdConverter());

            Map(o => o.Id).ToSimpleElement("CodelistID", false)
                .Set(v => codeListRef.Id = v)
                .Converter(new IdConverter());

            Map(o => o.Version).ToSimpleElement("Version", false)
                .Set(v => codeListRef.Version = v)
                .Converter(new StringConverter());

            Map(o => o.Alias).ToSimpleElement("Alias", false)
                .Set(v => codeListRef.Alias = v)
                .Converter(new IdConverter());
        }

        protected override CodeListRef Return()
        {
            return codeListRef;
        }
    }
}
