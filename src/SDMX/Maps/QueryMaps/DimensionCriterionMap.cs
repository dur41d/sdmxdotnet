using OXM;

namespace SDMX.Parsers
{
    internal class DimensionCriterionMap : ClassMap<DimensionCriterion>
    {
        string _name;
        string _value;

        public DimensionCriterionMap()
        {
            Map(o => o.Name).ToAttribute("id", true)
                .Set(v => _name = v)
                .Converter(new StringConverter());

            Map(o => o.Value).ToContent()
                .Set(v => _value = v)
                .Converter(new StringConverter());
        }

        protected override DimensionCriterion Return()
        {
            return new DimensionCriterion() { Name = _name, Value = _value };
        }
    }
}
