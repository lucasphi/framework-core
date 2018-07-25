using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML (unordered or ordered) list element.
    /// </summary>
    public class FWListElement : FWTagBuilder
    {
        /// <summary>
        /// Creates a new undordered list element.
        /// </summary>
        public FWListElement()
            : base("ul")
        { }

        /// <summary>
        /// Creates a new list element.
        /// </summary>
        /// <param name="listType">The type of the list.</param>
        public FWListElement(FWListType listType)
            : base((listType == FWListType.Unordered) ? "ul" : "ol")
        { }
    }

    /// <summary>
    /// Enumerates the list types.
    /// </summary>
    public enum FWListType
    {
        /// <summary>
        /// Unordered list.
        /// </summary>
        Unordered,
        
        /// <summary>
        /// Ordered list.
        /// </summary>
        Ordered
    }
}
