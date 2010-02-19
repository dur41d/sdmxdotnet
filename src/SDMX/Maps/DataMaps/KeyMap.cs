using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class KeyMap : ClassMap<ReadOnlyKey>
    {
        Key key;
        public KeyMap(DataSet dataSet)
        {
            key = dataSet.NewKey();

            MapCollection(o => o).ToElement("Value", true)
                .Set(v => key[v.Key] = v.Value)
                .ClassMap(() => new ValueMap(dataSet.KeyFamily));
        }

        protected override ReadOnlyKey Return()
        {
            return new ReadOnlyKey(key);
        }
    }
}
