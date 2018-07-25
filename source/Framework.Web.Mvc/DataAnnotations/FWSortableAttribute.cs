using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Sets the property as a sort parameter.
    /// </summary>
    public class FWSortableAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Gets or sets the name of the sort.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        public FWSortDirection SortDirection { get; set; }

        /// <summary>
        /// Gets or sets if the user can change the sort direction.
        /// </summary>
        public bool AllowReorder { get; set; } = true;
    }
}
