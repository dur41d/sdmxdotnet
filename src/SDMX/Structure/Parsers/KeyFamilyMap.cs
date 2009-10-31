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
                .Set(v => _keyFamily.AddDimension(v))
                .ClassMap(() => new DimensionMap(dsd));

            components.Map(o => o.TimeDimension).ToElement("TimeDimension", false)
                .Set(v => _keyFamily.TimeDimension = v)
                .ClassMap(new TimeDimensionMap(dsd));

            components.MapCollection(o => o.Groups).ToElement("Group", false)
               .ClassMap(() => new GroupMap(_keyFamily));

            components.Map(o => o.PrimaryMeasure).ToElement("PrimaryMeasure", true)
                .Set(v => _keyFamily.PrimaryMeasure = v)
                .ClassMap(new PrimaryMeasureMap(dsd));

            components.MapCollection(o => o.CrossSectionalMeasures).ToElement("CrossSectionalMeasure", false)
                .Set(v => _keyFamily.AddMeasure(v))
                .ClassMap(() => new CrossSectionalMeasureMap(dsd));

            components.MapCollection(o => o.Attributes).ToElement("Attribute", false)
                .Set(v => _keyFamily.AddAttribute(v))
                .ClassMap(() => new AttributeMap(dsd));
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

        protected override void SetName(InternationalString name)
        {
            if (_keyFamily == null)
            {
                _keyFamily = new KeyFamily(name, _id, _agencyID);
                _keyFamily.IsFinal = _isFinal;
                _keyFamily.Version = _version;
                _keyFamily.ValidFrom = _validFrom;
                _keyFamily.ValidTo = _validTo;
                _keyFamily.Uri = _uri;
                _keyFamily.IsExternalReference = _isExternalReference;
            }
            else
            {
                _keyFamily.Name.Add(name);
            }
        }

        protected override void SetDescription(InternationalString description)
        {
            _keyFamily.Description.Add(description);
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _keyFamily.Annotations.Add(annotation);
        }

        protected override KeyFamily Return()
        {
            return _keyFamily;
        }
    }
}
