using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework panel tag helper.
    /// </summary>
    [HtmlTargetElement("portlet")]
    public class FWPortletTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the panel title.
        /// </summary>
        [HtmlAttributeName("asp-header")]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the portlet icon.
        /// </summary>
        [HtmlAttributeName("asp-icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets if the portlet allows fullscreen mode.
        /// </summary>
        [HtmlAttributeName("asp-allowfullscreen")]
        public bool AllowFullscreen { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWPortletControl(Id, Header)
            {
                AllowFullscreen = AllowFullscreen
            };
            control.Attributes.Add("data-control", "panel");
            control.ChildBody = ChildContent.GetContent();

            if (Icon != null)
                control.Icon(Icon);

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWPortletTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWPortletTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
