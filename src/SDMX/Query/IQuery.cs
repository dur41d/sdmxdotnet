using System.Collections.Generic;

namespace SDMX
{
    public interface IQuery
    {
        ICriterion Criterion { get; set; }
    }
}