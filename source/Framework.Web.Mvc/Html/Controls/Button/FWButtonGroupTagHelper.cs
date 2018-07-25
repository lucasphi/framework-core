using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework button tag helper.
    /// </summary>
    [HtmlTargetElement("buttongroup")]    
    [RestrictChildren("button", "a")]
    public class FWButtonGroupTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the number of visible buttons in the group.
        /// </summary>
        [HtmlAttributeName("asp-visiblebuttons")]
        public int VisibleButtons { get; set; }

        /// <summary>
        /// Gets or sets the title of the grouped buttons.
        /// </summary>
        [HtmlAttributeName("asp-title")]
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the group color.
        /// </summary>
        [HtmlAttributeName("asp-color")]
        public FWStateColors? Color { get; set; }

        /// <summary>
        /// Gets or sets the button size.
        /// </summary>
        [HtmlAttributeName("asp-size")]
        public FWElementSize? Size { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWButtonGroupControl(Id, VisibleButtons)
            {
                Title = Title
            };
            if (Color.HasValue)
                control.Color(Color.Value);
            if (Size.HasValue)
                control.Size(Size.Value);

            context.Items["btngroup"] = control;
            ChildContent.GetContent();
            // Clears the group button.
            context.Items.Remove("btngroup");

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWButtonGroupTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWButtonGroupTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
