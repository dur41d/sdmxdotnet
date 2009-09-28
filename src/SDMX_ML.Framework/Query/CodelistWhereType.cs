using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Query
{
    /// <summary>
    /// The CodelistWhere element representes a query for a codelist or codelists. 
    /// It contains all of the clauses in that query, represented by its child 
    /// elements. Values are the IDs of the referenced object
    /// </summary>
    public class CodelistWhereType
    {
        private CodelistType _codelist;
        private string _agencyid;
        private string _version;
        private AndType _and;
        private OrType _or;

        public CodelistWhereType()
        {
        }
        
        public AndType And
        {
            set { _and = value; }
            get { return _and; }
        }

        public OrType Or
        {
            set { _or = value; }
            get { return _or; }
        }

        public CodelistType Codelist
        {
            get { return _codelist; }
            set { _codelist = value; }
        }

        public string Agencyid
        {
            get { return _agencyid; }
            set { _agencyid = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
    }
}
