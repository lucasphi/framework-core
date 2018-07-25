using Framework.Core;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework radio tag helper.
    /// </summary>
    [HtmlTargetElement("detail")]
    public class FWDetailTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the model expression for the control property.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Gets or sets the detail control.
        /// </summary>
        [HtmlAttributeName("asp-type")]
        public FWDetailType Type { get; set; } = FWDetailType.Table;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            if (!FWReflectionHelper.IsCollection(For.Metadata.ModelType))
            {
                throw new FWInvalidModelException("Invalid model for detail. Must use a collection.");
            }

            FWControl control = null;

            switch (Type)
            {
                case FWDetailType.Table:
                    control = new FWDetailTableControl(RequestContext, For.Model as IEnumerable, For.Metadata);
                    control.Attributes.Add("data-detailtype", "table");
                    break;
                case FWDetailType.Grid:
                    control = new FWDetailGridControl(RequestContext, For.Model as IEnumerable, For.Metadata);
                    control.Attributes.Add("data-detailtype", "grid");
                    break;
            }

            control.Attributes.Add("data-control", "detail");

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWDetailTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="metadataProvider">The model metadata provider.</param>
        public FWDetailTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider metadataProvider) 
            : base(urlHelperFactory, actionAccessor, metadataProvider)
        { }

        /// <summary>
        /// Gets or set the current view context.
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { set => RequestContext.Model = value.ViewData.Model; }
    }
}
