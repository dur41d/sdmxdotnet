using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Query
{
    public class DimensionType
    {
        private string _id;
        private string _value;

        public string Id
        {
            get { return _id; }
            set { _id = value; } 
        }
        

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public DimensionType(string id, string value)
        {
            _id = id;
            _value = value;
        }

        public DimensionType()
        {
        }


    }
}
