using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SDMX
{
    public class Attribute : Component, Item
    {   
        public AttachmentLevel AttachementLevel { get; set; }
        public AssignmentStatus AssignmentStatus { get; set; }
        public bool IsEntityAttribute { get; set; }
        public bool IsNonObservationalTimeAttribute { get; set; }
        public bool IsCountAttribute { get; set; }
        public bool IsFrequencyAttribute { get; set; }
        public bool IsIdentityAttribute { get; set; }
        public bool IsTimeFormat { get; set; }
        public CrossSectionalAttachmentLevel CrossSectionalAttachmentLevel { get; set; }

        private IList<ID> _attachmentMeasures = new List<ID>();
        public IList<ID> AttachmentMeasures
        {
            get
            {
                return _attachmentMeasures;
            }
        }

        private IList<ID> _attachmentGroups = new List<ID>();
        public IList<ID> AttachmentGroups
        {
            get
            {
                return _attachmentGroups;
            }
        }

        public Attribute(Concept concept)
            : base(concept)
        {
        }

        public Attribute(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }

        #region Item Members

        ID Item.ID
        {
            get { return Concept.ID; }
        }

        #endregion
    }
}