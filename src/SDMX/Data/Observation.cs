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
        public ITimePeriod Time { get; private set; }
        public AttributeValueCollection Attributes { get; private set; }

        internal Observation(Series series, ITimePeriod time)
        {
            Series = series;
            Time = time;
            Attributes = new AttributeValueCollection(series.DataSet.KeyFamily, AttachmentLevel.Observation);
        }

        private IValue _value;
        public IValue Value 
        {
            get
            {
                return _value;
            }
            set
            {
                if (!Series.DataSet.KeyFamily.PrimaryMeasure.IsValid(value))
                {
                    throw new SDMXException("The value is not valid for the primary measure of the key family. Value: '{0}'.", value);
                }

                _value = value;
            }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
