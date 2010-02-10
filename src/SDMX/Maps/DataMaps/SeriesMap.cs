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

        public SeriesMap(DataSet dataSet)
        {
            ElementsOrder("SeriesKey", "Attributes", "Obs", "Annotations");

            //Map(o => o.Key).ToElement("SeriesKey", true)
            //    .Set(v => _series = dataSet.Series.Get(v))
            //    .ClassMap(() => new KeyMap());

            //MapContainer(Namespaces.Generic + "Attributes", false)
            //    .MapCollection(o => GetKeyValues(o.Attributes)).ToElement("Value", false)
            //        .Set(v => dataSet.Attributes[v.Concept].Parse(v.Value))
            //        .ClassMap(() => new KeyValueMap());;

            MapCollection(o => o).ToElement("Obs", true)
                //.Set(v => dataSet.Series[key][")
                .ClassMap(() => new ObservationMap(dataSet));
        }

        //private IEnumerable<KeyValue> GetKeyValues(AttributeValueCollection attributes)
        //{
        //    foreach (var attribute in attributes)
        //    {
        //        var value = new KeyValue() { Concept = attribute.Attribute.Concept.ID, Value = attribute.Value.ToString() };
        //        // TODO: implement startTime
        //        yield return value;
        //    }
        //}

        protected override Series Return()
        {
            return _series;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _series.Annotations.Add(annotation);
        }
    }
}
