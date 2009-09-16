using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    //public class SeriesKey
    //{
    //    private List<Value> values;
    //    internal void AddValue(Value value)
    //    {
    //        values.Add(value);
    //    }


    //}

    //public class Value
    //{
    //    public Dimension Dimension { get; set; }
    //    public object DimensionValue { get; set; }
    //    public DateTime? StartTime { get; set; }

    //    public Value(Dimension dimension)
    //    {
    //        // TODO: Complete member initialization
    //        this.Dimension = dimension;
    //    }

    //    public Value()
    //    {
    //        // TODO: Complete member initialization
    //    }
    //}

    public class Dimension
    {
        public Concept Concept { get; private set; }
        public CodeList CodeList { get; set; }

        public Dimension(Concept concept)
        {
            this.Concept = concept;
        }

        public Dimension(Concept concept, CodeList codeList)
        {
            this.Concept = concept;
            this.CodeList = codeList;
        }

        public bool IsCoded
        {
            get
            {
                return CodeList != null;
            }
        }

        internal object GetValue(string value)
        {
            if (IsCoded)
            {
                return CodeList[value];
            }
            else
            {
                throw new Exception("non coded dimensions are not supported yet.");
            }
        }

        public int Order { get; set; }
    }
}
