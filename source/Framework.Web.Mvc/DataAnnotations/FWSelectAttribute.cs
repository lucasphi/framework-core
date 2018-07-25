using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Marks a property to be displayed as a Select.
    /// </summary>
    public class FWSelectAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Gets or sets the select datasource.
        /// </summary>
        public string DataSource { get; set; }
    }
}
