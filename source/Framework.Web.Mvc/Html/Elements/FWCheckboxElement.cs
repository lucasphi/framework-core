using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents an HTML Checkbox element.
    /// </summary>
    public class FWCheckboxElement : FWInputElement
    {
        /// <summary>
        /// Gets or sets if the checkbox is marked or not.
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
        /// Creates a new checkbox element.
        /// </summary>
        /// <param name="name">The element name.</param>
        public FWCheckboxElement(string name)
            : base(name, FWInputType.Checkbox)
        { }
    }
}
