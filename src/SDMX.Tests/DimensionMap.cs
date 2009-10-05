using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace SDMX.Tests
{
  
    public class DimensionMap : ClassMap<Dimension>
    {
        private DSD dsd;
        private AttributeMap<Dimension, ID> conceptRef;
        private AttributeMap<Dimension, ID> conceptAgency;

        public override string ElementName
        {
            get
            {
                return "Dimension";
            }
        }

        public DimensionMap(DSD dsd)
        {   
            this.dsd = dsd;

            conceptRef = MapAttribute<ID>("conceptRef", true)
                .Getter(d => d.Concept.Id)
                .Parser(s => s);

            conceptAgency = MapAttribute<ID>("conceptAgency", false)
               .Getter(d => d.Concept.Id)
               .Parser(s => s);

            MapAttribute<bool>("isMeasureDimension", false, false)
                .Getter(d => d.IsMeasureDimension)
                .Setter((d, b) => d.IsMeasureDimension = b)
                .Parser(s => bool.Parse(s));

            MapElement<TextFormat>("TextFormat", 0, 1)
                .Using(new TextFormatMap());
        }

        protected override Dimension CreateObject()
        {
            var concept = dsd.GetConcept(conceptRef.Value, conceptAgency.Value);
            var dimension = new Dimension(concept);
            return dimension;

        }
    }
}
