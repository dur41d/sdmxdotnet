using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    public class CompactObservationMap : ClassMap<Observation>
    {
        Observation _obs;
        ValueConverter converter = new ValueConverter();

        public CompactObservationMap(Series series, KeyFamily keyFamily)
        {
            string startTime = null;
            Map(o => converter.Serialize(o.Time, out startTime)).ToAttribute(keyFamily.TimeDimension.Concept.ID.ToString(), true)
                .Set(v => _obs = series.Create((TimePeriod)converter.Parse(keyFamily.TimeDimension, v, null)))                
                .Converter(new StringConverter());

            Map(o => converter.Serialize(o.Value, out startTime)).ToAttribute(keyFamily.PrimaryMeasure.Concept.ID.ToString(), true)
                .Set(v => _obs.Value = converter.Parse(keyFamily.PrimaryMeasure, v, null))
                .Converter(new StringConverter());

            foreach (var attribute in keyFamily.Attributes.Where(a => a.AttachementLevel == AttachmentLevel.Observation))
            {
                var _att = attribute;
                bool required = attribute.AssignmentStatus == AssignmentStatus.Mandatory;
                Map(o => GetValue(_att, o)).ToAttribute(_att.Concept.ID.ToString(), required)
                    .Set(v => _obs.Attributes[_att.Concept.ID] = converter.Parse(_att, v, null))
                    .Converter(new StringConverter());
            }
        }

        private string GetValue(Attribute _att, Observation o)
        {
            var value = o.Attributes[_att.Concept.ID];

            if (value == null)
            {
                return null;
            }

            string st = null;
            return converter.Serialize((Value)o.Attributes[_att.Concept.ID], out st);
        }

        protected override Observation Return()
        {
            return _obs;
        }
    }
}
