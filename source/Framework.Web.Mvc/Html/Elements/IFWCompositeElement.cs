using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Defines the common interface for composite elements.
    /// </summary>
    public interface IFWCompositeElement
    {
        /// <summary>
        /// Adds a tag.
        /// </summary>
        /// <param name="tag">The tag object.</param>
        T Add<T>(T tag) where T : TagBuilder;

        /// <summary>
        /// Adds a html element.
        /// </summary>
        /// <param name="element">The element html string.</param>
        void Add(string element);

        /// <summary>
        /// Adds a html string at the specified index.
        /// </summary>
        /// <param name="html">The HTML string.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        void Append(string html, int index);

        /// <summary>
        /// Adds a html string at the specified index.
        /// </summary>
        /// <param name="tag">The tag object.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        void Append(TagBuilder tag, int index);
    }
}
