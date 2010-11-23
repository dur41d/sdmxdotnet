using System.Collections.Generic;

namespace SDMX
{
    public class DataQuery : IQuery
    {
        public ICriterion Criterion { get; set; }
    }
}