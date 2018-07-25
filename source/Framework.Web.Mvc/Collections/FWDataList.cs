using Framework.Core;
using Framework.Model;
using Framework.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Collections
{
    /// <summary>
    /// Represents a strongly type list with pagination.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class FWDataList<T> : List<T>, IFWDataList, IFWDataOptions
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the amount of items beeing displayed.
        /// </summary>
        public int Display { get; set; }

        /// <summary>
        /// Gets or sets the sorting information.
        /// </summary>
        public List<FWDataSorting> SortInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the Framework.Model.FWList class.
        /// </summary>
        /// <param name="pagination">The pagination object.</param>
        public FWDataList(IFWDataOptions pagination)
        {
            Initialize(pagination);
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Model.FWList class.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pagination">The pagination object.</param>
        public FWDataList(IEnumerable<T> list, IFWDataOptions pagination)
            : base(list)
        {
            Initialize(pagination);
        }

        private void Initialize(IFWDataOptions pagination)
        {
            Page = pagination.Page;
            Total = pagination.Total;
            Display = pagination.Display;
            SortInfo = pagination.SortInfo;
        }
    }

    /// <summary>
    /// Common interface for a data list.
    /// </summary>
    public interface IFWDataList
    { }
}
