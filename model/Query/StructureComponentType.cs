using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Query
{
    public class StructureComponentType
    {
        private string _value;
        private string _id;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


    }
}
