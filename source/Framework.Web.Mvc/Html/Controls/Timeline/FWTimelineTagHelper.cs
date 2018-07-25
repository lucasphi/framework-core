using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework accordion tag helper.
    /// </summary>
    [HtmlTargetElement("timeline")]
    public class FWTimelineTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the timeline entries.
        /// </summary>
        [HtmlAttributeName("asp-entries")]
        public IEnumerable<FWTimelineEntry> Entries { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWTimelineControl(Id, Entries, RequestContext);
            control.Attributes.Add("data-control", "accordion");
            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWTimelineTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWTimelineTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
        : base(urlHelperFactory, actionAccessor)
        { }
    }
}
