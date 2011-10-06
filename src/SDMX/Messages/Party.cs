using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Party
    {
        public Id Id { get; set; }
        public InternationalText Name { get; private set; }
        public IList<Contact> Contacts { get; private set; }

        public Party(Id id)
        {
            Id = id;
            Contacts = new List<Contact>();
            Name = new InternationalText();
        }
    }
}
