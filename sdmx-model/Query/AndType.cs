using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Abstract = SDMX_ML.Framework.Abstract;

namespace SDMX_ML.Framework.Query
{
    /// <summary>
    /// For the And element, each of its immediate child elements represent 
    /// clauses all of which represent conditions which must be satisfied. 
    /// If children are A, B, and C, then any legitimate response will meet 
    /// conditions A, B, and C. Values are the IDs of the referenced object
    /// </summary>
    public class AndType : SDMX_Abstract.AndOR
    {

    }
}
