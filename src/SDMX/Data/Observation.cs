using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class Observation : AnnotableArtefact, IAttachableArtefact
    {
        public Series Series { get; private set; }
        public TimePeriod Time { get; private set; }
        public AttributeValueCollection Attributes { get; private set; }

        internal Observation(Series series, TimePeriod time)
        {
            Series = series;
            Time = time;
            Attributes = new AttributeValueCollection(series.DataSet.KeyFamily, AttachmentLevel.Observation);
        }

        private Value _value;
        public Value Value 
        {
            get
            {
                return _value;
            }
            set
            {
                Series.DataSet.KeyFamily.PrimaryMeasure.Validate(value);

                _value = value;
            }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
