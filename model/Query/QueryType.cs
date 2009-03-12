using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Query
{
    public class QueryType
    {
        private int _defaultlimit;

        public int DefaultLimit
        {
            get { return _defaultlimit; }
            set { _defaultlimit = value; }
        }
        List<DataWhereType> _datawhere = null;

        public List<DataWhereType> Datawhere
        {
            get { return _datawhere; }
            set { _datawhere = value; }
        }
        List<CodelistWhereType> _codelistwhere = null;

        public List<CodelistWhereType> Codelistwhere
        {
            get { return _codelistwhere; }
            set { _codelistwhere = value; }
        }
        List<ConceptWhereType> _conceptwhere = null;

        public List<ConceptWhereType> Conceptwhere
        {
            get { return _conceptwhere; }
            set { _conceptwhere = value; }
        }
        List<KeyFamilyWhereType> _keyfamilywhere = null;

        public List<KeyFamilyWhereType> Keyfamilywhere
        {
            get { return _keyfamilywhere; }
            set { _keyfamilywhere = value; }
        }
    }
}
