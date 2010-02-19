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
    public interface IMessage
    {
        Header Header { get; set; }
    }
    
    public abstract class Message : IMessage
    {
        public Header Header { get; set; }

        public override string ToString()
        {
            return Header.ToString();
        }
    }
}
