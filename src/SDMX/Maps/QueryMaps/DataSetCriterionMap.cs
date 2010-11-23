using OXM;

namespace SDMX.Parsers
{
    internal class DataSetCriterionMap : ClassMap<DataSetCriterion>
    {
        string _name;

        public DataSetCriterionMap()
        {
            Map(o => o.Name).ToContent()
            .Set(v => _name = v)
            .Converter(new StringConverter());
        }

        protected override DataSetCriterion Return()
        {
            return new DataSetCriterion() { Name = _name };
        }
    }
}
