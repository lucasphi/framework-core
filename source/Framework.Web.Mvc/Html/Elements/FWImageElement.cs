using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML image element.
    /// </summary>
    public class FWImageElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public string Source
        {
            get
            {
                return Attributes["src"];
            }
            set
            {
                Attributes["src"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the image alt.
        /// </summary>
        public string Alternate
        {
            get
            {
                return Attributes["alt"];
            }
            set
            {
                Attributes["alt"] = value;
            }
        }

        /// <summary>
        /// Creates a new Image element.
        /// </summary>
        public FWImageElement() 
            : base("img")
        { }
    }
}
