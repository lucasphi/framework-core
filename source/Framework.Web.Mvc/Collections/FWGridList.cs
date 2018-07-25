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
    /// Represents a strongly type list with pagination and selection for Grids.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class FWGridList<T> : FWDataList<T>, IFWSelectable
    {
        /// <summary>
        /// Gets or sets the selected values.
        /// </summary>
        public IEnumerable Selected { get; set; }

        /// <summary>
        /// Initializes a new instance of the Framework.Model.FWGridList class.
        /// </summary>
        /// <param name="pagination">The pagination object.</param>
        public FWGridList(IFWDataOptions pagination)
            : base(pagination)
        { }

        /// <summary>
        /// Initializes a new instance of the Framework.Model.FWGridList class.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pagination">The pagination object.</param>
        public FWGridList(IEnumerable<T> list, IFWDataOptions pagination)
            : base(list, pagination)
        { }
    }
}
