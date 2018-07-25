using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Data
{
    /// <summary>
    /// Represents a sorting option.
    /// </summary>
    public class FWDataSorting
    {
        /// <summary>
        /// Gets or sets the name of the sorting.
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// Gets or sets the direction of the sorting.
        /// </summary>
        public FWSortDirection SortDirection { get; set; }
    }
}
