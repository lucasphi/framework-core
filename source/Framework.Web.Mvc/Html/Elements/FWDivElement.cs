using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML div element.
    /// </summary>
    public class FWDivElement : FWTagBuilder
    {   
        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        public FWDivElement()
            : base ("div")
        { }
    }
}
