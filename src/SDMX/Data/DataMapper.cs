using System;
using System.Collections.Generic;
using System.Reflection;
using Common;

namespace SDMX
{
    public class DataMapper
    {
        Dictionary<string, KeyValuePair<string, object>> _maps = new Dictionary<string, KeyValuePair<string, object>>();

        public bool HasMaps
        {
            get
            {
                return _maps.Count > 0;
            }
        }

        /// <summary>
        /// Allows to map the reader field names to different ones.
        /// </summary>        
        /// <param name="source">The name of the source field.</param>
        /// /// <param name="source">The name of the destination field.</param>
        /// <param name="castAction">The cast action.</param>
        public void Map(string source, string destination)
        {
            Contract.AssertNotNullOrEmpty(source, "source");
            Contract.AssertNotNullOrEmpty(destination, "destination");
            MapInternal(source, destination, null);
        }

        /// <summary>
        /// Allows to map the reader field names to different ones and set the cast action.
        /// </summary>        
        /// <param name="source">The name of the source field.</param>
        /// /// <param name="source">The name of the destination field.</param>
        /// <param name="castAction">The cast action.</param>
        public void Map(string source, string destination, Func<object, object> castAction)
        {
            Contract.AssertNotNullOrEmpty(source, "source");
            Contract.AssertNotNullOrEmpty(destination, "destination");
            Contract.AssertNotNull(castAction, "castAction");
            MapInternal(source, destination, castAction);
        }

        void MapInternal(string source, string destination, Func<object, object> castAction)
        {
            var value = new KeyValuePair<string, object>(destination, castAction);
            _maps.Add(source, value);
        }

        object Cast(string source, string destination, object castAction, object oldValue)
        {
            try
            {
                return ((Delegate)castAction).DynamicInvoke(oldValue);
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException && ex.InnerException != null)
                    ex = ex.InnerException;

                string message = string.Format("Exception occured in the cast action 'Map(\"{0}\",\"{1}\", castAction)' when casting value '{2}' of type '{3}' (see inner exception for details): {4}", source, destination, oldValue, oldValue.GetType(), ex.Message);
                throw new SDMXException(message, ex);
            }
        }

        public void MapRecord(Dictionary<string, object> target, Dictionary<string, KeyValuePair<string, object>> source)
        {
            foreach (var item in source)
            {
                var mapValue = new KeyValuePair<string, object>();
                string name = item.Key;
                object value = item.Value.Value;
                if (_maps.TryGetValue(item.Key, out mapValue))
                {
                    name = mapValue.Key;
                    if (value != null && mapValue.Value != null)
                    {
                        value = Cast(item.Key, mapValue.Key, mapValue.Value, value);
                    }
                }

                target[name] = value;
            }
        }

        public bool TryGetValue(string name, out KeyValuePair<string, object> value)
        {
            return _maps.TryGetValue(name, out value);
        }
    }
}
