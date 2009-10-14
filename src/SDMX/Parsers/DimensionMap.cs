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
              .Setter(p => Instance.IsMeasureDimension = p)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isFrequencyDimension", false, false)
              .Getter(o => o.IsFrequencyDimension)
              .Setter(p => Instance.IsFrequencyDimension = p)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isEntityDimension", false, false)
              .Getter(o => o.IsEntityDimension)
              .Setter(p => Instance.IsEntityDimension = p)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isCountDimension", false, false)
              .Getter(o => o.IsCountDimension)
              .Setter(p => Instance.IsCountDimension = p)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isNonObservationTimeDimension", false, false)
              .Getter(o => o.IsNonObservationTimeDimension)
              .Setter(p => Instance.IsNonObservationTimeDimension = p)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("isIdentityDimension", false, false)
              .Getter(o => o.IsIdentityDimension)
              .Setter(p => Instance.IsIdentityDimension = p)
              .Parser(s => bool.Parse(s));
        }

        //protected override Dimension CreateObject()
        //{
        //    var concept = _dsd.GetConcept(conceptRef.Value, conceptAgency.Value, conceptVersion.Value, conceptSchemeRef.Value, conceptSchemeAgency.Value);

        //    var dimension = new Dimension(concept);

        //    SetComponentProperties(dimension);

        //    return dimension;
        //}       

        protected override Dimension Create(Concept conecpt)
        {
            throw new NotImplementedException();
        }
    }
}