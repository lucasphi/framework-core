using Framework.Web.Mvc.Helper;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents the base control class.
    /// </summary>
    public abstract class FWControl : IFWHtmlElement
    {
        /// <summary>
        /// Creates an empty control.
        /// </summary>
        public static FWControl EmptyControl
        {
            get
            {
                return new FWEmptyControl();
            }
        }

        /// <summary>
        /// Adds a CSS class to the list of CSS classes in the control.
        /// </summary>
        /// <param name="css">The CSS class to add.</param>
        public void AddCssClass(string css)
        {
            if (!string.IsNullOrWhiteSpace(css))
            {
                StringBuilder sb = new StringBuilder(CustomCss);

                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(css);

                CustomCss = sb.ToString();
            }
        }

        /// <summary>
        /// Removes all css classes added to this point.
        /// </summary>
        protected void ClearCss()
        {
            CustomCss = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWControl" />.
        /// </summary>
        /// <param name="id">The control Id.</param>
        public FWControl(string id)
            : this()
        {
            Id = id;
            OriginalId = id;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWControl class.
        /// </summary>
        protected FWControl()
        { }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns></returns>
        protected virtual IFWHtmlElement CreateControl() { return null; }

        /// <summary>
        /// Writes the content by encoding it with the specified encoder to the specified writer.
        /// </summary>
        /// <param name="writer">The System.IO.TextWriter to which the content is written.</param>
        /// <param name="encoder">The System.Text.Encodings.Web.HtmlEncoder which encodes the content to be written.</param>
        public virtual void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var controlContent = CreateControl();

            // Set the control size
            foreach (var size in Sizes)
            {
                controlContent.AddCssClass(FWGridSizeHelper.GetCss(size.Size, size.Device));
            }

            writer.Write(controlContent?.ToString());
        }

        /// <summary>
        /// Converts the control to an html string.
        /// </summary>
        /// <returns>The control html string.</returns>
        public override string ToString()
        {
            StringWriter writer = new StringWriter();
            WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the control Id.
        /// </summary>
        internal string Id { get; set; }

        /// <summary>
        /// Gets the original control ID. This value cannot be changed.
        /// </summary>
        internal string OriginalId { get; private set; }
        
        /// <summary>
        /// Gets or sets the control title.
        /// </summary>
        internal string Title { get; set; }

        /// <summary>
        /// Gets or sets the control
        /// </summary>
        internal string CustomCss { get; private set; }

        /// <summary>
        /// Gets or sets the control visibility.
        /// </summary>
        internal bool IsHidden { get; set; }

        /// <summary>
        /// Gets the control html attributes
        /// </summary>
        public AttributeDictionary Attributes { get; private set; } = new AttributeDictionary();

        /// <summary>
        /// Gets the control size handler.
        /// </summary>
        internal List<FWControlSize> Sizes { get; private set; } = new List<FWControlSize>();

        #endregion
    }

    class FWEmptyControl : FWControl
    { }
}
