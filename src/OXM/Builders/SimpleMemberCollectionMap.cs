using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;

namespace OXM
{
    public class SimpleMemberCollectionMap<TObj, TProperty>
    {
        ISimpleTypeConverter<TProperty> _converter;
        MemberCollectionMap<TObj, TProperty> _memberCollectionMap;

        public SimpleMemberCollectionMap(Expression<Func<TObj, IEnumerable<TProperty>>> collection)
        {
            _memberCollectionMap = new MemberCollectionMap<TObj, TProperty>(collection);
        }

        public SimpleMemberCollectionMap<TObj, TProperty> Set(Action<IEnumerable<TProperty>> set)
        {
            _memberCollectionMap.Set(set);
            return this;
        }

        public SimpleMemberCollectionMap<TObj, TProperty> Converter(ISimpleTypeConverter<TProperty> converter)
        {
            _converter = converter;
            return this;
        }

        internal Collection<TObj, TProperty> GetCollection()
        {
            return _memberCollectionMap.GetCollection();
        }

        internal ISimpleTypeConverter<TProperty> GetConverter()
        {
            if (_converter == null)
            {
                throw new OXMException("Converter is not set for collection.");
            }

            return _converter;
        }
    }
}
