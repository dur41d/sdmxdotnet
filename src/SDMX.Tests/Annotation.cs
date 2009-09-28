using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class Annotation
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public Text Text { get; private set; }

        public Annotation()
        {
            Text = new Text();
        }
    }
}
