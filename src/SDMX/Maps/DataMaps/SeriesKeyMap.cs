using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class KeyItemMap : ClassMap<KeyValuePair<ID, IValue>>
    {
        ID conceptID;
        string value, startTime;


        public KeyItemMap()
        {
            Map(o => o.Key).ToAttribute("concept", true)
                .Set(v => conceptID = v)
                .Converter(new IDConverter());

            //Map(o => o.Value.ToString()).ToAttribute("value", true)
            //    .Set(v => value = v)
            //    .Converter(new StringConverter());

            //Map(o => o.Value.ToString()).ToAttribute("startTime", false)
            //    .Set(v => startTime = v)
            //    .Converter(new StringConverter());
        }

        protected override KeyValuePair<ID, IValue> Return()
        {
            throw new NotImplementedException();
        }
    }
    
    internal class KeyMap : ClassMap<Key>
    {
        Key key = new Key();
        public KeyMap()
        {
            //MapCollection(o => o).ToElement("Value", true)
            //    .Set(v => key.Add(v))
            //    .ClassMap(() => new KeyItemMap());
        }

        protected override Key Return()
        {
            return key;
        }
    }

    internal class SeriesKeyMap : ClassMap<Key>
    {
      //  SeriesKeyBuilder builder;

        public SeriesKeyMap(DataSet dataSet)
        {
            //builder = dataSet.Series.CreateKeyBuilder();

            //MapCollection(o => GetKeyValues(o)).ToElement("Value", true)
            //    .Set(v => builder.Add(v.Concept, v.Value))
            //    .ClassMap(() => new KeyValueMap());
        }

        //private IEnumerable<KeyValue> GetKeyValues(SeriesKey key)
        //{
        //    foreach (var dimValue in key)
        //    {
        //        var value = new KeyValue()
        //            {
        //                Concept = dimValue.Key.Concept.ID,
        //                Value = dimValue.Value.ToString()
        //            };

        //        // TODO: implement startTime
        //        yield return value;
        //    }

        //}

        protected override Key Return()
        {
            throw new NotImplementedException();
        }
    }
}
