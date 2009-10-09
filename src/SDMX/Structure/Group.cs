using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class Group2
    {
        public IList<Attribute> Attributes { get; internal set; }
        public IList<Annotation> Annotations { get; internal set; }

        internal Group2()
        {         
            
        }

    }

    public class Group : AnnotableArtefact
    {
        private Dictionary<ID, Dimension> dimensions = new Dictionary<ID, Dimension>();
        
        public ID ID { get; set; }                
        public ID AttachmentConstraintRef { get; set; }
        public InternationalText Description { get; private set; }
        public KeyFamily KeyFamily { get; private set; }

        public IEnumerable<Dimension> Dimensions
        {
            get
            {
                return dimensions.Values.AsEnumerable();
            }
        }
        
        internal Group(ID id, KeyFamily keyFamily)
        {
            Contract.AssertNotNull(() => id);
            Contract.AssertNotNull(() => keyFamily);

            ID = id;
            KeyFamily = keyFamily;
            Description = new InternationalText();
        }

        public void AddDimension(ID conceptID)
        {
            Contract.AssertNotNull(() => conceptID);

            var dimension = KeyFamily.GetDimension(conceptID);
            dimensions.Add(conceptID, dimension);
        }

        public void RemoveDimension(ID conceptID)
        {
            Contract.AssertNotNull(() => conceptID);

            dimensions.Remove(conceptID);
        }
    }
}
