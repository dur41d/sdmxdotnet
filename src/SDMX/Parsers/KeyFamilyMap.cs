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
        private DSD _dsd;
        private ElementCollectionMap<KeyFamily, Dimension> _dimensions;

        public KeyFamilyMap(DSD dsd)
        {
            _dsd = dsd;

            MapAttribute<bool>("isExternalReference", false)
                .Getter(o => o.IsExternalReference)
                .Setter((o, p) => o.IsExternalReference = p)
                .Parser(s => bool.Parse(s));

            var components = MapElementContainer("Components", false);

            _dimensions = components.MapElementCollection<Dimension>("Dimension", false)
                .Parser(new DimensionMap(dsd))
                .Getter(o => o.Dimensions);                
            
            components.MapElement<TimeDimension>("TimeDimension", false)
                    .Parser(new TimeDimensionMap(dsd))
                    .Getter(o => o.TimeDimension)
                    .Setter((o, p) => o.TimeDimension = p);

            components.MapElementCollection<GroupValuesHolder>("Group", false)
                    .Parser(new GroupMap())
                    .Getter(o => GetGroupHolders(o))
                    .Setter((o, p) => BuildGroups(p, o));

            components.MapElement<PrimaryMeasure>("PrimaryMeasure", true)
                   .Parser(new PrimaryMeasureMap(dsd))
                   .Getter(o => o.PrimaryMeasure)
                   .Setter((o, p) => o.PrimaryMeasure = p);

            components.MapElementCollection<CrossSectionalMeasure>("CrossSectionalMeasure", false)
                   .Parser(new CrossSectionalMeasureMap(dsd))
                   .Getter(o => o.CrossSectionalMeasures)
                   .Setter((o, list) => list.ForEach(i => o.AddMeasure(i)));

            components.MapElementCollection<Attribute>("Attribute", false)
                   .Parser(new AttributeMap(dsd))
                   .Getter(o => o.Attributes)
                   .Setter((o,list) => list.ForEach(i => o.AddAttribute(i)));
        }

        private IEnumerable<GroupValuesHolder> GetGroupHolders(KeyFamily keyFamily)
        {
            List<GroupValuesHolder> groupHolders = new List<GroupValuesHolder>();
            foreach (var group in keyFamily.Groups)
            {
                var groupHolder = new GroupValuesHolder();
                groupHolder.ID = group.ID;
                group.Annotations.ForEach(item => groupHolder.Annotations.Add(item));
                groupHolder.AttachmentConstraintRef = group.AttachmentConstraintRef;
                group.Description.Languages.ForEach(lang => groupHolder.Description[lang] = group.Description[lang]);
                groupHolder.DimensionRefs = group.Dimensions.Select(d => d.Concept.ID).ToList();
                groupHolders.Add(groupHolder);
            }

            return groupHolders;
        }

        private void BuildGroups(IList<GroupValuesHolder> groupHolders, KeyFamily keyFamily)
        {
            foreach (var holder in groupHolders)
            {
                var group = keyFamily.CreateNewGroup(holder.ID);
                holder.DimensionRefs.ForEach(item => group.AddDimension(item));
                group.Annotations.ForEach(Item => group.Annotations.Add(Item));
                holder.Description.Languages.ForEach(lang => group.Description[lang] = holder.Description[lang]);
                group.AttachmentConstraintRef = holder.AttachmentConstraintRef;
            }
        }

        protected override KeyFamily CreateObject()
        {
            var keyFamily = new KeyFamily(_id.Value, _agencyIDMap.Value);
            _dimensions.Values.ForEach(item => keyFamily.AddDimension(item));


            return keyFamily;    
        }
    }
}
