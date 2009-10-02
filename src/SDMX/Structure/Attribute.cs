using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SDMX
{
    public class Attribute : Component
    {
        public AttachmentLevel AttachementLevel { get; set; }
        public AssignmentStatus AssignmentStatus { get; set; }

        public Attribute(Concept concept)
            : base(concept)
        {
        }

        public Attribute(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }
    }
}