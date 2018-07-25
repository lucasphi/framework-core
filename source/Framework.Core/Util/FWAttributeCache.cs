using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Util
{
    /// <summary>
    /// Represents cached attributes.
    /// </summary>
    public class FWAttributeCache
    {
        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public object this[string key]
        {
            get
            {
                return cachedAttributes[key];
            }
        }

        /// <summary>
        /// Determines whether the key is cached or not.
        /// </summary>
        /// <param name="key">The key to locate in the cache.</param>
        /// <returns>True if the key is cached. False otherwise.</returns>
        public bool ContainsKey(string key)
        {
            return cachedAttributes.ContainsKey(key);
        }

        /// <summary>
        /// Adds a value to the attribute cache.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The cache value.</param>
        public void Add(string key, object value)
        {
            lock(_lock)
            {
                if (!cachedAttributes.ContainsKey(key))
                    cachedAttributes.Add(key, value);
            }
        }

        private static Dictionary<string, object> cachedAttributes = new Dictionary<string, object>();
        private static object _lock = new object();
    }
}
