using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Structure
{
    public class ConceptType
    {
        private string _name;
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _textformat;

        public string Textformat
        {
            get { return _textformat; }
            set { _textformat = value; }
        }
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _agencyid;

        public string AgencyID
        {
            get { return _agencyid; }
            set { _agencyid = value; }
        }
        private string _version;

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        private string _uri;

        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }
        private string _urn;

        public string Urn
        {
            get { return _urn; }
            set { _urn = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


    }
}
