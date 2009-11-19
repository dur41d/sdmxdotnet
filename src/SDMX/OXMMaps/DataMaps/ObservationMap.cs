using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class ObservationMap : AnnotableArtefactMap<Observation>
    {
        Observation _obs;

        public ObservationMap(DataSet dataSet)
        {

        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _obs.Annotations.Add(annotation);
        }

        protected override Observation Return()
        {
            return _obs;
        }
    }
}
