using System.Collections.Generic;

namespace SDMX
{
    public class AttributeCriterion : ICriterion
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public AttachmentLevel AttachmentLevel { get; set; }
    }
}
