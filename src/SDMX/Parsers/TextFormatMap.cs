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
        public TextFormatMap()
        {
            Instance = new TextFormat();
            
            MapAttribute<TextType>("textType", false)
                .Parser(s => (TextType)Enum.Parse(typeof(TextType), s))
                .Getter(o => o.TextType)
                .Setter(p => Instance.TextType = p);
        }    
    }
}
