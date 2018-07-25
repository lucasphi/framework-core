using Framework.Core;
using Framework.Web.Mvc.Html.Elements;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Hidden control.
    /// </summary>
    public class FWHiddenControl : FWInputControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var input = new FWInputElement(Name, FWInputType.Hidden);
            input.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                input.AddCssClass(CustomCss);

            input.Id = Id;
            input.DataType = "fw-hidden";

            if (DataBind)
            {
                DataBind.AddMainBind(FWBindConfiguration.VALUE);
                input.DataBind = DataBind.CreateBind();
            }
            else if (Model != null)
            {
                input.Value = Model.ToString();
            }
            else if (FWReflectionHelper.IsNumeric(ModelType))
            {
                // If the property is a number, but the model is null, the value defaults to 0.
                input.Value = "0";
            }

            return input;
        }

        /// <summary>
        /// Creates a new Hidden control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWHiddenControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        { }
    }
}
