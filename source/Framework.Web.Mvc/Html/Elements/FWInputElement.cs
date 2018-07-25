using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents an abstract HTML input element.
    /// </summary>
    public class FWInputElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the attribute "value".
        /// </summary>
        public string Value
        {
            get => Attributes["value"];
            set => Attributes["value"] = value;
        }

        /// <summary>
        /// Gets or sets the 'data-bind' attribute.
        /// </summary>
        internal string DataBind
        {
            get => Attributes["data-bind"];
            set => Attributes["data-bind"] = value;
        }

        /// <summary>
        /// Creates a new input element.
        /// </summary>
        public FWInputElement(string name, FWInputType type)
            : base("input")
        {
            Attributes.Add("name", name);

            switch (type)
            {
                case FWInputType.Textbox:
                    Attributes.Add("type", "text");
                    break;
                case FWInputType.Hidden:
                    Attributes.Add("type", "hidden");
                    break;
                case FWInputType.Checkbox:
                    Attributes.Add("type", "checkbox");
                    break;
                case FWInputType.Radio:
                    Attributes.Add("type", "radio");
                    break;
                case FWInputType.Password:
                    Attributes.Add("type", "password");
                    break;
                case FWInputType.Date:
                    Attributes.Add("type", "date");
                    break;
                case FWInputType.Datetime:
                    Attributes.Add("type", "datetime-local");
                    break;
            }
        }
    }
}
