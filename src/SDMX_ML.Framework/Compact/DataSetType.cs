using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Compact
{
    public class DataSetType
    {
        //Elements
        private SDMX_Common.IDType _keyfamilyref;
        private List<Group> _group = new List<Group>();
        private List<SeriesType> _series = new List<SeriesType>(); 
        
    }
}
