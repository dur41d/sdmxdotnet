using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Structure = SDMX_ML.Framework.Structure;

namespace SDMX_ML.Framework.Query
{
    public class AttributeType
    {
        private string _id;
        private string _value;
        private AttachmentLevel _attachmentlevel;

        public AttributeType()
        {
            //Default Any
            _attachmentlevel = AttachmentLevel.Any;
        }
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Default value: Any
        /// </summary>
        public AttachmentLevel Attachmentlevel
        {
            get { return _attachmentlevel; }
            set { _attachmentlevel = value; }
        }
    }
}
