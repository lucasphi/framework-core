using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents an HTML Radio-Button element.
    /// </summary>
    public class FWRadiobuttonElement : FWInputElement
    {
        /// <summary>
        /// Gets or sets if the radiobutton is marked or not.
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return Attributes["checked"] == "checked";
            }
            set
            {
                if (value)
                {
                    Attributes["checked"] = "checked";
                }
                else
                {
                    Attributes.Remove("checked");
                }
            }
        }

        /// <summary>
        /// Creates a new Radio-Button element.
        /// </summary>
        /// <param name="name">The element name.</param>
        public FWRadiobuttonElement(string name) 
            : base(name, FWInputType.Radio)
        { }
    }
}
