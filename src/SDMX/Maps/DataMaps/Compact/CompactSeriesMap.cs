using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class CompactSeriesMap : ClassMap<Series>
    {
        Series _series;
        ValueConverter converter = new ValueConverter();
        
        public CompactSeriesMap(DataSet dataSet)
        {
            Map(o => o.Key).ToAttributeGroup("key")
                .Set(v => _series = dataSet.Series.Create(v))
                .GroupTypeMap(new CompactSeriesKeyMap(dataSet));

            string startTime = null;

            foreach (var attribute in dataSet.KeyFamily.Attributes.Where(a => a.AttachementLevel == AttachmentLevel.Series))
            {
                var _att = attribute;
                bool required = attribute.AssignmentStatus == AssignmentStatus.Mandatory;
                Map(o => converter.Serialize((IValue)o.Attributes[_att.Concept.ID], out startTime)).ToAttribute(_att.Concept.ID.ToString(), required)
                    .Set(v => _series.Attributes[_att.Concept.ID] = converter.Parse(_att, v, null))
                    .Converter(new StringConverter());
            }

            MapCollection(o => o).ToElement("Obs", false)
                .Set(v => _series.Add(v))
                .ClassMap(() => new CompactObservationMap(_series, dataSet.KeyFamily));
        }

        protected override Series Return()
        {
            return _series;
        }
    }
}
