using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Extension methods for collections.
    /// </summary>
    public static class FWEnumerableExtensions
    {
        /// <summary>
        /// Adds a collection to another.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="target">The target collection.</param>
        /// <param name="source">The source collection.</param>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
                target.Add(element);
        }
    }
}
