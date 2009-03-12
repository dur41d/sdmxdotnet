using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Message
{
    public class PartyType
    {

        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private SDMX_Common.TextType _name;

        public SDMX_Common.TextType Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private List<ContactType> _contact = new List<ContactType>();

        public List<ContactType> Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

    }
}
