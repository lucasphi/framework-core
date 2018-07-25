using System;
using System.Collections.Generic;
using System.Text;
using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework modal tag helper.
    /// </summary>
    [HtmlTargetElement("modal")]
    [RestrictChildren("modalbody", "modalfooter")]
    public class FWModalTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the modal title.
        /// </summary>
        [HtmlAttributeName("asp-title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url to load the content from.
        /// </summary>
        [HtmlAttributeName("asp-dataurl")]
        public string ContentUrl { get; set; }

        /// <summary>
        /// Gets or sets the form post url.
        /// </summary>
        [HtmlAttributeName("asp-formurl")]
        public string FormUrl { get; set; }

        /// <summary>
        /// Gets or sets the element size.
        /// </summary>
        [HtmlAttributeName("asp-size")]
        public FWElementSize Size { get; set; } = FWElementSize.Regular;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWModalControl(Id, Title, Size)
            {
                // Sets the url to load the content from.
                ContentUrl = ContentUrl,

                // Sets the modal form action.
                FormUrl = FormUrl
            };
            control.Attributes.Add("data-control", "modal");

            context.Items["modal"] = control;
            // If no url is defined, load the body from the tag helper child content.
            ChildContent.GetContent();
            context.Items.Remove("modal");

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWModalTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWModalTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
