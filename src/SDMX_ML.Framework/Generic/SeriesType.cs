using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Common = SDMX_ML.Framework.Common;

namespace SDMX_ML.Framework.Generic
{
    /// <summary>
    /// SeriesType specifies the structure of a series. This includes all of 
    /// the key values, values for all the attributes, and the set of observations 
    /// making up the series content. Messages may transmit only attributes, only 
    /// data, or both. Regardless, the series key is always required. Key values 
    /// appear at the Series level in an ordered sequence which corresponds to the 
    /// key sequence in the key family. A series in a delete message need not supply 
    /// more than the key, indicating that the entire series identified by that key 
    /// should be deleted. If series attributes are sent in a delete message, any 
    /// valid value specified for an attribute indicates that the attribute should 
    /// be deleted
    /// </summary>
    public class SeriesType
    {
        private List<ValueType> _serieskeys = new List<ValueType>();
        private List<ValueType> _attributes = new List<ValueType>();
        private List<ObsType> _obs = new List<ObsType>();
        private List<SDMX_Common.AnnotationType> _annotations = new List<SDMX_ML.Framework.Common.AnnotationType>();

        public List<ValueType> Serieskey
        {
            get { return _serieskeys; }
            set { _serieskeys = value; }
        }

        public List<ValueType> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public List<ObsType> Obs
        {
            get { return _obs; }
            set { _obs = value; }
        }

        public List<SDMX_Common.AnnotationType> Annotations
        {
            get { return _annotations; }
            set { _annotations = value; }
        }
    }
}