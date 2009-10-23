using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    
    public class KeyFamilyMap : MaintainableArtefactMap<KeyFamily>
    {
        KeyFamily _keyFamily;
        
        
        public KeyFamilyMap(DSD dsd)
        {          
            AttributesOrder("id" ,"agencyID", "version", "uri","isFinal","isExternalReference","validFrom","validTo");
            ElementsOrder("Name", "Description", "Components", "Annotations");

            Map(o => o.IsExternalReference).ToAttribute("isExternalReference", false)
                .Set(v => _isExternalReference = v)
                .Converter(new BooleanConverter());            
            
            var components = MapContainer("Components", false);

            components.MapCollection(o => o.Dimensions).ToElement("Dimension", false)
                .Set(v => v.ForEach(d => _keyFamily.AddDimension(d)))
                .ClassMap(new DimensionMap(dsd));

            components.Map(o => o.TimeDimension).ToElement("TimeDimension", false)
                .Set(v => _keyFamily.TimeDimension = v)
                .ClassMap(new TimeDimensionMap(dsd));

            components.MapCollection(o => o.Groups).ToElement("Group", false)
               .ClassMap(new GroupMap(_keyFamily));

            components.Map(o => o.PrimaryMeasure).ToElement("PrimaryMeasure", true)
                .Set(v => _keyFamily.PrimaryMeasure = v)
                .ClassMap(new PrimaryMeasureMap(dsd));

            components.MapCollection(o => o.CrossSectionalMeasures).ToElement("CrossSectionalMeasure", false)
                .Set(v => v.ForEach(d => _keyFamily.AddMeasure(d)))
                .ClassMap(new CrossSectionalMeasureMap(dsd));

            components.MapCollection(o => o.Attributes).ToElement("Attribute", false)
                .Set(v => v.ForEach(d => _keyFamily.AddAttribute(d)))
                .ClassMap(new AttributeMap(dsd));
        }

        ID _id;
        ID _agencyID;
        bool _isFinal;
        string _version;
        TimePeriod _validTo;
        TimePeriod _validFrom;
        Uri _uri;
        bool _isExternalReference;

        protected override void SetAgencyID(ID agencyId)
        {
            _agencyID = agencyId;
        }

        protected override void SetID(ID id)
        {
            _id = id;
        }

        protected override void SetIsFinal(bool isFinal)
        {
            _isFinal = isFinal;
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

        protected override void SetName(IEnumerable<InternationalString> name)
        {
            _keyFamily = new KeyFamily(name.First(), _id, _agencyID);
            _keyFamily.IsFinal = _isFinal;
            _keyFamily.Version = _version;
            _keyFamily.ValidFrom = _validFrom;
            _keyFamily.ValidTo = _validTo;
            _keyFamily.Uri = _uri;
            _keyFamily.IsExternalReference = _isExternalReference;

            name.Skip(1).ForEach(i => _keyFamily.Name.Add(i));
        }

        protected override void SetDescription(IEnumerable<InternationalString> description)
        {
            description.ForEach(i => _keyFamily.Description.Add(i));
        }

        protected override void SetAnnotations(IEnumerable<Annotation> annotations)
        {
            annotations.ForEach(i => _keyFamily.Annotations.Add(i));
        }

        protected override KeyFamily Return()
        {
            return _keyFamily;
        }
    }
}
