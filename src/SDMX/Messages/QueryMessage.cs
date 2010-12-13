using OXM;
using SDMX.Parsers;

namespace SDMX
{
    public class QueryMessage : MessageBase<QueryMessage>
    {
        public Query Query { get; set; }

        protected override QueryMessage GetThis()
        {
            return this;
        }
    }
}
