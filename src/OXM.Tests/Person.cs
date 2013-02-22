using System.Collections.Generic;

namespace OXM.Tests
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Address> Addresses { get; private set; }

        public Person()
        {
            Addresses = new List<Address>();
        }

    }
}
