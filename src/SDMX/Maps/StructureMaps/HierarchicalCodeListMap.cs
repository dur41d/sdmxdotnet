using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class HierarchicalCodeListMap : MaintainableArtefactMap<HierarchicalCodeList>
    {
        HierarchicalCodeList hcl;
        Id _id;

        public HierarchicalCodeListMap()
        {
            MapCollection(o => o.CodeListRefs).ToElement("CodelistRef", false)
                .Set(v => hcl.AddCodeList(v))
                .ClassMap(() => new CodeListRefMap());

            MapCollection(o => o).ToElement("Hierarchy", false)
                .Set(v => hcl.Add(v))
                .ClassMap(() => new HierarchyMap(hcl));

        }

        protected override void SetId(Id id)
        {
            _id = id;
        }

        protected override void SetAgencyId(Id agencyId)
        {
            hcl = new HierarchicalCodeList(_id, agencyId);
        }

        protected override void SetIsFinal(bool isFinal)
        {
            hcl.IsFinal = isFinal;
        }

        protected override void SetIsExternalReference(bool isExternalReference)
        {
            hcl.IsExternalReference = isExternalReference;
        }

        protected override void SetVersion(string version)
        {
            hcl.Version = version;
        }

        protected override void SetValidTo(TimePeriod validTo)
        {
            hcl.ValidTo = validTo;
        }

        protected override void SetValidFrom(TimePeriod validFrom)
        {
            hcl.ValidFrom = validFrom;
        }

        protected override void SetUri(Uri uri)
        {
            hcl.Uri = uri;
        }

        protected override void SetName(InternationalString name)
        {
            hcl.Name.Add(name);
        }

        protected override void SetDescription(InternationalString description)
        {
            hcl.Description.Add(description);
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            hcl.Annotations.Add(annotation);
        }

        protected override HierarchicalCodeList Return()
        {
            return hcl;
        }
    }
}
