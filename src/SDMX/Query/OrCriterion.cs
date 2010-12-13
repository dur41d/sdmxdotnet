using System.Collections.Generic;
using System.Linq;

namespace SDMX
{
    public class OrCriterion : ICriteriaContainer
    {
        List<ICriterion> _list = new List<ICriterion>();

        public IEnumerable<ICriterion> Criteria 
        {
            get
            {
                return _list.AsEnumerable();
            }
        }

        public void Add(ICriterion criterion)
        {
            _list.Add(criterion);
        }

        public void Remove(ICriterion criterion)
        {
            _list.Remove(criterion);
        }

        public void Clear()
        {
            _list.Clear();
        }
    }
}
