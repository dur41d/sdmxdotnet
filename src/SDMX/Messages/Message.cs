using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using SDMX.Parsers;
using System.Xml;
using System.IO;

namespace SDMX
{
    public abstract class Message
    {
        public Header Header { get; set; }
    }
}
