using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class CompactDataMessageMap : RoolElementMap<DataMessage>
    {
        DataMessage _message = new DataMessage();

        public KeyFamily KeyFamily { get; set; }        

        public CompactDataMessageMap(string prefix, XNamespace ns)
        {
            RegisterNamespace("common", Namespaces.Common);
            RegisterNamespace(prefix, ns);

            ElementsOrder("Header", "DataSet");

            Map(o => o.Header).ToElement("Header", true)
                .Set(v => _message.Header = v)
                .ClassMap(() => new HeaderMap());

            Map(o => o.DataSet).ToElement(ns + "DataSet", true)
                .Set(v => _message.DataSet = v)
                .ClassMap(() => new CompactDataSetMap(KeyFamily));
        }

        public override System.Xml.Linq.XName Name
        {
            get
            {
                return Namespaces.Message + "CompactData";
            }
        }

        protected override DataMessage Return()
        {
            return _message;
        }
    }
}
