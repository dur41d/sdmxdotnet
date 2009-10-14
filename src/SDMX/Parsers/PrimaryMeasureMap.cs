using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class PrimaryMeasureMap : MeasureMap<PrimaryMeasure>
    {
        DSD _dsd;

        internal PrimaryMeasureMap(DSD dsd)
            : base(dsd)
        {
            _dsd = dsd;
        }

        //protected override PrimaryMeasure CreateObject()
        //{
        //    var concept = _dsd.GetConcept(conceptRef.Value, conceptAgency.Value, conceptVersion.Value, conceptSchemeRef.Value, conceptSchemeAgency.Value);
        //    var primaryMeasure = new PrimaryMeasure(concept);
        //    SetMeasureProperties(primaryMeasure);
        //    return primaryMeasure;
        //}
    }
}
