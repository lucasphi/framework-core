using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML span element.
    /// </summary>
    public class FWSpanElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the 'data-bind' attribute.
        /// </summary>
        internal string DataBind
        {
            get => Attributes["data-bind"];
            set => Attributes["data-bind"] = value;
        }

        /// <summary>
        /// Creates a new HTML span element.
        /// </summary>
        public FWSpanElement()
            : base("span")
        { }

        /// <summary>
        /// Creates a new HTML span element.
        /// </summary>
        /// <param name="text">The span text.</param>
        public FWSpanElement(string text)
            : this()
        {
            Add(text);
        }
    }
}
