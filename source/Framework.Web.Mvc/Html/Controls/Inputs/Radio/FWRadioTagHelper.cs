using Framework.Model;
using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework radio tag helper.
    /// </summary>
    [HtmlTargetElement("radio")]
    public class FWRadioTagHelper : FWInputControlTagHelper
    {
        /// <summary>
        /// Gets or sets the control datasource.
        /// </summary>
        [HtmlAttributeName("asp-datasource")]
        public ModelExpression DataSource { get; set; }

        /// <summary>
        /// Gets or sets the items display orientation.
        /// </summary>
        [HtmlAttributeName("asp-orientation")]
        public FWOrientation? Orientation { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            FWRadioControl radio;

            if (DataSource == null)
            {
                radio = new FWRadioControl(RequestContext, For.Model, For.Metadata);
            }
            else
            {
                var datasource = DataSource.Model as IEnumerable<FWDatasourceItem>;
                if (datasource == null)
                    throw new ArgumentException(nameof(DataSource));

                radio = new FWRadioControl(RequestContext, For.Model, For.Metadata, datasource);
            }

            if (Orientation.HasValue)
                radio.Orientation(Orientation.Value);

            radio.Attributes.Add("data-control", "radio");
            return radio;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWRadioTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWRadioTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }
    }
}
