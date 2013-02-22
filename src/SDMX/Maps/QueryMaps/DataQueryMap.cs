using OXM;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace SDMX.Parsers
{
    internal class DataQueryMap : ClassMap<DataQuery>
    {
        DataQuery _query = new DataQuery();

        public DataQueryMap()
        {
            Map(o => o.Criterion as DataSetCriterion).ToElement("DataSet", false)
                .Set(v => _query.Criterion = v)
                .ClassMap(() => new DataSetCriterionMap());

            Map(o => o.Criterion as KeyFamilyCriterion).ToElement("KeyFamily", false)
                .Set(v => _query.Criterion = v)
                .ClassMap(() => new KeyFamilyCriterionMap());

            Map(o => o.Criterion as DimensionCriterion).ToElement("Dimension", false)
                .Set(v => _query.Criterion = v)
                .ClassMap(() => new DimensionCriterionMap());

            Map(o => o.Criterion as AttributeCriterion).ToElement("Attribute", false)
                .Set(v => _query.Criterion = v)
                .ClassMap(() => new AttributeCriterionMap());

            Map(o => o.Criterion as ITimeCriterion).ToElement("Time", false)
                .Set(v => _query.Criterion = v)
                .ClassMap(() => new ITimeCriterionMap());

            Map(o => o.Criterion as OrCriterion).ToElement("Or", false)
               .Set(v => _query.Criterion = v)
               .ClassMap(() => new OrCriterionMap());

            Map(o => o.Criterion as AndCriterion).ToElement("And", false)
               .Set(v => _query.Criterion = v)
               .ClassMap(() => new AndCriterionMap());
        }

        protected override DataQuery Return()
        {
            return _query;
        }
    }
}
