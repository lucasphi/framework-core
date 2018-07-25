using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework password tag helper.
    /// </summary>
    [HtmlTargetElement("password")]
    public class FWPasswordTagHelper : FWInputControlTagHelper
    {
        /// <summary>
        /// Gets or sets the textbox icon.
        /// </summary>
        [HtmlAttributeName("asp-icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the textbox placeholder.
        /// </summary>
        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the password to validate.
        /// </summary>
        [HtmlAttributeName("asp-validate")]
        public ModelExpression ValidateFor { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            FWTextboxControl control;
            if (ValidateFor == null)
            {
                control = new FWTextboxControl(RequestContext, For.Model, For.Metadata);
                control.Attributes.Add("data-control", "textbox");
            }
            else
            {
                control = new FWTextboxControl(RequestContext, null, ValidateFor.Metadata);
                control.Validation();
            }
            
            control.Password();

            if (!string.IsNullOrWhiteSpace(Icon))
                control.Icon(Icon);

            if (!string.IsNullOrWhiteSpace(Placeholder))
                control.PlaceHolder(Placeholder);

            return control;
        }

        /// <summary>
        /// Executes the Microsoft.AspNetCore.Razor.TagHelpers.TagHelper with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For == null)
                For = ValidateFor;

            base.Process(context, output);
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWTextboxTagHelper class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWPasswordTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }
    }
}
