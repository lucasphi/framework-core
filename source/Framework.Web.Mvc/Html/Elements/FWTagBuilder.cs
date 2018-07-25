using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;
using System.IO;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Extends the TagBuilder class to add custom behavior. Contains classes and properties that are used to create HTML elements.
    /// </summary>
    public class FWTagBuilder : TagBuilder, IFWHtmlElement, IFWCompositeElement
    {
        /// <summary>
        /// Adds a html string to the body of the tag.
        /// </summary>
        /// <param name="html">The HTML string.</param>
        public virtual void Add(string html)
        {
            if (html != null)
                HtmlList.Add(html);
        }

        /// <summary>
        /// Adds another FWTagBuilder to the body of the tag.
        /// </summary>
        /// <param name="tag">The FWTagBuilder object.</param>
        public virtual T Add<T>(T tag)
            where T : TagBuilder
        {
            if (tag != null)
                HtmlList.Add(tag);
            return tag;
        }

        /// <summary>
        /// Adds a html string to the body of the tag at the specified index.
        /// </summary>
        /// <param name="html">The HTML string.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        public void Append(string html, int index)
        {
            if (html != null)
                HtmlList.Insert(index, html);
        }

        /// <summary>
        /// Adds a html string at the specified index.
        /// </summary>
        /// <param name="tag">The tag object.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        public void Append(TagBuilder tag, int index)
        {
            if (tag != null)
                HtmlList.Insert(index, tag.ToString());
        }

        /// <summary>
        /// Sets the CSS size class.
        /// </summary>
        /// <param name="className">The element name.</param>
        /// <param name="size">The element size.</param>
        public virtual void ElementSizeCSS(string className, FWElementSize size)
        {
            switch (size)
            {
                case FWElementSize.Small:
                    AddCssClass(string.Format("{0}-sm", className));
                    break;
                case FWElementSize.Large:
                    AddCssClass(string.Format("{0}-lg", className));
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the element Id.
        /// </summary>
        public string Id
        {
            get
            {
                return Attributes["id"];
            }
            set
            {
                Attributes["id"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the [data-type] attribute to the div element.
        /// </summary>
        public string DataType
        {
            get
            {
                return Attributes["data-type"];
            }
            set
            {
                Attributes["data-type"] = value;
            }
        }

        /// <summary>
        /// Renders the HTML tag by using the specified render mode.
        /// </summary>
        /// <param name="renderMode">The render mode.</param>
        /// <returns>The rendered HTML tag.</returns>
        public string ToString(TagRenderMode renderMode)
        {
            this.TagRenderMode = renderMode;

            if (renderMode != TagRenderMode.EndTag)
            {
                foreach (var html in HtmlList)
                {
                    InnerHtml.AppendHtml(html.ToString());
                }
            }

            StringWriter writer = new StringWriter();
            WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        /// <summary>
        /// Renders the element as a Microsoft.AspNetCore.Mvc.TagRenderMode.Normal element.
        /// </summary>        
        public override string ToString()
        {
            return this.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Creates a new tag that has the specified tag name.
        /// </summary>
        /// <param name="tagName">The tag name without the "&lt;", "/", or "&gt;" delimiters.</param>
        public FWTagBuilder(string tagName)
            : base(tagName)
        { }

        /// <summary>
        /// Html list field.
        /// </summary>
        protected List<object> HtmlList = new List<object>();
    }
}
