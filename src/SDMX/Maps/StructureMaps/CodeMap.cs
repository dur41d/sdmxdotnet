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

            Map(o => o.ID).ToAttribute("value", true)
               .Set(v => code = new Code(v))
               .Converter(new IDConverter());

            Map(o => GetParentID(o)).ToAttribute("parentCode", false)
               .Set(v => SetParentID(v.Value, codeList))
               .Converter(new NullableIDConverter());

            MapCollection(o => o.Description).ToElement("Description", false)
               .Set(v => code.Description.Add(v))
               .ClassMap(() => new InternationalStringMap());
        }

        private ID? GetParentID(Code code)
        {
            return code.Parent == null ? null : (ID?)code.Parent.ID;
        }

        private void SetParentID(ID parentID, CodeList codeList)
        {
            if (parentID != null)
                code.Parent = codeList.Get(parentID);
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
