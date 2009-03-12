using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Abstract = SDMX_ML.Framework.Abstract;

namespace SDMX_ML.Framework.Query
{
    /// <summary>
    /// The Or element's immediate children represent clauses in the query any 
    /// one of which is sufficient to satisfy the query. If these children are 
    /// A, B, and C, then any result which meets condition A, or condition B, 
    /// or condition C is a match for that query. Values are the IDs of the 
    /// referenced object
    /// </summary>
    public class OrType : SDMX_Abstract.AndOR
    {
    }
}
