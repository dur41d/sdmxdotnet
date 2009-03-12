using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Common
{
    public class TextType
    {
        public TextType()
        {
            _lang = "en";
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private string _lang;

        public string Lang
        {
            get { return _lang; }
            set { _lang = value; }
        }

    }
}
