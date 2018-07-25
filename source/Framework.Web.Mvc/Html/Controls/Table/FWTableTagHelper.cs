using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework table tag helper.
    /// </summary>
    [HtmlTargetElement("table")]
    public class FWTableTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the model expression for the control property.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Gets or sets if the table columns have sizes defined.
        /// </summary>
        [HtmlAttributeName("asp-autosize")]
        public bool AutoSizeColumns { get; set; } = true;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWTableControl(RequestContext, For.Model as IEnumerable, For.Metadata);
            control.Attributes.Add("data-control", "table");
            control.AutoSizeColumns = AutoSizeColumns;
            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWTableTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="metadataProvider">The model metadata provider.</param>
        public FWTableTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider metadataProvider) 
            : base(urlHelperFactory, actionAccessor, metadataProvider)
        { }
    }
}
