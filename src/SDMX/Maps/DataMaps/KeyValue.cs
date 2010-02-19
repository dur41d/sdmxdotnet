using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class KeyValue
    {
        public ID Concept { get; set; }
        public string Value { get; set; }
        public string StartTime { get; set; }
    }
}
