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
    [HtmlTargetElement("textbox")]
    public class FWTextboxTagHelper : FWInputControlTagHelper
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
        /// Gets or sets the autocomplete url datasource.
        /// </summary>
        [HtmlAttributeName("asp-autocomplete")]
        public string Autocomplete { get; set; }

        /// <summary>
        /// Gets or sets the maximum results to be displayed by the autocomplete.
        /// </summary>
        [HtmlAttributeName("asp-results")]
        public int Results { get; set; } = 10;

        /// <summary>
        /// Gets or sets if the autocomplete will force the user to select a valid option.
        /// </summary>
        [HtmlAttributeName("asp-force-selection")]
        public bool ForceSelection { get; set; } = false;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            FWTextboxControl control = new FWTextboxControl(RequestContext, For.Model, For.Metadata);
            control.Attributes.Add("data-control", "textbox");
            
            if (!string.IsNullOrWhiteSpace(Icon))
                control.Icon(Icon);

            if (!string.IsNullOrWhiteSpace(Placeholder))
                control.PlaceHolder(Placeholder);

            if (Autocomplete != null)
            {
                control.Autocomplete(Autocomplete, Results, ForceSelection);
            }

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWTextboxTagHelper class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWTextboxTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }
    }
}
