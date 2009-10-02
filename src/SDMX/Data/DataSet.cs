using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;

namespace SDMX
{
    public class DataSet : AnnotableArtefact
    {
        public KeyFamily KeyFamily { get; internal set; }
        public IList<Group> Groups { get; internal set; }
        public IList<Series> Series { get; internal set; }
        public AttributeValues Attributes { get; internal set; }        

        public DataSet()
        {
            Groups = new List<Group>();
            Series = new List<Series>();
            Attributes = new AttributeValues();
            Annotations = new List<Annotation>();            
        }

        public DataSet(KeyFamily keyFamily) : this()
        {            
            this.KeyFamily = keyFamily;
        }

        public Series CreateEmptySeries()
        {
            var series = new Series(this);
            return series;
        }

        public static DataSet Parse(XElement dataSetElement, KeyFamily keyFamiliy)
        {
            GenericDataSetParser parser = new GenericDataSetParser(keyFamiliy);
            return parser.Parse(dataSetElement);
        }
    }
}
