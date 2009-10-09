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
    internal class AttributeMap : CompoenentMap<Attribute>
    {
        private DSD _dsd;
        private ElementCollectionMap<Attribute, ID> _attachmentGroupIDs;

        public AttributeMap(DSD dsd)
            : base(dsd)
        {
            _dsd = dsd;

            MapAttribute<AttachmentLevel>("attachmentLevel", true)
              .Getter(o => o.AttachementLevel)
              .Setter((o, p) => o.AttachementLevel = p)
              .Parser(s => (AttachmentLevel)Enum.Parse(typeof(AttachmentLevel), s));

            MapAttribute<AssignmentStatus>("assignmentStatus", true)
             .Getter(o => o.AssignmentStatus)
             .Setter((o, p) => o.AssignmentStatus = p)
             .Parser(s => (AssignmentStatus)Enum.Parse(typeof(AssignmentStatus), s));

            MapAttribute<bool>("isTimeFormat", false, false)
              .Getter(o => o.IsTimeFormat)
              .Setter((d, b) => d.IsTimeFormat = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isEntityAttribute", false, false)
              .Getter(o => o.IsEntityAttribute)
              .Setter((d, b) => d.IsEntityAttribute = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isNonObservationalTimeAttribute", false, false)
              .Getter(o => o.IsNonObservationalTimeAttribute)
              .Setter((d, b) => d.IsNonObservationalTimeAttribute = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isCountAttribute", false, false)
              .Getter(o => o.IsCountAttribute)
              .Setter((d, b) => d.IsCountAttribute = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("IsFrequencyAttribute", false, false)
              .Getter(o => o.IsFrequencyAttribute)
              .Setter((d, b) => d.IsFrequencyAttribute = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isIdentityAttribute", false, false)
             .Getter(o => o.IsIdentityAttribute)
             .Setter((d, b) => d.IsIdentityAttribute = b)
             .Parser(s => bool.Parse(s));

            MapElementCollection<ID>("AttachmentGroup", false)
                .Getter(o => o.AttachmentGroups)
                .Setter((o,list) => list.ForEach(i => o.AttachmentGroups.Add(i)))
                .Parser(new ValueElementMap<ID>(s => new ID(s)));

            MapElementCollection<ID>("AttachmentMeasure", false)
                .Getter(o => o.AttachmentMeasures)
                .Setter((o, list) => list.ForEach(i => o.AttachmentMeasures.Add(i)))
                .Parser(new ValueElementMap<ID>(s => new ID(s)));
        }

        protected override Attribute CreateObject()
        {
            var concept = _dsd.GetConcept(conceptRef.Value, conceptAgency.Value, conceptVersion.Value, conceptSchemeRef.Value, conceptSchemeAgency.Value);

            var attribute = new Attribute(concept);

            SetComponentProperties(attribute);

            return attribute;
        }
    }
}
