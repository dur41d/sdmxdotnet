using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class CompactDataSetMap : ClassMap<DataSet>
    {
        DataSet _dataSet;
        ValueConverter converter = new ValueConverter();

        public CompactDataSetMap(KeyFamily keyFamily)
        {
            _dataSet = new DataSet(keyFamily);

            MapCollection(o => o.Series).ToElement("Series", false)
                .Set(v => _dataSet.Series.Add(v))
                .ClassMap(() => new CompactSeriesMap(_dataSet));

            string startTime = null;

            foreach (var attribute in keyFamily.Attributes.Where(a => a.AttachementLevel == AttachmentLevel.DataSet))
            {
                var _att = attribute;
                bool required = attribute.AssignmentStatus == AssignmentStatus.Mandatory;
                Map(o => converter.Serialize((Value)o.Attributes[_att.Concept.ID], out startTime)).ToAttribute(_att.Concept.ID.ToString(), required)
                    .Set(v => _dataSet.Attributes[_att.Concept.ID] = converter.Parse(_att, v, null))
                    .Converter(new StringConverter());
            }
        }

        protected override DataSet Return()
        {
            return _dataSet;
        }
    }
}
