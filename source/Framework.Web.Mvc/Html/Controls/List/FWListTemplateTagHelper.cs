using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.List
{
    /// <summary>
    /// Framework list template tag helper.
    /// </summary>
    [HtmlTargetElement("listtemplate")]
    [RestrictChildren("button", "label", "span", "textbox", "checkbox", "radio", "currency", "select", "datepicker", "hidden")]
    public class FWListTemplateTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the column class.
        /// </summary>
        [HtmlAttributeName("class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var options = context.Items["TemplateOptions"] as FWListOptions;
            if (options == null)
                return null;

            context.StartInnerContent();
            var content = ChildContent.GetContent();
            context.EndInnerContent();

            if (content != null)
            {
                // TODO: Url.Action will encode { and } characters. Is there anyway to improve the performance by removing the replaces below?
                content = content.Replace("%7B", "{").Replace("%7D", "}");
                options.FluentConfiguration.Add(x => x.Template(content).ColumnClass(CssClass));
            }

            return null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWListTemplateTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWListTemplateTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
