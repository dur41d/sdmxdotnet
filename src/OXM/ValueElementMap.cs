using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{
    public class ValueElementMap<T> : ClassMap<T>
    {
        ValueMap<T, T> _valueMap;
        public ValueElementMap(Func<string, T> parser)
        {
            _valueMap = MapValue<T>()
                        .Getter(o => o)
                        .Parser(s => parser(s));
        }
    }
}
