using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class ConceptSchemeMap : MaintainableArtefactMap<ConceptScheme>
    {
        ConceptScheme _conceptScheme;

        public ConceptSchemeMap()
        {
            AttributesOrder("id", "agencyID", "version", "uri", "isExternalReference", "isFinal", "validFrom", "validTo");
            ElementsOrder("Name", "Description", "Concept", "Annotations");

            MapCollection(o => o).ToElement(Namespaces.Structure + "Concept", false)
                    .Set(v => _conceptScheme.Add(v))
                    .ClassMap(() => new ConceptMap());
        }

        Id _id;
        Id _agencyId;
        string _version;
        TimePeriod _validTo;
        TimePeriod _validFrom;
        Uri _uri;
        bool _isExternalReference;
        bool _isFinal;

        protected override void SetId(Id id)
        {
            _id = id;
        }

        protected override void SetAgencyId(Id agencyId)
        {
            _agencyId = agencyId;
        }

        protected override void SetIsFinal(bool isFinal)
        {
            _isFinal = isFinal;
        }

        protected override void SetIsExternalReference(bool isExternalReference)
        {
            _isExternalReference = isExternalReference;
        }

        protected override void SetVersion(string version)
        {
            _version = version;
        }

        protected override void SetValidTo(TimePeriod validTo)
        {
            _validTo = validTo;
        }

        protected override void SetValidFrom(TimePeriod validFrom)
        {
            _validFrom = validFrom;
        }

        protected override void SetUri(Uri uri)
        {
            _uri = uri;
        }

        protected override void SetName(InternationalString name)
        {
            if (_conceptScheme == null)
            {
                _conceptScheme = new ConceptScheme(name, _id, _agencyId);                
                _conceptScheme.Version = _version;
                _conceptScheme.ValidFrom = _validFrom;
                _conceptScheme.ValidTo = _validTo;
                _conceptScheme.Uri = _uri;
                _conceptScheme.IsExternalReference = _isExternalReference;
                _conceptScheme.IsFinal = _isFinal;
            }
            else
            {
                _conceptScheme.Name.Add(name);
            }
        }

        protected override void SetDescription(InternationalString description)
        {
            _conceptScheme.Description.Add(description);
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _conceptScheme.Annotations.Add(annotation);
        }

        protected override ConceptScheme Return()
        {
            return _conceptScheme;
        }
    }
}
