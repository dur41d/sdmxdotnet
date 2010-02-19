using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Generic
{
    /// <summary>
    /// The key values at the group level may be stated explicitly, 
    /// and all which are not wildcarded listed in GroupKey - they 
    /// must also all be given a value at the series level. It is 
    /// not necessary to specify the group key, however, as this may 
    /// be inferred from the values repeated at the series level. If 
    /// only documentation (group-level attributes) are being transmitted, 
    /// however, the GroupKey cannot be omitted. The type attribute 
    /// contains the name of the declared group in the key family. If 
    /// any group-level attributes are specified in a delete message, 
    /// then any valid value supplied for the attribute indicates that 
    /// the current attribute value should be deleted for the specified 
    /// attribute.
    /// </summary>
    public class GroupType
    {
        private string _type;
        private List<ValueType> _attributes = new List<ValueType>();
        private List<SeriesType> _series = new List<SeriesType>();
        private List<ValueType> _groupkey = new List<ValueType>();
        private List<SDMX_Common.AnnotationType> _annotations = new List<SDMX_Common.AnnotationType>();

        public List<SDMX_Common.AnnotationType> Annotations
        {
            get { return _annotations; }
            set { _annotations = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public List<ValueType> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public List<SeriesType> Series
        {
            get { return _series; }
            set { _series = value; }
        }

        public List<ValueType> Groupkey
        {
            get { return _groupkey; }
            set { _groupkey = value; }
        }
    }
}
