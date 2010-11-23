using System.Collections.Generic;

namespace SDMX
{
    public class Query
    {
        public IList<DataQuery> DataQueries { get; private set; }

        public Query()
        {
            DataQueries = new List<DataQuery>();
        }
    }
}
