using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework label tag helper.
    /// </summary>
    [HtmlTargetElement("label")]
    public class FWLabelControlTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the model expression for the control property.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            FWLabelControl control = new FWLabelControl(For.Metadata.PropertyName, For.Metadata.DisplayName, For.Metadata.IsRequired, null);
            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWLabelControlTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWLabelControlTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
