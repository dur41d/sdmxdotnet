using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Structure
{
    public class KeyFamiliesType
    {
        private List<KeyFamilyType> _keyfamily;

        public KeyFamiliesType()
        {
            _keyfamily = new List<KeyFamilyType>();
        }

        public List<KeyFamilyType> KeyFamily
        {
            get { return _keyfamily; }
            set { _keyfamily = value; }
        }

    }
}
