using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class ConceptMap : VersionableArtefactMap<Concept>
    {
        Concept _concept;
        
        public ConceptMap()
        {
            AttributesOrder("id", "agencyID", "version", "uri", "isExternalReference", "validFrom", "validTo");
            ElementsOrder("Name", "Description", "TextFormat", "Annotations");

            Map(o => o.AgencyID).ToAttribute("agencyID", true)
                .Set(v => _agencyID = v)
                .Converter(new IDConverter());

            Map(o => o.IsExternalReference).ToAttribute("isExternalReference", false)
               .Set(v => _isExternalReference = v)
               .Converter(new BooleanConverter());

            Map(o => o.TextFormat).ToElement("TextFormat", false)
                .Set(v => _concept.TextFormat = v)
                .ClassMap(new TextFormatMap());
        }

        ID _id;
        ID _agencyID;
        string _version;
        TimePeriod _validTo;
        TimePeriod _validFrom;
        Uri _uri;
        bool _isExternalReference;       

        protected override void SetID(ID id)
        {
            _id = id;
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
            if (_concept == null)
            {
                _concept = new Concept(name, _id, _agencyID);
                _concept.Version = _version;
                _concept.ValidFrom = _validFrom;
                _concept.ValidTo = _validTo;
                _concept.Uri = _uri;
                _concept.IsExternalReference = _isExternalReference;
            }
            else
            {
                _concept.Name.Add(name);
            }
        }

        protected override void SetDescription(InternationalString description)
        {
            _concept.Description.Add(description);
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _concept.Annotations.Add(annotation);
        }

        protected override Concept Return()
        {
            return _concept;
        }
    }
}
