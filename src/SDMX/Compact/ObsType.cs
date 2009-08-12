using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Compact
{
    public class ObsType
    {
        private SDMX_Common.TimePeriodType _time;
        private ObsValueType _obs;
        private List<GroupType> _attributes = new List<GroupType>();
        private List<SDMX_Common.AnnotationType> _annotations = new List<SDMX_ML.Framework.Common.AnnotationType>();

        public List<SDMX_Common.AnnotationType> Annotations
        {
            get { return _annotations; }
            set { _annotations = value; }
        }

        public SDMX_Common.TimePeriodType Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public ObsValueType ObsValue
        {
            get { return _obs; }
            set { _obs = value; }
        }

        public List<GroupType> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }
        
        
    }
}
