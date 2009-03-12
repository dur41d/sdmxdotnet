using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Generic = SDMX_ML.Framework.Generic;
using SDMX_Structure = SDMX_ML.Framework.Structure;
using SDMX_Query = SDMX_ML.Framework.Query;
using SDMX_Common = SDMX_ML.Framework.Common;


namespace SDMX_ML.Framework.Abstract
{
    public abstract class AndOR
    {
        private List<string> _dataset;
        private List<string> _metadataset;
        private List<string> _keyfamily;
        private List<string> _metadatastructure;
        private List<SDMX_Query.DimensionType> _dimensions;
        private List<SDMX_Query.StructureComponentType> _structurecomponent;
        private List<SDMX_Query.AttributeType> _attributes;
        private List<SDMX_Query.CodelistType> _codelists;       
        private List<SDMX_Query.TimeType> _time;        
        private List<SDMX_Query.CategoryType> _category;
        private List<string> _concept;
        private List<string> _agencyid;
        private List<string> _dataprovider;
        private List<string> _dataflow;
        private List<string> _metadataflow;
        private List<string> _version;
        private List<AndOR> _or;
        private List<AndOR> _and;

        public AndOR()
        {
            _dataset = new List<string>();
            _metadataset = new List<string>();
            _keyfamily = new List<string>();
            _metadatastructure = new List<string>();
            _dimensions = new List<SDMX_Query.DimensionType>();
            _structurecomponent = new List<SDMX_Query.StructureComponentType>();
            _attributes = new List<SDMX_Query.AttributeType>();
            _codelists = new List<SDMX_Query.CodelistType>();
            _time = new List<SDMX_Query.TimeType>();
            _category = new List<SDMX_Query.CategoryType>();
            _concept = new List<string>();
            _agencyid = new List<string>();
            _dataprovider = new List<string>();
            _dataflow = new List<string>();
            _metadataflow = new List<string>();
            _version = new List<string>();
            _or = new List<AndOR>();
            _and = new List<AndOR>();

        }

        public List<string> Dataset
        {
            get { return _dataset; }
            set { _dataset = value; }
        }

        public List<string> Metadataset
        {
            get { return _metadataset; }
            set { _metadataset = value; }
        }

        public List<string> Keyfamily
        {
            get { return _keyfamily; }
            set { _keyfamily = value; }
        }

        public List<string> Metadatastructure
        {
            get { return _metadatastructure; }
            set { _metadatastructure = value; }
        }
        
        public List<SDMX_Query.DimensionType> Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        public List<SDMX_Query.StructureComponentType> Structurecomponent
        {
            get { return _structurecomponent; }
            set { _structurecomponent = value; }
        }

        public List<SDMX_Query.AttributeType> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public List<SDMX_Query.CodelistType> Codelists
        {
            get { return _codelists; }
            set { _codelists = value; }
        }

        public List<SDMX_Query.TimeType> Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public List<SDMX_Query.CategoryType> Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public List<string> Concept
        {
            get { return _concept; }
            set { _concept = value; }
        }

        public List<string> Agencyid
        {
            get { return _agencyid; }
            set { _agencyid = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public List<string> DataProvider
        {
            get { return _dataprovider; }
            set { _dataprovider = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public List<string> DataFlow
        {
            get { return _dataflow; }
            set { _dataflow = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public List<string> MetadataFlow
        {
            get { return _metadataflow; }
            set { _metadataflow = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public List<string> Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public List<AndOR> Or
        {
            get { return _or; }
            set { _or = value; }
        }
        public List<AndOR> And
        {
            get { return _and; }
            set { _and = value; }
        }
    }
}
