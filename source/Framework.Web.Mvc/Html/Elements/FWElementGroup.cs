using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.Encodings.Web;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a group of elements with the same hierarchical level.
    /// </summary>
    public class FWElementGroup : IHtmlContent, IFWCompositeElement
    {
        /// <summary>
        /// Adds a tag to the group.
        /// </summary>
        /// <param name="tag">The tag object.</param>
        public T Add<T>(T tag)
            where T: TagBuilder
        {
            _tags.Add(tag.ToString());
            return tag;
        }

        /// <summary>
        /// Adds a html element to the group.
        /// </summary>
        /// <param name="element">The element html string.</param>
        public void Add(string element)
        {
            _tags.Add(element);
        }

        /// <summary>
        /// Adds a html to the group at the specified index.
        /// </summary>
        /// <param name="html">The HTML string.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        public void Append(string html, int index)
        {
            _tags.Insert(index, html);
        }

        /// <summary>
        /// Adds a tag to the group at the specified index.
        /// </summary>
        /// <param name="tag">The tag object.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        public void Append(TagBuilder tag, int index)
        {
            _tags.Insert(index, tag.ToString());
        }

        /// <summary>
        /// Creates an HTML string for all the tags added.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var tag in _tags)
            {
                sb.Append(tag);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Writes the content by encoding it with the specified encoder to the specified writer.
        /// </summary>
        /// <param name="writer">The System.IO.TextWriter to which the content is written.</param>
        /// <param name="encoder">The System.Text.Encodings.Web.HtmlEncoder which encodes the content to be written.</param>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write(this.ToString());
        }

        private List<string> _tags = new List<string>();
    }
}
