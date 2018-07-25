using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Mvc.Html.Elements;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework upload layout tag helper.
    /// </summary>
    [HtmlTargetElement("uploadlayout")]
    public class FWUploadLayoutTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var element = new FWDivElement
            {
                DataType = "upload-template"
            };
            element.Add(ChildContent.GetContent());
            return element;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWUploadLayoutTagHelper"/> class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWUploadLayoutTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
