using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class GenericKeyMap : ClassMap<ReadOnlyKey>
    {
        Key key;
        public GenericKeyMap(DataSet dataSet)
        {
            key = dataSet.NewKey();

            MapCollection(o => o).ToElement("Value", true)
                .Set(v => key[v.Id] = v.Value)
                .ClassMap(() => new GenericValueMap(dataSet.KeyFamily));
        }

        protected override ReadOnlyKey Return()
        {
            return new ReadOnlyKey(key);
        }
    }
}
