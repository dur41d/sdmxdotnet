using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class QueryMessageMap : RootElementMap<QueryMessage>
    {
        public override XName Name
        {
            get { return Namespaces.Message + "QueryMessage"; }
        }

        QueryMessage _message = new QueryMessage();

        public QueryMessageMap()
        {
            RegisterNamespace("common", Namespaces.Common);
            RegisterNamespace("query", Namespaces.Query);

            ElementsOrder("Header", "Query");

            Map(o => o.Header).ToElement("Header", true)
                .Set(v => _message.Header = v)
                .ClassMap(() => new HeaderMap());

            Map(o => o.Query).ToElement("Query", true)
                    .Set(v => _message.Query = v)
                    .ClassMap(() => new QueryMap());
        }

        protected override QueryMessage Return()
        {
            return _message;
        }
    }
}
