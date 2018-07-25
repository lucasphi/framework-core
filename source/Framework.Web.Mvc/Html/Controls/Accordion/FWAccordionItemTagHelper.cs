using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.Accordion
{
    /// <summary>
    /// Framework accordion tag helper.
    /// </summary>
    [HtmlTargetElement("accordionitem")]
    public class FWAccordionItemTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the tab title.
        /// </summary>
        [HtmlAttributeName("asp-header")]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the url to call the lazy tab content.
        /// </summary>
        [HtmlAttributeName("asp-url")]
        public string LazyUrl { get; set; }

        /// <summary>
        /// Gets or sets if the tab content should be cached.
        /// </summary>
        [HtmlAttributeName("asp-cache")]
        public bool Cached { get; set; } = true;

        /// <summary>
        /// Gets or sets the item header icon.
        /// </summary>
        [HtmlAttributeName("asp-icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var tab = context.Items["accordion"] as FWAccordionControl;

            if (LazyUrl == null)
            {
                tab.AddContentItem(Header, ChildContent.GetContent(), Icon);
            }
            else
            {
                tab.AddLazyItem(Header, LazyUrl, Cached, Icon);
            }

            return tab;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWAccordionItemTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWAccordionItemTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
