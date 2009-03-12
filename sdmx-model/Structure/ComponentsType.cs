using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Structure
{
    public class ComponentsType
    {
        private List<DimensionType> _dimensions;
        private List<TimeDimensionType> _timedimension;
        private List<GroupType> _group;
        private PrimaryMeasureType _primarymeasure;
        private List<CrossSectionalMeasure> _crosssectionalmeasure;
        private List<AttributeType> _attribute;

        public ComponentsType()
        {
            _dimensions = new List<DimensionType>();
            _timedimension = new List<TimeDimensionType>();
            _group = new List<GroupType>();
            _crosssectionalmeasure = new List<CrossSectionalMeasure>();
            _attribute = new List<AttributeType>();
        }

        

        public List<DimensionType> Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }
        
        public List<TimeDimensionType> Timedimension
        {
            get { return _timedimension; }
            set { _timedimension = value; }
        }
        

        public List<GroupType> Group
        {
            get { return _group; }
            set { _group = value; }
        }
        

        public PrimaryMeasureType Primarymeasure
        {
            get { return _primarymeasure; }
            set { _primarymeasure = value; }
        }
        

        public List<CrossSectionalMeasure> Crosssectionalmeasure
        {
            get { return _crosssectionalmeasure; }
            set { _crosssectionalmeasure = value; }
        }
        

        public List<AttributeType> Attribute
        {
            get { return _attribute; }
            set { _attribute = value; }
        }

    }
}
