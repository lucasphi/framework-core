using Framework.Web.Mvc.Html.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML table row element.
    /// </summary>
    public class FWTableLineElement : FWTagBuilder
    {
        /// <summary>
        /// Creates a new HTML table row element.
        /// </summary>
        public FWTableLineElement() 
            : base("tr")
        { }
    }
}
