using System.Collections.Generic;

namespace SDMX
{
    public interface ICriteriaContainer : ICriterion
    {
        void Add(ICriterion criterion);
        void Remove(ICriterion criterion);
        void Clear();
        IEnumerable<ICriterion> Criteria { get; }
    }
}