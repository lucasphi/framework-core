using Framework.Core;
using Framework.Model.Chart;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework chart tag helper.
    /// </summary>
    [HtmlTargetElement("chart")]
    public class FWChartTagHelper : FWControlTagHelper
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
            if (!typeof(IFWChart).IsAssignableFrom(For.Metadata.ModelType) || For.Model == null)
                throw new FWInvalidModelException($"The chart {Id} does not have a valid data!");

            string chartKey = $"chartKey{Id}";
            RequestContext.HttpContext.Session.SetString(chartKey, (For.Model as IFWChart).CreateChart());

            var control = new FWChartControl(RequestContext, Id);
            control.Attributes.Add("data-control", "chart");
            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWChartTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWChartTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
