using Framework.Model;
using Framework.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Models
{
    /// <summary>
    /// Reprensents a temporary model for binding the data for data options.
    /// </summary>
    public class FWDataOptionsViewModel : IFWDataOptions
    {
        /// <inheritdoc />
        public int Page { get; set; } = 1;

        /// <inheritdoc />
        public int Total { get; set; }

        /// <inheritdoc />
        public int Display { get; set; } = 10;

        /// <inheritdoc />
        public List<FWDataSorting> SortInfo { get; set; }

        /// <summary>
        /// The id of the control.
        /// </summary>
        public string CId { get; set; }        
    }
}
