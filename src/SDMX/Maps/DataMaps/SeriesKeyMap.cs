using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class ValueConverter
    {        
        private Dictionary<Type, IValueConverter> registry = new Dictionary<Type, IValueConverter>();

        public ValueConverter()
        {            
            registry.Add(typeof(Code), new CodeValueConverter());
            registry.Add(typeof(StringValue), new StringValueConverter());
            registry.Add(typeof(DecimalValue), new DecimalValueConverter());
            registry.Add(typeof(YearTimePeriod), new YearValueConverter());
        }

        public IValue Parse(Component component, string s, string startTime)
        {
            Type valueType = component.GetValueType();
            var converter = registry[valueType];
            return converter.Parse(component, s, startTime);
        }

        public string Serialize(IValue value, out string startTime)
        {
            var converter = registry[value.GetType()];
            return converter.Serialize(value, out startTime);
        }
    }

    internal interface IValueConverter
    {
        IValue Parse(Component component, string s, string startTime);
        string Serialize(IValue value, out string startTime);
    }

    internal class CodeValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            return component.CodeList.Get(s);
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            return ((Code)value).ID.ToString();
        }
    }

    internal class StringValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            return new StringValue(s);
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            return ((StringValue)value).ToString();
        }
    }

    internal class DecimalValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            return new DecimalValue(decimal.Parse(s));
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            return ((DecimalValue)value).ToString();
        }
    }

    internal class YearValueConverter : IValueConverter
    {
        public IValue Parse(Component component, string s, string startTime)
        {
            return new YearTimePeriod(int.Parse(s));
        }

        public string Serialize(IValue value, out string startTime)
        {
            startTime = null;
            DateTimeOffset time = (DateTimeOffset)(YearTimePeriod)value;
            return time.Year.ToString();
        }
    }



    internal class ObsValueMap : ClassMap<IValue>
    {
        string s, startTime;
        ValueConverter converter = new ValueConverter();
        KeyFamily _keyFamily;

        public ObsValueMap(KeyFamily keyFamily)
        {
            _keyFamily = keyFamily;

            Map(o => converter.Serialize(o, out startTime)).ToAttribute("value", true)
                .Set(v => s = v)
                .Converter(new StringConverter());

            Map(o => startTime).ToAttribute("startTime", false)
                .Set(v => startTime = v)
                .Converter(new StringConverter());
        }

        protected override IValue Return()
        {
            var component = _keyFamily.PrimaryMeasure;
            IValue value = converter.Parse(component, s, startTime);
            return value;
        }
    }


    internal class ValueMap : ClassMap<KeyValuePair<ID, IValue>>
    {
        ID id;
        string s, startTime;
        ValueConverter converter = new ValueConverter();
        KeyFamily _keyFamily;

        public ValueMap(KeyFamily keyFamily)
        {
            _keyFamily = keyFamily;

            Map(o => o.Key).ToAttribute("concept", true)
                .Set(v => id = v)
                .Converter(new IDConverter());

            Map(o => converter.Serialize(o.Value, out startTime)).ToAttribute("value", true)
                .Set(v => s = v)
                .Converter(new StringConverter());

            Map(o => startTime).ToAttribute("startTime", false)
                .Set(v => startTime = v)
                .Converter(new StringConverter());
        }

        protected override KeyValuePair<ID, IValue> Return()
        {
            var component = _keyFamily.GetComponent(id);
            IValue value = converter.Parse(component, s, startTime);
            return new KeyValuePair<ID, IValue>(id, value);
        }
    }
    
    internal class KeyMap : ClassMap<ReadOnlyKey>
    {
        Key key;
        public KeyMap(DataSet dataSet)
        {
            key = dataSet.NewKey();

            MapCollection(o => o).ToElement("Value", true)
                .Set(v => key[v.Key] = v.Value)
                .ClassMap(() => new ValueMap(dataSet.KeyFamily));
        }

        protected override ReadOnlyKey Return()
        {
            return new ReadOnlyKey(key);
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
