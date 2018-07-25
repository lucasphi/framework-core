using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework select tag helper.
    /// </summary>
    [HtmlTargetElement("select")]
    public class FWSelectTagHelper : FWInputControlTagHelper
    {
        /// <summary>
        /// Gets or sets if the select allows multiple selections.
        /// </summary>
        [HtmlAttributeName("asp-multiple")]
        public bool? Multiple { get; set; }

        /// <summary>
        /// Gets or sets if the select displays the search input.
        /// </summary>
        [HtmlAttributeName("asp-hidesearch")]
        public bool? HideSearch { get; set; } = true;

        /// <summary>
        /// Gets or sets the number of results required to display the search field. Negative numbers will hide it.
        /// </summary>
        [HtmlAttributeName("asp-minimumresultsforsearch")]
        public sbyte? MinimumResultsForSearch { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            var select = new FWSelectControl(RequestContext, For.Model, For.Metadata);
            select.Attributes.Add("data-control", "select");

            if (Multiple.HasValue)
                select.Multiple(Multiple.Value);

            if (HideLabel.HasValue)
                select.HideLabel(HideLabel.Value);

            if (HideSearch.HasValue)
                select.HideSearch(HideSearch.Value);

            if (MinimumResultsForSearch.HasValue)
                select.MinimumResultsForSearch(MinimumResultsForSearch.Value);            
            return select;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWTextboxTagHelper"/> class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWSelectTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }

    }
}
