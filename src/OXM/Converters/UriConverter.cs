using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public class UriConverter : SimpleTypeConverter<Uri>
    {
        public override string ToXml(Uri value)
        {
            return value == null ? null : value.ToString();
        }

        public override Uri ToObj(string value)
        {
            return new Uri(value);
        }
    }
}
