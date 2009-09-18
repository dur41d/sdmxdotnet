using System.Collections.Generic;
using System;

namespace SDMX.Tests
{
    public class DataSet
    {
        public KeyFamily KeyFamily { get; internal set; }
        public IList<Group> Groups { get; internal set; }
        public IList<Series> Series { get; internal set; }
        public IList<Attribute> Attributes { get; internal set; }
        public IList<Annotation> Annotations { get; internal set; }

        public DataSet()
        {
            Groups = new List<Group>();
            Series = new List<Series>();
            Attributes = new List<Attribute>();
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
    }
}
