using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class GenericObservationMap : AnnotableArtefactMap<Observation>
    {
        Observation _obs;
        ValueConverter converter = new ValueConverter();

        public GenericObservationMap(Series series, KeyFamily keyFamily)
        {
            string startTime = null;
            Map(o => converter.Serialize(o.Time, out startTime)).ToSimpleElement("Time", true)
                .Set(v => _obs = series.Create((TimePeriod)converter.Parse(keyFamily.TimeDimension, v, startTime)))
                .Converter(new StringConverter());

            Map(o => o.Value).ToElement("ObsValue", true)
                .Set(v => _obs.Value = v)
                .ClassMap(() => new GenericObsValueMap(keyFamily));

            MapContainer("Attributes", false)
               .MapCollection(o => o.Attributes).ToElement("Value", false)
                   .Set(v => _obs.Attributes[v.Key] = v.Value)
                   .ClassMap(() => new GenericValueMap(keyFamily));
                
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
