using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Message
{
    public class ContactType
    {
        private SDMX_Common.TextType _name;

        public SDMX_Common.TextType Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private SDMX_Common.TextType _department;

        public SDMX_Common.TextType Department
        {
            get { return _department; }
            set { _department = value; }
        }
        private SDMX_Common.TextType _role;

        public SDMX_Common.TextType Role
        {
            get { return _role; }
            set { _role = value; }
        }
        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _fax;

        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        private string _uri;

        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }
        private string _x400;

        public string X400
        {
            get { return _x400; }
            set { _x400 = value; }
        }
    }
}
