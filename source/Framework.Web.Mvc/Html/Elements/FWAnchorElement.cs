using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents an HTML anchor element.
    /// </summary>
    public class FWAnchorElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the href attribute.
        /// </summary>
        public string Href
        {
            get
            {
                return Attributes["href"];
            }
            set
            {
                Attributes["href"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the anchor title.
        /// </summary>
        public string Title
        {
            get
            {
                return Attributes["title"];
            }
            set
            {
                Attributes["title"] = value;
            }
        }

        /// <summary>
        /// Creates a new anchor element.
        /// </summary>
        public FWAnchorElement(string href)
            : base("a")
        {
            this.Href = href;
        }
    }
}
