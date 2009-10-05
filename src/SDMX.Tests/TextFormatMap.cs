using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace SDMX.Tests
{
    public class TextFormatMap : ClassMap<TextFormat>
    {
        public TextFormatMap()
        {
            MapAttribute<TextType>("textType", false)
                .Parser(s => (TextType)Enum.Parse(typeof(TextType), s))
                .Getter(o => o.TextType)
                .Setter((o, v) => o.TextType = v);
        }

        public override string ElementName
        {
            get
            {
                return "TextFormat";
            }
        }

        protected override TextFormat CreateObject()
        {
            return new TextFormat();
        }
    }
}
