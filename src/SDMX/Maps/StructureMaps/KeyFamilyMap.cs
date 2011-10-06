using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class KeyFamilyMap : MaintainableArtefactMap<KeyFamily>
    {
        KeyFamily _keyFamily;
        
        
        public KeyFamilyMap(StructureMessage message)
        {          
            AttributesOrder("id" ,"agencyID", "version", "uri","isFinal","isExternalReference","validFrom","validTo");
            ElementsOrder("Name", "Description", "Components", "Annotations");
            
            var components = MapContainer("Components", false);

            components.MapCollection(o => o.Dimensions).ToElement("Dimension", false)
                .Set(v => _keyFamily.Dimensions.Add(v))
                .ClassMap(() => new DimensionMap(message));

            components.Map(o => o.TimeDimension).ToElement("TimeDimension", false)
                .Set(v => _keyFamily.TimeDimension = v)
                .ClassMap(() => new TimeDimensionMap(message));

            components.MapCollection(o => o.Groups).ToElement("Group", false)
               .ClassMap(() => new GroupDescriptorMap(_keyFamily));

            components.Map(o => o.PrimaryMeasure).ToElement("PrimaryMeasure", true)
                .Set(v => _keyFamily.PrimaryMeasure = v)
                .ClassMap(() => new PrimaryMeasureMap(message));

            components.MapCollection(o => o.XMeasures).ToElement("CrossSectionalMeasure", false)
                .Set(v => _keyFamily.XMeasures.Add(v))
                .ClassMap(() => new XMeasureMap(message));

            components.MapCollection(o => o.Attributes).ToElement("Attribute", false)
                .Set(v => _keyFamily.Attributes.Add(v))
                .ClassMap(() => new AttributeMap(message));
        }

        Id _id;
        Id _agencyId;
        bool _isFinal;
        string _version;
        TimePeriod _validTo;
        TimePeriod _validFrom;
        Uri _uri;
        bool _isExternalReference;

        protected override void SetAgencyId(Id agencyId)
        {
            _agencyId = agencyId;
        }

        protected override void SetId(Id id)
        {
            _id = id;
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
            if (_keyFamily == null)
            {
                _keyFamily = new KeyFamily(name, _id, _agencyId);
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
