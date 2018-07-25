using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework radio tag helper.
    /// </summary>
    [HtmlTargetElement("datepicker")]
    public class FWDatepickerTagHelper : FWInputControlTagHelper
    {
        /// <summary>
        /// Gets or sets the textbox placeholder.
        /// </summary>
        [HtmlAttributeName("asp-placeholder")]
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets if the control has a time picker.
        /// </summary>
        [HtmlAttributeName("asp-timepicker")]
        public bool? TimePicker { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWDatepickerControl(RequestContext, For.Model, For.Metadata);
            control.Attributes.Add("data-control", "datepicker");

            if (!string.IsNullOrWhiteSpace(Placeholder))
                control.PlaceHolder(Placeholder);

            if (TimePicker.HasValue)
                control.TimePicker(TimePicker.Value);

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWDatepickerTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWDatepickerTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }
    }
}
