using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public class UriConverter : SimpleTypeConverter<Uri>
    {
        public override bool TrySerialize(Uri value, out string s)
        {
            s = value == null ? null : value.ToString();
            return true;
        }

        public override bool TryParse(string s, out Uri value)
        {
            return Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out value);
        }
    }
}
