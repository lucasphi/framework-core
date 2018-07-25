using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework input control base class.
    /// </summary>
    public abstract class FWInputControlTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the model expression for the control property.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// When used inside a template, gets or sets the input template property name.
        /// </summary>
        [HtmlAttributeName("asp-name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets if the input should display or hide its label.
        /// </summary>
        [HtmlAttributeName("asp-hidelabel")]
        public bool? HideLabel { get; set; }

        /// <summary>
        /// Gets or sets if the input is disabled.
        /// </summary>
        [HtmlAttributeName("disabled")]
        public bool? IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets if the input is readonly.
        /// </summary>
        [HtmlAttributeName("readonly")]
        public bool? IsReadonly { get; set; }

        /// <summary>
        /// Gets or sets the input tooltip
        /// </summary>
        [HtmlAttributeName("asp-tooltip")]
        public string Tooltip { get; set; }

        /// <summary>
        /// Creates the input framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns></returns>
        protected abstract FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output);

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected sealed override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            // TODO: template of input control is still experimental and might need fixing.
            IFWTemplateOptions templateOptions = null;
            if (context.IsInnerContent() && Name != null)
            {
                templateOptions = context.Items["TemplateOptions"] as IFWTemplateOptions;
                var metadata = RequestContext.MetadataProvider.GetMetadataForType(templateOptions.ItemType);
                For = new ModelExpression(Name, new ModelExplorer(RequestContext.MetadataProvider, metadata, $"{{{Name}}}"));
            }

            var control = RenderInputControl(context, output);

            if (templateOptions != null)
            {
                control.Id = $"{templateOptions.Id}_{Name}_{{FWTemplateIndex}}";
                control.Name = $"{templateOptions.Id}.{Name}"; //$"{templateOptions.Id}.{Name}[{{Index}}]";
            }

            if (HideLabel.HasValue)
                control.DisplayLabel = !HideLabel.Value;

            if (IsDisabled.HasValue)
                control.IsDisabled = IsDisabled.Value;

            if (IsReadonly.HasValue)
                control.IsReadOnly = IsReadonly.Value;

            control.Tooltip = Tooltip;

            return control;
        }

        /// <summary>
        /// Executes the Microsoft.AspNetCore.Razor.TagHelpers.TagHelper with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For == null && Name == null)
                throw new NullReferenceException($"The tag {output.TagName} is missing attribute 'For'");

            base.Process(context, output);
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWInputControlTagHelper class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        protected FWInputControlTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor,
                                            IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }

        /// <summary>
        /// Gets or set the current view context.
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { set => RequestContext.Model = value.ViewData.Model; }
    }
}
