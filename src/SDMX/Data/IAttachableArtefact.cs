using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;

namespace SDMX
{
    public interface IAttachableArtefact
    {
        AttributeValueCollection Attributes { get; }
    }
}
