using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML list item element.
    /// </summary>
    public class FWListItemElement : FWTagBuilder
    {
        /// <summary>
        /// Creates a new HTML list item.
        /// </summary>
        public FWListItemElement()
            : base("li")
        { }
    }
}
