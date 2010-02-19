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
    internal class SimpleElementClassMap<T> : ClassMap<T>
    {
        T instance;

        internal ISimpleTypeConverter<T> Converter { get; set; }

        public SimpleElementClassMap(ISimpleTypeConverter<T> converter)
        {
            Converter = converter;

            Map(o => o).ToContent()
                .Set(v => instance = v)
                .Converter(converter);
        }

        protected override T Return()
        {
            return instance;
        }
    }
}
