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

        public CodeMap(CodeList codeList)
        {
            AttributesOrder("value", "parentCode");
            ElementsOrder("Description", "Annotations");

            Map(o => o.Id).ToAttribute("value", true)
               .Set(v => code = new Code(v))
               .Converter(new IdConverter());

            Map(o => GetParentId(o)).ToAttribute("parentCode", false)
               .Set(v => SetParentId(v, codeList))
               .Converter(new IdConverter());

            int count = 0;
            MapCollection(o => { count = o.Description.Count(); return o.Description; }).ToElement("Description", false)
               .Set(v => code.Description.Add(v))
               .ClassMap(() => new InternationalStringMap(count));
        }

        private Id GetParentId(Code code)
        {
            return code.Parent == null ? null : code.Parent.Id;
        }

        private void SetParentId(Id parentId, CodeList codeList)
        {
            if (parentId != null)
                code.Parent = codeList.Get(parentId);
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
