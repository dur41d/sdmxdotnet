using OXM;

namespace SDMX.Parsers
{
    internal class KeyFamilyCriterionMap : ClassMap<KeyFamilyCriterion>
    {
        string _name;

        public KeyFamilyCriterionMap()
        {
            Map(o => o.Name).ToContent()
            .Set(v => _name = v)
            .Converter(new StringConverter());
        }

        protected override KeyFamilyCriterion Return()
        {
            return new KeyFamilyCriterion() { Name = _name };
        }
    }
}
