using System;
using System.Collections.Generic;
using System.Text;
using SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Structure
{
    public class DimensionType
    {
        private List<TextFormatType> _textformat;
        private List<AnnotationType> _annotations;
        private IDType _conceptref;

        public IDType Conceptref
        {
            get { return _conceptref; }
            set { _conceptref = value; }
        }
        private IDType _conceptagency;
        private string _conceptversion;
        private IDType _conceptschemaref;
        private IDType _codelist;
        private bool isfrequencydimension;

        public bool IsFrequencyDimension
        {
            get { return isfrequencydimension; }
            set { isfrequencydimension = value; }
        }

        public IDType Codelist
        {
            get { return _codelist; }
            set { _codelist = value; }
        }

        public DimensionType()
        {
            _textformat = new List<TextFormatType>();
            _annotations = new List<AnnotationType>();
        }


    }
}
