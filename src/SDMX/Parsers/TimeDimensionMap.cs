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
    public class TimeDimensionMap : CompoenentMap<TimeDimension>
    {
        DSD _dsd;

        public TimeDimensionMap(DSD dsd)
            : base(dsd)
        {
            _dsd = dsd;
        }

        protected override TimeDimension CreateObject()
        {
            var concept = _dsd.GetConcept(conceptRef.Value, conceptAgency.Value,
                conceptVersion.Value, conceptSchemeRef.Value, conceptSchemeAgency.Value);

            var timeDimension = new TimeDimension(concept);

            SetComponentProperties(timeDimension);

            return timeDimension;
        }
    }
}
