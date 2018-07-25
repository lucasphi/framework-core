using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML textarea list element.
    /// </summary>
    public class FWTextareaElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the attribute "value".
        /// </summary>
        public string Value
        {
            get => string.Join(" ", HtmlList);
            set => Add(value);
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
        /// Adds a html string to the body of the tag.
        /// </summary>
        /// <param name="html">The HTML string.</param>
        public override void Add(string html)
        {
            HtmlList.Clear();
            base.Add(html);
        }

        /// <summary>
        /// Adds another FWTagBuilder to the body of the tag.
        /// </summary>
        /// <param name="tag">The FWTagBuilder object.</param>
        public override T Add<T>(T tag)
        {
            HtmlList.Clear();
            return base.Add(tag);
        }        

        /// <summary>
        /// Creates a new textarea element.
        /// </summary>
        public FWTextareaElement(string name) 
            : base("textarea")
        {
            Attributes.Add("name", name);
        }
    }
}
