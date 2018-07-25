using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Framework.Collections
{
    /// <summary>
    /// Defines the interface for the FWDetailList.
    /// </summary>
    public interface IFWList : IList
    {
        /// <summary>
        /// Sets the list of the removed itens indexes.
        /// </summary>
        /// <param name="items">The removed item list.</param>
        void RemoveItems(IEnumerable<int> items);

        /// <summary>
        /// Gets the list removed items.
        /// </summary>
        IEnumerable RemovedItems { get; }
    }
}
