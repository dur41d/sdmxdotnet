using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class CrossSectionalMeasureMap : MeasureMap<CrossSectionalMeasure>
    {
        DSD _dsd;

        internal CrossSectionalMeasureMap(DSD dsd)
            : base(dsd)
        {
            _dsd = dsd;

            MapAttribute<ID>("measureDimension", true)
                .Getter(o => o.Dimension)
                .Setter(p => Instance.Dimension = p)
                .Parser(s => new ID(s));

            MapAttribute<ID>("code", true)
                .Getter(o => o.Code)
                .Setter(p => Instance.Code = p)
                .Parser(s => new ID(s));
        }

        // protected override CrossSectionalMeasure CreateObject()
        //{
        //    var concept = _dsd.GetConcept(conceptRef.Value, conceptAgency.Value, conceptVersion.Value, conceptSchemeRef.Value, conceptSchemeAgency.Value);
        //    var xMeasure = new CrossSectionalMeasure(concept);
        //    SetMeasureProperties(xMeasure);
        //    return xMeasure;
        //}
    }
}
