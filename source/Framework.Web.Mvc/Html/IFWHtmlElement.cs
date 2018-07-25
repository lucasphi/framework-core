using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Represents the framework html element base interface.
    /// </summary>
    public interface IFWHtmlElement : IHtmlContent
    {
        /// <summary>
        /// Gets or sets the element html attributes.
        /// </summary>
        AttributeDictionary Attributes { get; }

        /// <summary>
        /// Adds a CSS class to the list of CSS classes.
        /// </summary>
        /// <param name="css">The CSS class to add.</param>
        void AddCssClass(string css);
    }
}
