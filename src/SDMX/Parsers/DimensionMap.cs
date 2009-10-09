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
    
    public class DimensionMap : CompoenentMap<Dimension>
    {
        private DSD _dsd;
        

        public DimensionMap(DSD dsd)
            : base(dsd)
        {   
            _dsd = dsd;

            MapAttribute<bool>("isMeasureDimension", false, false)
              .Getter(o => o.IsMeasureDimension)
              .Setter((d, b) => d.IsMeasureDimension = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isFrequencyDimension", false, false)
              .Getter(o => o.IsFrequencyDimension)
              .Setter((d, b) => d.IsFrequencyDimension = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isEntityDimension", false, false)
              .Getter(o => o.IsEntityDimension)
              .Setter((d, b) => d.IsEntityDimension = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isCountDimension", false, false)
              .Getter(o => o.IsCountDimension)
              .Setter((d, b) => d.IsCountDimension = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isNonObservationTimeDimension", false, false)
              .Getter(o => o.IsNonObservationTimeDimension)
              .Setter((d, b) => d.IsNonObservationTimeDimension = b)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isIdentityDimension", false, false)
              .Getter(o => o.IsIdentityDimension)
              .Setter((d, b) => d.IsIdentityDimension = b)
              .Parser(s => bool.Parse(s));
        }

        protected override Dimension CreateObject()
        {
            var concept = _dsd.GetConcept(conceptRef.Value, conceptAgency.Value, conceptVersion.Value, conceptSchemeRef.Value, conceptSchemeAgency.Value);

            var dimension = new Dimension(concept);

            SetComponentProperties(dimension);

            return dimension;
        }       
    }
}