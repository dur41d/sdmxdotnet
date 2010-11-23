using OXM;

namespace SDMX.Parsers
{
    internal class AttributeCriterionMap : ClassMap<AttributeCriterion>
    {
        string _name;
        string _value;
        AttachmentLevel _attachmentLevel;

        public AttributeCriterionMap()
        {
            Map(o => o.Name).ToAttribute("id", true)
                .Set(v => _name = v)
                .Converter(new StringConverter());

            Map(o => o.AttachmentLevel).ToAttribute("attachmentLevel", false)
               .Set(v => _attachmentLevel = v)
               .Converter(new EnumConverter<AttachmentLevel>());

            Map(o => o.Value).ToContent()
                .Set(v => _value = v)
                .Converter(new StringConverter());
        }

        protected override AttributeCriterion Return()
        {
            return new AttributeCriterion() { Name = _name, Value = _value, AttachmentLevel = _attachmentLevel };
        }
    }
}
