using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Structure
{
    public class KeyFamilyType
    {
        private ComponentsType _component;

        public KeyFamilyType()
        {

        }

        public ComponentsType Component
        {
            get { return _component; }
            set { _component = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _description;
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
        private string _uri;
        private string _urn;
        private bool _isfinal;
        private bool _isexternalreference;
        private string _validfrom;
        private string _validto;


    }
}
