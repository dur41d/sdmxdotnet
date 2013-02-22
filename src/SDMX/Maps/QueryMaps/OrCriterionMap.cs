using OXM;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace SDMX.Parsers
{
    internal class OrCriterionMap : ClassMap<OrCriterion>
    {
        OrCriterion _result = new OrCriterion();

        public OrCriterionMap()
        {
            MapCollection(o => o.Criteria.Filter<DataSetCriterion>()).ToElement("DataSet", false)
                .Set(v => _result.Add(v))
                .ClassMap(() => new DataSetCriterionMap());

            MapCollection(o => o.Criteria.Filter<KeyFamilyCriterion>()).ToElement("KeyFamily", false)
                .Set(v => _result.Add(v))
                .ClassMap(() => new KeyFamilyCriterionMap());

            MapCollection(o => o.Criteria.Filter<DimensionCriterion>()).ToElement("Dimension", false)
                .Set(v => _result.Add(v))
                .ClassMap(() => new DimensionCriterionMap());

            MapCollection(o => o.Criteria.Filter<AttributeCriterion>()).ToElement("Attribute", false)
                .Set(v => _result.Add(v))
                .ClassMap(() => new AttributeCriterionMap());

            MapCollection(o => o.Criteria.Filter<ITimeCriterion>()).ToElement("Time", false)
                .Set(v => _result.Add(v))
                .ClassMap(() => new ITimeCriterionMap());

            MapCollection(o => o.Criteria.Filter<OrCriterion>()).ToElement("Or", false)
             .Set(v => _result.Add(v))
             .ClassMap(() => new OrCriterionMap());

            MapCollection(o => o.Criteria.Filter<AndCriterion>()).ToElement("And", false)
              .Set(v => _result.Add(v))
              .ClassMap(() => new AndCriterionMap());
        }

        protected override OrCriterion Return()
        {
            return _result;
        }
    }
}
