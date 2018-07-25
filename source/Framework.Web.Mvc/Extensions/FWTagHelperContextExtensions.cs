using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Razor.TagHelpers
{
    static class FWTagHelperContextExtensions
    {
        /// <summary>
        /// Prevents the controls from now on to render data-control tags.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        public static void StartInnerContent(this TagHelperContext context)
        {
            context.Items["TemplateControl"] = true;
        }

        /// <summary>
        /// Allows the controls to be rendered with data-control tags again.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        public static void EndInnerContent(this TagHelperContext context)
        {
            context.Items.Remove("TemplateControl");
        }

        /// <summary>
        /// Checks if the control is an inner control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <returns>True if the control is inside another one. Otherwise returns false.</returns>
        public static bool IsInnerContent(this TagHelperContext context)
        {
            return context.Items.ContainsKey("TemplateControl");
        }
    }
}
