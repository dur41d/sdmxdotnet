using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Structure
{
    public class PrimaryMeasureType
    {
        private string _textformat;

        public string Textformat
        {
            get { return _textformat; }
            set { _textformat = value; }
        }
        private SDMX_Common.AnnotationType _annotation;

        public SDMX_Common.AnnotationType Annotation
        {
            get { return _annotation; }
            set { _annotation = value; }
        }
        private SDMX_Common.IDType _conceptref;

        public SDMX_Common.IDType ConceptRef
        {
            get { return _conceptref; }
            set { _conceptref = value; }
        }
        private string _conceptversion;

        public string ConceptVersion
        {
            get { return _conceptversion; }
            set { _conceptversion = value; }
        }
        private SDMX_Common.IDType _conceptagency;

        public SDMX_Common.IDType ConceptAgency
        {
            get { return _conceptagency; }
            set { _conceptagency = value; }
        }
        private SDMX_Common.IDType _conceptschemaref;

        public SDMX_Common.IDType ConceptSchemaRef
        {
            get { return _conceptschemaref; }
            set { _conceptschemaref = value; }
        }
        private SDMX_Common.IDType _conceptschemaagency;

        public SDMX_Common.IDType ConceptSchemaAgency
        {
            get { return _conceptschemaagency; }
            set { _conceptschemaagency = value; }
        }
        private SDMX_Common.IDType _codelist;

        public SDMX_Common.IDType Codelist
        {
            get { return _codelist; }
            set { _codelist = value; }
        }
        private string _codelistversion;

        public string CodelistVersion
        {
            get { return _codelistversion; }
            set { _codelistversion = value; }
        }
        private SDMX_Common.IDType _codelistagency;

        public SDMX_Common.IDType CodelistAgency
        {
            get { return _codelistagency; }
            set { _codelistagency = value; }
        }
    }
}