using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Common
{
    /// <summary>
    /// AnnotationType provides for non-documentation notes and annotations to be embedded 
    /// in data and structure messages. It provides optional fields for providing a title, 
    /// a type description, a URI, and the text of the annotation.
    /// </summary>
    public class AnnotationType
    {
        private TextType _annotationtext;
        private string _annotationtitle;
        private string _annotationtype;
        private string _annotationurl;

        public TextType AnnotationText
        {
            get { return _annotationtext; }
            set { _annotationtext = value; }
        }

        public string AnnotationTitle
        {
            get { return _annotationtitle; }
            set { _annotationtitle = value; }
        }

        public string Annotationtype
        {
            get { return _annotationtype; }
            set { _annotationtype = value; }
        }

        public string AnnotationUrl
        {
            get { return _annotationurl; }
            set { _annotationurl = value; }
        }
    }
}
