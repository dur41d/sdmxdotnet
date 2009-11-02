using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using OXM;

namespace SDMX.Parsers
{
    public class TextFormatMap : ClassMap<TextFormat>
    {
        TextFormat _textFormat = new TextFormat();

        public TextFormatMap()
        {
            Map<TextType?>(o => o.TextType == TextType.None ? (TextType?)null : o.TextType).ToAttribute("textType", false)
                .Set(v => _textFormat.TextType = v.Value)
                .Converter(new EnumConverter<TextType?>());
        }

        protected override TextFormat Return()
        {
            if (_textFormat.TextType == TextType.None)
                return null;

            return _textFormat;
        }
    }
}
