using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal class DataMessageMap : RoolElementMap<DataMessage>
    {
        DataMessage _message = new DataMessage();

        public DataMessageMap(KeyFamily keyFamily)
        {
            RegisterNamespace("common", Namespaces.Common);
            RegisterNamespace("generic", Namespaces.Generic);

            ElementsOrder("Header", "DataSet");

            Map(o => o.Header).ToElement("Header", true)
                .Set(v => _message.Header = v)
                .ClassMap(() => new HeaderMap());

            Map(o => o.DataSet).ToElement("DataSet", true)
                .Set(v => _message.DataSet = v)
                .ClassMap(() => new DataSetMap(keyFamily));
        }

        public override System.Xml.Linq.XName Name
        {
            get 
            { 
                return Namespaces.Message + "GenericData"; 
            }
        }

        protected override DataMessage Return()
        {
            return _message;
        }
    }
}
