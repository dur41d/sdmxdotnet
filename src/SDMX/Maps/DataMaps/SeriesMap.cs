using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class SeriesMap : AnnotableArtefactMap<Series>
    {
        Series _series;
        List<Observation> observations = new List<Observation>();

        public SeriesMap(DataSet dataSet)
        {
            ElementsOrder("SeriesKey", "Attributes", "Obs", "Annotations");

            Map(o => o.Key).ToElement("SeriesKey", true)
                .Set(v => _series = dataSet.Series[v])
                .ClassMap(() => new KeyMap(dataSet));

            MapContainer("Attributes", false)
                .MapCollection(o => o.Attributes).ToElement("Value", false)
                    .Set(v => _series.Attributes[v.Key] = v.Value)
                    .ClassMap(() => new ValueMap(dataSet.KeyFamily));

            MapCollection(o => o).ToElement("Obs", true)
                .Set(v => observations.Add(v))
                .ClassMap(() => new ObservationMap(_series, dataSet.KeyFamily));
        }
       
        protected override Series Return()
        {
            observations.ForEach(obs => _series.Add(obs));
            return _series;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _series.Annotations.Add(annotation);
        }
    }
}
