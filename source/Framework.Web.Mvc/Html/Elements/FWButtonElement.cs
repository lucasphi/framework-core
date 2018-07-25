using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML button element.
    /// </summary>
    public class FWButtonElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the button type.
        /// </summary>
        public FWButtonType ButtonType
        {
            get
            {
                var att = Attributes["type"];
                switch (att)
                {
                    case "submit":
                        return FWButtonType.Submit;
                    case "reset":
                        return FWButtonType.Reset;
                }
                return FWButtonType.Button;
            }
            set
            {
                Attributes["type"] = value.ToString().ToLower();
            }
        }

        /// <summary>
        /// Sets the CSS size class.
        /// </summary>
        public void Size(FWElementSize size)
        {
            ElementSizeCSS("btn", size);
        }

        /// <summary>
        /// Creates a new HTML button element.
        /// </summary>
        public FWButtonElement()
            : base("button")
        { }
    }
}
