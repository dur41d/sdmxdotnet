using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Concept
    {
        public string Id { get; private set; }

        public Concept(string id)
        {
            this.Id = id;
        }
    }
}
