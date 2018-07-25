using Framework.Core;
using Framework.Web.Mvc.Html.Controls.Forms.Textbox;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Currency control.
    /// </summary>
    public class FWCurrencyControl : FWInputControl
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
            element.DataType = "fw-currency";
            element.AddCssClass("m-form__group form-group");

            if (DisplayLabel)
            {
                FWLabelControl label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                label.AddCssClass("control-label");
                element.Add(label.ToString());
            }
            FWInputElement input = CreateInput();

            if (_icon != null)
            {
                element.Add(_icon.Create(input).ToString());
            }
            else
            {
                element.Add(input);
            }

            return element;
        }

        private FWInputElement CreateInput()
        {
            var input = new FWInputElement(Name, FWInputType.Textbox);
            input.AddCssClass("form-control");

            input.Attributes.Add("data-thousands", CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);
            input.Attributes.Add("data-decimal", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            input.Attributes.Add("data-allownegative", _allowNegative ? "true" : "false");

            input.Attributes.Add("data-rule-required", IsRequired.ToString().ToLower());
            input.Attributes.Add("data-msg-required", string.Format(ViewResources.Validation_Required, DisplayName));

            if (IsReadOnly)
            {
                input.Attributes.Add("readonly", "readonly");
            }

            if (_displayCurrency)
            {
                input.Attributes.Add("data-prefix", CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol);
            }

            if (DataBind)
            {
                DataBind.AddMainBind(FWBindConfiguration.CURRENCY);
                input.DataBind = DataBind.CreateBind();
            }
            else if (Model != null)
            {
                input.Value = Model.ToString();
            }

            return input;
        }

        /// <summary>
        /// Sets an image for the currency control.
        /// </summary>
        /// <param name="icon">The image name.</param>
        /// <param name="background">Informs if the icon has a background or not.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWCurrencyControl Icon(string icon = "fa fa-money", bool background = false)
        {
            _icon = new FWTextboxIcon(icon);
            return this;
        }

        /// <summary>
        /// Configures if the control label should be displayed or not.
        /// </summary>
        /// <param name="hideLabel">True to hide the label.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWCurrencyControl HideLabel(bool hideLabel = true)
        {
            DisplayLabel = !hideLabel;
            return this;
        }

        /// <summary>
        /// Allows the control to hold negative numbers.
        /// </summary>
        /// <param name="allowNegative">Should the control allow negative numbers or not.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWCurrencyControl AllowNegative(bool allowNegative = true)
        {
            _allowNegative = allowNegative;
            return this;
        }

        /// <summary>
        /// Configures the control to display the currency symbol.
        /// </summary>
        /// <param name="displayCurrency"></param>
        /// <returns>The fluent configurator object.</returns>
        public FWCurrencyControl DisplayCurrency(bool displayCurrency = true)
        {
            _displayCurrency = displayCurrency;
            return this;
        }

        /// <summary>
        /// Creates a new Currency control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWCurrencyControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        {
            if (ModelType != FWKnownTypes.Decimal)
            {
                throw new ArgumentException("Currency must use decimal properties.");
            }
        }

        private FWTextboxIcon _icon;
        private bool _allowNegative;
        private bool _displayCurrency;
    }
}
