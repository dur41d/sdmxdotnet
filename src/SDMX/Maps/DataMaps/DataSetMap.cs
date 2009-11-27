using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class DataSetMap : ClassMap<DataSet>
    {
        DataSet _dataSet;

        public DataSetMap(KeyFamily keyFamily)
        {
            _dataSet = new DataSet(keyFamily);

            Map(o => o.KeyFamily.ID).ToSimpleElement(Namespaces.Generic + "KeyFamilyRef", true)
                .Set(id => VerifyKeyFamilyRef(id))
                .Converter(new IDConverter());

            MapContainer(Namespaces.Generic + "Attributes", false)
                .MapCollection(o => GetKeyValues(o.Attributes)).ToElement("Value", false)
                    .Set(v => _dataSet.Attributes[v.Concept].Parse(v.Value))
                    .ClassMap(() => new KeyValueMap());

            MapCollection(o => o.Series).ToElement(Namespaces.Generic + "Series", false)
                .ClassMap(() => new SeriesMap(_dataSet));
        }

        private IEnumerable<KeyValue> GetKeyValues(AttributeValueCollection attributes)
        {
            foreach (var attribute in attributes)
            {
                var value = new KeyValue() { Concept = attribute.Attribute.Concept.ID, Value = attribute.Value.ToString() };
                // TODO: implement startTime
                yield return value;
            }
        }

        private void VerifyKeyFamilyRef(ID id)
        {
            if (id != _dataSet.KeyFamily.ID)
            {
                throw new SDMXException("KeyFamilyRef '{0}' is not the same as the dataset key family id '{1}'", id, _dataSet.KeyFamily.ID);
            }
        }

        protected override DataSet Return()
        {
            return _dataSet;
        }
    }
}
