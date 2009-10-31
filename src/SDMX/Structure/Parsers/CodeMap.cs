using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class CodeMap : AnnotableArtefactMap<Code>
    {
        Code code;

        public CodeMap(CodeList codelist)
        {
            AttributesOrder("value", "parentCode");
            ElementsOrder("Description", "Annotations");

            Map(o => o.ID).ToAttribute("value", true)
               .Set(v => code = new Code(v))
               .Converter(new IDConverter());

            Map(o => o.ID).ToAttribute("parentCode", false)
               .Set(v => code.Parent = codelist[v])
               .Converter(new IDConverter());

            MapCollection(o => o.Description).ToElement("Description", false)
               .Set(v => code.Description.Add(v))
               .ClassMap(() => new InternationalStringMap());
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            code.Annotations.Add(annotation);
        }

        protected override Code Return()
        {
            return code;
        }
    }
}
