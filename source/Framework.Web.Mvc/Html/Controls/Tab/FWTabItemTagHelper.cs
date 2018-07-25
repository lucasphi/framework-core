using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework tab tag helper.
    /// </summary>
    [HtmlTargetElement("tabitem")]
    public class FWTabItemTagHelper : FWControlTagHelper
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
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var tab = context.Items["tab"] as FWTabControl;

            if (LazyUrl == null)
            {
                tab.AddContentTab(Header, ChildContent.GetContent());    
            }
            else
            {
                tab.AddLazyTab(Header, LazyUrl, Cached);
            }

            return tab;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWTabItemTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWTabItemTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        {
        }
    }
}
