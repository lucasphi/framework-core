using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Mvc.Html.Controls.Grid;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework grid column tag helper.
    /// </summary>
    [HtmlTargetElement("gridcolumn")]
    [RestrictChildren("button", "label", "span", "textbox", "checkbox", "radio", "currency", "select", "datepicker", "hidden")]
    public class FWGridColumnTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the column class.
        /// </summary>
        [HtmlAttributeName("class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the column order.
        /// </summary>
        public int ColumnOrder { get; set; } = int.MaxValue;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var options = context.Items["TemplateOptions"] as FWGridOptions;
            if (options == null)
                return null;

            context.StartInnerContent();
            var content = ChildContent.GetContent();
            context.EndInnerContent();

            if (content != null)
            {
                // TODO: Url.Action will encode { and } characters. Is there anyway to improve the performance by removing the replaces below?
                content = content.Replace("%7B", "{").Replace("%7D", "}");
                options.FluentConfiguration.Add(x => x.AddColumn(CssClass, ColumnOrder, content));
            }

            return null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWGridColumnTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWGridColumnTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
