using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents an HTML label element.
    /// </summary>
    public class FWLabelElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the attribute "for".
        /// </summary>
        public string For
        {
            get
            {
                return Attributes["for"];
            }
            set
            {
                Attributes["for"] = value;
            }
        }

        /// <summary>
        /// Creates a new label element.
        /// </summary>
        public FWLabelElement()
            : base("label")
        { }
    }
}
