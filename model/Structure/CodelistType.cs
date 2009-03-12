using System;
using System.Collections.Generic;
using System.Text;
//using SDMX_Element = SDMX_ML.Framework.ElementType;

namespace SDMX_ML.Framework.Structure
{
    public class CodelistType
    {
        private List<CodeType> _codevalues = new List<CodeType>();
        private string _language;

        public string Lang
        {
            get { return _language; }
            set { _language = value; }
        }

        public List<CodeType> Codevalues
        {
            get { return _codevalues; }
            set { _codevalues = value; }
        }

        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _agencyid;

        public string Agencyid
        {
            get { return _agencyid; }
            set { _agencyid = value; }
        }
        private string _uri = "";

        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }
        private string _version = "";

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

    }
}
