using OXM;

namespace SDMX.Parsers
{
    public class QueryMap : ClassMap<Query>
    {
        Query _query = new Query();

        public QueryMap()
        {
            MapCollection(o => o.DataQueries).ToElement(Namespaces.Query + "DataWhere", false)
                .Set(v => _query.DataQueries.Add(v))
                .ClassMap(() => new DataQueryMap());
        }

        protected override Query Return()
        {
            return _query;
        }
    }
}
