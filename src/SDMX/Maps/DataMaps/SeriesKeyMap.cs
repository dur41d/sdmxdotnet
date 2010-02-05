using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class KeyItemMap : ClassMap<KeyItem>
    {
        ID conceptID;
        string value, startTime;


        public KeyItemMap()
        {
            Map(o => o.Concept).ToAttribute("concept", true)
                .Set(v => conceptID = v)
                .Converter(new IDConverter());

            Map(o => o.Value).ToAttribute("value", true)
                .Set(v => value = v)
                .Converter(new StringConverter());

            Map(o => o.StartTime).ToAttribute("startTime", false)
                .Set(v => startTime = v)
                .Converter(new StringConverter());
        }

        protected override KeyItem Return()
        {
            var keyItem = new KeyItem(conceptID, value);
            if (!String.IsNullOrEmpty(startTime))
            {
                keyItem.StartTime = startTime;
            }
            return keyItem;
        }
    }
    
    internal class KeyMap : ClassMap<Key>
    {
        Key key = new Key();
        public KeyMap()
        {
            MapCollection(o => o).ToElement("Value", true)
                .Set(v => key.Add(v))
                .ClassMap(() => new KeyItemMap());
        }

        protected override Key Return()
        {
            return key;
        }
    }
    
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
                        Concept = dimValue.Key.Concept.ID, 
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
