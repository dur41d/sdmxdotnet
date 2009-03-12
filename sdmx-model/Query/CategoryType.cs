using System;
using System.Collections.Generic;
using System.Text;

using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Query
{
    public class CategoryType
    {
        private string _value;
        private SDMX_Common.IDType _categoryscheme;
        private SDMX_Common.IDType _id;
        private SDMX_Common.IDType _agencyid;
        private string _version;

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public SDMX_Common.IDType ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public SDMX_Common.IDType agencyID
        {
            get { return _agencyid; }
            set { _agencyid = value; }
        }

        public SDMX_Common.IDType CategoryScheme
        {
            get { return _categoryscheme; }
            set { _categoryscheme = value; }
        }



    }
}
