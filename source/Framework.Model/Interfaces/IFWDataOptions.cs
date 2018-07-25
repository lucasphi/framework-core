using Framework.Core;
using Framework.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model
{
    /// <summary>
    /// Defines the common interface for query filtering and sorting.
    /// </summary>
    public interface IFWDataOptions : IFWPagination
    {
        /// <summary>
        /// Gets or sets the sorting information.
        /// </summary>
        List<FWDataSorting> SortInfo { get; set; }
    }
}
