using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework accordion tag helper.
    /// </summary>
    [HtmlTargetElement("accordion")]
    [RestrictChildren("accordionitem")]
    public class FWAccordionTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWAccordionControl(Id);
            control.Attributes.Add("data-control", "accordion");
            context.Items["accordion"] = control;
            control.ChildBody = ChildContent.GetContent();

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWAccordionTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWAccordionTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
