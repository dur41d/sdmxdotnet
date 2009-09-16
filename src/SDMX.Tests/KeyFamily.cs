using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    //public class Code
    //{
    //    public string Value { get; private set; }

    //    public Code(string value)
    //    {
    //        Value = value;
    //    }
    //}
    //public class CodeList
    //{
    //    public string Name;
    //    public List<Code> codes = new List<Code>();

    //    public Code this[string codeValue]
    //    {
    //        get
    //        {
    //            var code = Codes.Where(c => c.Value == codeValue).SingleOrDefault();
    //            if (code == null)
    //            {
    //                throw new Exception("Code does not exsist with this value");
    //            }
    //            return code;
    //        }
    //    }
    //}

    public class KeyFamily
    {
        private Dictionary<string, Dimension> dimensions = new Dictionary<string, Dimension>();

        public IEnumerable<Dimension> Dimensions
        {
            get
            {
                return dimensions.Values.AsEnumerable();
            }
        }

        //public SeriesKey CreateSereisKey(string[,] keyValues)
        //{
        //    return new SeriesKey();
        //}


        //internal object GetDimesionValue(string concept, string dimensionValue, out int dimensionOrder)
        //{
        //    var dimension = this.Dimensions.Where(c => c.Concept.Id == concept).FirstOrDefault();
        //    if (dimension == null)
        //    {
        //        throw new ApplicationException("no dimension existions with this concept name.");
        //    }
        //    dimensionOrder = dimension.Order;
        //    return dimension.GetValue(dimensionValue);
        //}

        public Dimension GetDimension(string conceptName)
        {
            return dimensions.GetValueOrDefault(conceptName, null);
        }

        public void AddDimension(Dimension dimension)
        {
            dimensions.Add(dimension.Concept.Id, dimension);
        }
    }
}
