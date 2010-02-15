using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class ObservationMap : AnnotableArtefactMap<Observation>
    {
        Observation _obs;

        public ObservationMap(Series series, KeyFamily keyFamily)
        {
            Map(o => o.Time).ToSimpleElement("Time", true)
                .Set(v => _obs = series[v])
                .Converter(new TimePeriodConverter());

            Map(o => o.Value).ToElement("ObsValue", true)
                .Set(v => _obs.Value = v)
                .ClassMap(() => new ObsValueMap(keyFamily));

            MapContainer("Attributes", false)
               .MapCollection(o => o.Attributes).ToElement("Value", false)
                   .Set(v => _obs.Attributes[v.Key] = v.Value)
                   .ClassMap(() => new ValueMap(keyFamily));
                
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _obs.Annotations.Add(annotation);
        }

        protected override Observation Return()
        {
            return _obs;
        }
    }
}
