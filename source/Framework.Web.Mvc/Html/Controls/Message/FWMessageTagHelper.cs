using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework message tag helper.
    /// </summary>
    [HtmlTargetElement("message")]
    public class FWMessageTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        [HtmlAttributeName("asp-type")]
        public FWMessageType Type { get; set; }

        /// <summary>
        /// Gets or sets if the message can be close.
        /// </summary>
        [HtmlAttributeName("asp-allowclose")]
        public bool AllowClose { get; set; } = true;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var message = ChildContent.GetContent();

            var control = new FWMessageControl(message, Type);
            control.AllowClose(AllowClose);
            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMessageTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWMessageTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
