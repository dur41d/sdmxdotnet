using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Structure = SDMX_ML.Framework.Structure;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Query
{
    /// <summary>
    /// The DataWhere element representes a query for data. It contains all of the 
    /// clauses in that query, represented by its child elements. Values are the 
    /// IDs of the referenced object
    /// </summary>
    public class DataWhereType
    {
        private DimensionType _dimensions;
        private TimeType _time;
        private string _keyfamily;
        private AttributeType _attributes;
        private CodelistType _codelists;
        private string _concept;
        private CategoryType _category;
        private string _dataprovider;
        private string _dataflow;
        private string _version;
        private string _dataset;
        private AndType _and;
        private OrType _or;

        public DataWhereType()
        {
        }

        public string Dataset
        {
            get { return _dataset; }
            set { _dataset = value; }
        }

        public string Concept
        {
            get { return _concept; }
            set { _concept = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public CategoryType Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public string DataFlow
        {
            get { return _dataflow; }
            set { _dataflow = value; }
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        public string DataProvider
        {
            get { return _dataprovider; }
            set { _dataprovider = value; }
        }

        public CodelistType Codelists
        {
            get { return _codelists; }
            set { _codelists = value; }
        }

        public AttributeType Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public DimensionType Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        public TimeType Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string Keyfamily
        {
            get { return _keyfamily; }
            set { _keyfamily = value; }
        }

        public AndType And
        {
            get{ return _and; }
            set{ _and = value;}
        }

        public OrType Or
        {
            get{ return _or; }
            set{ _or = value; }
        }
    }
}
