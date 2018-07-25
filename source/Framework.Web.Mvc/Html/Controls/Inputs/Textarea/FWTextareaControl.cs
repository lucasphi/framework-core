using Framework.Web.Mvc.Html.Elements;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Textarea control.
    /// </summary>
    public class FWTextareaControl : FWInputControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement();
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Id = Id;
            element.DataType = "fw-textarea";
            element.AddCssClass("m-form__group form-group");

            if (DisplayLabel)
            {
                var label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                label.AddCssClass("control-label");
                element.Add(label.ToString());
            }

            FWTextareaElement input = CreateInput();

            element.Add(input);

            return element;
        }

        private FWTextareaElement CreateInput()
        {
            var input = new FWTextareaElement(Name);
            input.AddCssClass("form-control");

            if (!_resizable)
            {
                input.AddCssClass("textarea-fixed");
            }

            if (Rows.HasValue)
            {
                input.Attributes.Add("rows", Rows.Value.ToString());
            }

            if (DataBind != null)
            {
                DataBind.AddMainBind(FWBindConfiguration.VALUE);
                input.DataBind = DataBind.CreateBind();
            }
            else if (Model != null)
            {
                input.Value = Model.ToString();
            }

            return input;
        }

        /// <summary>
        /// Allows the textarea to be resized.
        /// </summary>
        /// <param name="resizable">Informs if the control is resizable or not.</param>
        /// <returns>The fluent configuration.</returns>
        public FWTextareaControl AllowResize(bool resizable = true)
        {
            _resizable = true;
            return this;
        }

        /// <summary>
        /// Creates a new Textarea control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWTextareaControl(FWRequestContext requestContext, object model, ModelMetadata metadata) 
            : base(requestContext, model, metadata)
        { }

        /// <summary>
        /// Gets or sets the number of rows.
        /// </summary>
        public byte? Rows { get; set; }

        private bool _resizable;
    }
}
