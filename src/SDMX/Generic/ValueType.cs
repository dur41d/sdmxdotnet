using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Generic
{
    public class ValueType
    {
        private SDMX_Common.IDType _concept;
        private string _value;
        private string _starttime;

        public SDMX_Common.IDType Concept
        {
            get { return _concept; }
            set { _concept = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string StartTime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }
    }
}
