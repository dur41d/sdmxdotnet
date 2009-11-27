using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class SeriesKeyMap : ClassMap<SeriesKey>
    {
        SeriesKeyBuilder builder;

        public SeriesKeyMap(DataSet dataSet)
        {
            builder = dataSet.Series.CreateKeyBuilder();

            MapCollection(o => GetKeyValues(o)).ToElement("Value", true)
                .Set(v => builder.Add(v.Concept, v.Value))
                .ClassMap(() => new KeyValueMap());
        }

        private IEnumerable<KeyValue> GetKeyValues(SeriesKey key)
        {
            foreach (var dimValue in key)
            {
                var value = new KeyValue() 
                    { 
                        Concept = dimValue.Dimension.Concept.ID, 
                        Value = dimValue.Value.ToString() 
                    };

                // TODO: implement startTime
                yield return value;
            }
        }

        protected override SeriesKey Return()
        {
            return builder.Build();
        }
    }
}
