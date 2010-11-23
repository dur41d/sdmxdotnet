using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class CompactSeriesKeyMap : AttributeGroupTypeMap<ReadOnlyKey>
    {
        Key _key;
        ValueConverter converter = new ValueConverter();

        public CompactSeriesKeyMap(DataSet dataSet)
        {
            _key = dataSet.NewKey();
            string startTime = null;

            foreach (var dim in dataSet.KeyFamily.Dimensions)
            {
                var anchor = dim;
                MapAttribute(o => converter.Serialize(o[anchor.Concept.ID], out startTime), anchor.Concept.ID.ToString(), true)
                    .Set(v => _key[anchor.Concept.ID] = converter.Parse(anchor, v, null))
                    .Converter(new StringConverter());
            }
        }

        protected override ReadOnlyKey Return()
        {
            return new ReadOnlyKey(_key);
        }
    }
}
