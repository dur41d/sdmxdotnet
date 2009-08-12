using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Generic
{
    /// <summary>
    /// DataSetType defines the structure of a data set. This consists of a key 
    /// family reference which contains the ID of the key family, and the attribute 
    /// values attached at the data set level. A DataSet may be used to transmit 
    /// documentation (that is, only attribute values), data, or a combination of 
    /// both. If providing only documentation, you need not send the complete set of 
    /// attributes. If transmitting only data, the Group may be omitted if desired. 
    /// Uniqueness constraints are defined for the attributes of the data set. If 
    /// dataset-level attributes are sent in a delete message, then any valid attribute 
    /// value will indicate that the current attribute value should be deleted. The 
    /// keyFamilyURI attribute is provided to allow a URI (typically a URL) to be provided, 
    /// pointing to an SDMX-ML Structure message describing the key family. Attributes are 
    /// provided for describing the contents of a data or metadata set, which are particularly 
    /// important for interactions with the SDMX Registry: datasetID, dataProviderSchemeAgencyID, 
    /// dataProviderSchemeID, dataflowAgencyID, and dataflowID all take the IDs specified by the 
    /// attribute names. The action attribute indicates whether the file is appending, replacing, 
    /// or deleting. Attributes reportingBeginDate, reportingEndDate, validFromDate, and 
    /// validToDate are inclusive. publicationYear holds the ISO 8601 four-digit year, and 
    /// publicationPeriod specifies the period of publication of the data in terms of whatever 
    /// provisioning agreements might be in force (ie, "Q1 2005" if that is the time of publication 
    /// for a data set published on a quarterly basis).
    /// </summary>
    public class DataSetType
    {
        //Elements
        private SDMX_Common.IDType _keyfamilyref;
        private List<ValueType> _attributes = new List<ValueType>();
        private List<GroupType> _group = new List<GroupType>();
        private List<SeriesType> _series = new List<SeriesType>();
        private List<SDMX_Common.AnnotationType> _annotations = new List<SDMX_ML.Framework.Common.AnnotationType>();

        //Attributes
        private string _keyfamilyuri;
        private SDMX_Common.IDType _datasetid;
        private SDMX_Common.IDType _dataproviderschemeagencyid;
        private SDMX_Common.IDType _dataproviderschemeid;
        private SDMX_Common.IDType _dataproviderid;
        private SDMX_Common.IDType _dataflowagencyid;
        private SDMX_Common.IDType _dataflowid;
        private SDMX_Common.IDType _action;

        public SDMX_Common.IDType Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public SDMX_Common.IDType DataflowID
        {
            get { return _dataflowid; }
            set { _dataflowid = value; }
        }

        public SDMX_Common.IDType DataflowAgencyID
        {
            get { return _dataflowagencyid; }
            set { _dataflowagencyid = value; }
        }

        public SDMX_Common.IDType DataProviderId
        {
            get { return _dataproviderid; }
            set { _dataproviderid = value; }
        }

        public SDMX_Common.IDType DataProviderSchemeId
        {
            get { return _dataproviderschemeid; }
            set { _dataproviderschemeid = value; }
        }

        public SDMX_Common.IDType DataproviderSchemeAgencyId
        {
            get { return _dataproviderschemeagencyid; }
            set { _dataproviderschemeagencyid = value; }
        }

        public SDMX_Common.IDType DataSetID
        {
            get { return _datasetid; }
            set { _datasetid = value; }
        }

        public string KeyFamilyURI
        {
            get { return _keyfamilyuri; }
            set { _keyfamilyuri = value; }
        }


        public List<SDMX_Common.AnnotationType> Annotations
        {
            get { return _annotations; }
            set { _annotations = value; }
        }

        public List<SeriesType> Series
        {
            get { return _series; }
            set { _series = value; }
        }

        public SDMX_Common.IDType Keyfamilyref
        {
            get { return _keyfamilyref; }
            set { _keyfamilyref = value; }
        }

        public List<GroupType> Group
        {
            get { return _group; }
            set { _group = value; }
        }

        public List<ValueType> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }
        
        

    }
}
