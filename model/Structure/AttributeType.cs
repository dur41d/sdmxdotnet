using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Structure
{

    public class AttributeType
    {
        private TextFormatType _textformat;

        public TextFormatType TextFormat
        {
            get { return _textformat; }
            set { _textformat = value; }
        }
        private SDMX_Common.IDType _attachmentgroup;

        public SDMX_Common.IDType AttachmentGroup
        {
            get { return _attachmentgroup; }
            set { _attachmentgroup = value; }
        }
        private SDMX_Common.IDType _attachmentmeasure;

        public SDMX_Common.IDType AttachmentMeasure
        {
            get { return _attachmentmeasure; }
            set { _attachmentmeasure = value; }
        }
        private SDMX_Common.AnnotationType _annotations;

        public SDMX_Common.AnnotationType Annotations
        {
            get { return _annotations; }
            set { _annotations = value; }
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
        private SDMX_Common.IDType _conceptsagency;
        private SDMX_Common.IDType _conceptschemaref;
        private SDMX_Common.IDType _conceptschemaagency;
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
        private bool _istimeformat;

        public bool IsTimeFormat
        {
            get { return _istimeformat; }
            set { _istimeformat = value; }
        }
        private bool _crosssectionalattachdataset;

        public bool CrossSectionalAttachDataset
        {
            get { return _crosssectionalattachdataset; }
            set { _crosssectionalattachdataset = value; }
        }
        private bool _crosssectionalattachgroup;

        public bool CrossSectionalAttachGroup
        {
            get { return _crosssectionalattachgroup; }
            set { _crosssectionalattachgroup = value; }
        }
        private bool _crosssectionalattachsection;

        public bool CrossSectionalAttachSection
        {
            get { return _crosssectionalattachsection; }
            set { _crosssectionalattachsection = value; }
        }
        private bool _crosssectionalattachobservation;

        public bool CrossSectionalAttachObservation
        {
            get { return _crosssectionalattachobservation; }
            set { _crosssectionalattachobservation = value; }
        }
        private bool _isentityattribute;
        private bool _isnonobservationaltimeattribute;
        private bool _iscountattribute;
        private bool _isfrequencyattribute;
        private bool _isidentityattribute;
        private string _value;
        private AttachmentLevel _attachmentlevel;
        private AssignmentStatus _assignmentstatus;

        public AssignmentStatus AssignmentStatus
        {
            get { return _assignmentstatus; }
            set { _assignmentstatus = value; }
        }

        public AttributeType()
        {
        }

        public AttachmentLevel AttachmentLevel
        {
            get { return _attachmentlevel; }
            set { _attachmentlevel = value; }
        }

    }
}
