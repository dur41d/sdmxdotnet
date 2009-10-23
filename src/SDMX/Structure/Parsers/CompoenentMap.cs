using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using OXM;

namespace SDMX.Parsers
{   
    public abstract class CompoenentMap<T> : AnnotableArtefactMap<T>
            where T : Component
    {
        protected abstract T Create(Concept conecpt);

        T _component;

        public CompoenentMap(DSD dsd)
        {
            Map(o => ConceptRef.Create(o.Concept)).ToAttributeGroup("conceptRef")
                .Set(v => _component = Create(dsd.GetConcept(v)))
                .GroupTypeMap(new ConceptRefMap());

            Map(o => CodelistRef.Create(o.CodeList)).ToAttributeGroup("codelistRef")
                .Set(v => _component.CodeList = dsd.GetCodeList(v))
                .GroupTypeMap(new CodelistRefMap());

            Map(o => o.CrossSectionalAttachmentLevel).ToAttributeGroup("crossSectionalAttachmentLevel", CrossSectionalAttachmentLevel.None)
                .Set(v => _component.CrossSectionalAttachmentLevel = v)
                .GroupTypeMap(new CrossSectionalAttachmentLevelMap());

            Map(o => o.TextFormat).ToElement("TextFormat", false)
                .Set(v => _component.TextFormat = v)
                .ClassMap(new TextFormatMap());
        }
    }
}
