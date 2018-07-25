using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework textbox tag helper.
    /// </summary>
    [HtmlTargetElement("textarea")]
    public class FWTextareaTagHelper : FWInputControlTagHelper
    {
        /// <summary>
        /// Gets or sets if the control can be resized.
        /// </summary>
        [HtmlAttributeName("asp-resizable")]
        public bool? Resizable { get; set; }

        /// <summary>
        /// Gets or sets the textarea number or rows.
        /// </summary>
        [HtmlAttributeName("rows")]
        public byte? Rows { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWTextareaControl(RequestContext, For.Model, For.Metadata);
            control.Attributes.Add("data-control", "textarea");
            control.Rows = Rows;

            if (Resizable.HasValue)
                control.AllowResize(Resizable.Value);

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWTextboxTagHelper class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWTextareaTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }
    }
}
