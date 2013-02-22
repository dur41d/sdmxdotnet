using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Annotation
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public Uri Url { get; set; }
        public InternationalText Text { get; private set; }

        public Annotation()
        {
            Text = new InternationalText();
        }
    }
}
