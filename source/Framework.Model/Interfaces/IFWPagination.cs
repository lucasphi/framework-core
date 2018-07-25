using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model
{
    /// <summary>
    /// Defines the common interface for pagination classes.
    /// </summary>
    public interface IFWPagination
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        int Total { get; set; }

        /// <summary>
        /// Gets or sets the amount of items beeing displayed.
        /// </summary>
        int Display { get; set; }
    }
}
