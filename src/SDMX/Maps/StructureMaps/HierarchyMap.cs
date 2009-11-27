using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class HierarchyMap : VersionableArtefactMap<Hierarchy>
    {
        Hierarchy _hierarchy;

        public HierarchyMap(HierarchicalCodeList hcl)
        {
            Map<bool?>(o => o.IsFinal ? true : (bool?)null).ToAttribute("isFinal", false)
                .Set(v => _hierarchy.IsFinal = v.Value)
                .Converter(new NullableBooleanConverter());

            Map(o => o.Root).ToElement("CodeRef", true)
                .Set(v => _hierarchy.Root = v)
                .ClassMap(() => new CodeRefMap(hcl));

        }

        protected override void SetVersion(string version)
        {
            _hierarchy.Version = version;
        }

        protected override void SetValidTo(ITimePeriod validTo)
        {
            _hierarchy.ValidTo = validTo;
        }

        protected override void SetValidFrom(ITimePeriod validFrom)
        {
            _hierarchy.ValidFrom = validFrom;
        }

        protected override void SetID(ID id)
        {
            _hierarchy = new Hierarchy(id);
        }

        protected override void SetUri(Uri uri)
        {
            _hierarchy.Uri = uri;
        }

        protected override void SetName(InternationalString name)
        {
            _hierarchy.Name.Add(name);
        }

        protected override void SetDescription(InternationalString description)
        {
            _hierarchy.Description.Add(description);
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _hierarchy.Annotations.Add(annotation);
        }

        protected override Hierarchy Return()
        {
            return _hierarchy;
        }
    }
}
