using System;
using System.Collections.Generic;
using System.Text;
using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework upload tag helper.
    /// </summary>
    [HtmlTargetElement("upload")]
    [RestrictChildren("uploadlayout")]
    public class FWUploadTagHelper : FWInputControlTagHelper
    {
        /// <summary>
        /// Gets or sets the upload layout.
        /// </summary>
        [HtmlAttributeName("asp-layout")]
        public FWUploadLayout Layout { get; set; } = FWUploadLayout.Input;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWUploadControl(RequestContext, For.Model, For.Metadata, Layout);
            control.Attributes.Add("data-control", "upload");

            if (Layout == FWUploadLayout.Custom)
            {
                control.LayoutBody = ChildContent.GetContent();
            }

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWUploadTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWUploadTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider)
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }
    }
}
