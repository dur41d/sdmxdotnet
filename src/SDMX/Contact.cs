using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Contact
    {
        public InternationalText Name { get; private set; }
        public InternationalText Department { get; private set; }
        public InternationalText Role { get; private set; }
        public IList<string> TelephoneList { get; private set; }
        public IList<string> FaxList { get; private set; }
        public IList<string> X400List { get; private set; }
        public IList<Uri> UriList { get; private set; }
        public IList<string> EmailList { get; private set; }

        public Contact()
        {
            Name = new InternationalText();
            Department = new InternationalText();
            Role = new InternationalText();

            TelephoneList = new List<string>();
            FaxList = new List<string>();
            X400List = new List<string>();
            UriList = new List<Uri>();
            EmailList = new List<string>();
        }
    }
}
