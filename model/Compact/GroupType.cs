using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Compact
{
    public class GroupType
    {
        private SDMX_Common.IDType _concept;
        private string _value;

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

    }
}
