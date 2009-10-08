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
    public class DimensionMap : ClassMap<Dimension>
    {
        private DSD dsd;
        private AttributeMap<Dimension, ID> conceptRef;
        private AttributeMap<Dimension, ID> conceptAgency;

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

            MapElement<TextFormat>("TextFormat", false)
                .Parser(new TextFormatMap())
                .Getter(o => o.TextFormat)
                .Setter((o, p) => o.TextFormat = p);
        }

        protected override Dimension CreateObject()
        {
            var concept = dsd.GetConcept(conceptRef.Value, conceptAgency.Value);
            var dimension = new Dimension(concept);
            return dimension;

        }
    }

    public class DSD
    {
        public Concept GetConcept(ID concept, ID conceptAgency)
        {
            return new Concept(concept);
        }
    }
}
