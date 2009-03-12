using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Structure
{
    public class CodeType
    {
        private string _code;
        private string _description;
        private string _language;

        public string Lang
        {
            get { return _language; }
            set { _language = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Codevalue
        {
            get { return _code; }
            set { _code = value; }
        

        }



    }
}
