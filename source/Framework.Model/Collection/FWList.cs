using Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Collections
{
    /// <summary>
    /// Represents a strongly typed list that stores removed items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class FWList<T> : List<T>, IFWList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWList&lt;Task&gt;" />.
        /// </summary>
        public FWList()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWList&lt;Task&gt;" /> that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection"></param>
        public FWList(IEnumerable<T> collection)
            : base(collection)
        { }

        /// <summary>
        /// Gets the list removed items.
        /// </summary>
        public IEnumerable RemovedItems
        {
            get
            {
                return _removedItems.Values;
            }
        }

        /// <summary>
        /// Removes items from the list.
        /// </summary>
        /// <param name="removedItems">The removed item list.</param>
        public void RemoveItems(IEnumerable<int> removedItems)
        {
            if (_removedItems.Count == 0)
            {
                removedItems = removedItems.OrderByDescending(f => f).ToList();
                foreach (var index in removedItems)
                {
                    _removedItems.Add(index, this[index]);
                    this.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Restore all cleared items to the list.
        /// </summary>
        public void UndoClear()
        {
            if (_removedItems.Count > 0)
            {
                for (int i = _removedItems.Count - 1; i >= 0; i--)
                {
                    int key = _removedItems.Keys.ElementAt(i);
                    this.Insert(key, _removedItems[key]);
                }
                _removedItems.Clear();
            }
        }

        [FWMapPrivate]
        private Dictionary<int, T> _removedItems = new Dictionary<int, T>();
    }
}
