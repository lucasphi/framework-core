using Framework.Core;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Html.Controls.Forms.Textbox;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Textbox control.
    /// </summary>
    public class FWTextboxControl : FWInputControl
    {
        /// <summary>
        /// Configures the control based on the model metadata.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected override void ReadMetadata(ModelMetadata metadata)
        {
            base.ReadMetadata(metadata);

            if (metadata.AdditionalValues.ContainsKey(nameof(FWStringLengthAttribute)))
            {
                var strLengthAttr = (metadata.AdditionalValues[nameof(FWStringLengthAttribute)] as FWStringLengthAttribute);
                _maxLength = strLengthAttr.MaximumLength;
                _minLength = strLengthAttr.MinimumLength;
            }

            if (metadata.AdditionalValues.ContainsKey(nameof(FWRegexAttribute)))
            {
                var regexAttr = (metadata.AdditionalValues[nameof(FWRegexAttribute)] as FWRegexAttribute);
                _regexPattern = regexAttr.Pattern;
            }
        }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            FWDivElement container;
            var element = container = new FWDivElement();
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Id = Id;
            element.DataType = "fw-textbox";
            element.AddCssClass("m-form__group form-group");

            if (DisplayLabel)
            {
                FWLabelControl label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                label.AddCssClass("control-label");
                element.Add(label.ToString());
            }

            if (_autocompleteUrl != null)
            {
                ConfigureAutocomplete(element);

                // Adds a div wrapper to the control.
                container = element.Add(new FWDivElement());
                container.AddCssClass("m-typeahead");
            }

            FWInputElement input = CreateInput();

            if (!string.IsNullOrWhiteSpace(_mask))
            {
                ConfigureMask(input);
            }

            if (_icon != null)
            {
                container.Add(_icon.Create(input).ToString());
            }
            else
            {
                container.Add(input);
            }

            return element;
        }

        private FWInputElement CreateInput()
        {
            var input = new FWInputElement(Name, (!_isPassword) ? FWInputType.Textbox : FWInputType.Password);
            input.AddCssClass("form-control");

            if (!string.IsNullOrWhiteSpace(_placeholder))
                input.Attributes.Add("placeholder", _placeholder);

            input.Attributes.Add("data-rule-required", IsRequired.ToString().ToLower());
            input.Attributes.Add("data-msg-required", string.Format(ViewResources.Validation_Required, DisplayName));

            if (IsReadOnly)
                input.Attributes.Add("readonly", "readonly");

            if (IsDisabled)
                input.Attributes.Add("disabled", "disabled");

            if (_maxLength > 0)
            {
                input.Attributes.Add("maxlength", _maxLength.ToString());
            }

            if (_minLength > 0)
            {
                input.Attributes.Add("data-rule-minlength", _minLength.ToString());
                input.Attributes.Add("data-msg-minlength", string.Format(ViewResources.Validation_MinLength, DisplayName, _minLength));
            }

            if (_regexPattern != null)
            {
                input.Attributes.Add("data-rule-pattern", _regexPattern);
                input.Attributes.Add("data-msg-pattern", string.Format(ViewResources.Validation_Regex, DisplayName));
            }

            if (_targetValidationField != null)
            {
                input.Attributes.Add("data-rule-passwordmatch", _targetValidationField);
                input.Attributes.Add("data-msg-passwordmatch", ViewResources.Validation_PasswordMismatch);
            }

            if (DataBind)
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

        private void ConfigureAutocomplete(FWDivElement element)
        {
            element.Attributes.Add("data-autocomplete-url", _autocompleteUrl);
            element.Attributes.Add("data-autocomplete-limit", _autocompleteLimit.ToString());
            if (_autocompleteForceSelection)
                element.Attributes.Add("data-autocomplete-force", "true");

            FWInputElement hidden = new FWInputElement($"{Id}Value", FWInputType.Hidden);
            element.Add(hidden);

            // TODO: Metronic 5 does not allow icons inside autocomplete combos yet
            //if (_icon == null)
            //    Icon("fa fa-search");
        }

        private void ConfigureMask(FWInputElement input)
        {
            input.Attributes.Add("data-mask", _mask);
            input.Attributes.Add("data-mask-reverse", _reversemask.ToString().ToLower());
        }

        /// <summary>
        /// Configures the textbox as a password textbox.
        /// </summary>
        /// <param name="isPassword">Defines is the textbox is a password or not.</param>
        /// <returns>The fluent configurator object.</returns>
        internal FWTextboxControl Password(bool isPassword = true)
        {
            _isPassword = isPassword;
            if (isPassword)
                Icon("fa fa-lock");
            return this;
        }

        /// <summary>
        /// Sets an image for the textbox.
        /// </summary>
        /// <param name="icon">The image name.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTextboxControl Icon(string icon)
        {
            _icon = new FWTextboxIcon(icon);
            return this;
        }

        /// <summary>
        /// Sets the control placeholder.
        /// </summary>
        /// <param name="placeholder">The placeholder text.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTextboxControl PlaceHolder(string placeholder = null)
        {
            if (placeholder != null)
            {
                _placeholder = placeholder;
            }
            else
            {
                _placeholder = GetModelResource("Placeholder");
            }
            return this;
        }

        /// <summary>
        /// Configures if the control label should be displayed or not.
        /// </summary>
        /// <param name="hideLabel">True to hide the label.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTextboxControl HideLabel(bool hideLabel = true)
        {
            DisplayLabel = !hideLabel;
            return this;
        }

        /// <summary>
        /// Configures the control to be a validation field for target password.
        /// </summary>
        /// <param name="targetField">The target password id.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTextboxControl Validation(string targetField = null)
        {
            _targetValidationField = targetField ?? Id;
            Name = Name + "_Validate";
            Id = Id + "_Validate";
            DisplayName = FWStringLocalizer.GetModelResource(Name, ContainerType.Name);
            return this;
        }

        /// <summary>
        /// Adds an autocomplete to the textbox.
        /// </summary>
        /// <param name="datasource">The autocomplete datasource url.</param>
        /// <param name="limit">The max result limit.</param>
        /// <param name="forceSelection">Forces a valid selection.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTextboxControl Autocomplete(string datasource, int limit = 10, bool forceSelection = true)
        {
            _autocompleteUrl = FWHttpUtility.UpdateQueryString(datasource, "query", "~QUERY");
            _autocompleteLimit = limit;
            _autocompleteForceSelection = forceSelection;
            return this;
        }

        /// <summary>
        /// Creates a new Textbox control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWTextboxControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(nameof(FWMaskAttribute)))
            {
                var mask = (metadata.AdditionalValues[nameof(FWMaskAttribute)] as FWMaskAttribute);
                _mask = mask.Mask;
                _reversemask = mask.Reverse;
            }

            if (_regexPattern == null)
            {
                if (FWReflectionHelper.IsNumeric(ModelType))
                {
                    _regexPattern = (FWReflectionHelper.IsIntegral(ModelType)) ? @"^\d+$" : $@"^[0-9]\d*(\{CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator}\d*)?$";
                }
            }
        }

        private FWTextboxIcon _icon;
        private bool _isPassword = false;
        private string _placeholder;

        private int _maxLength;
        private int _minLength;
        private string _regexPattern;
        private string _mask;
        private bool _reversemask;
        private string _targetValidationField;

        private string _autocompleteUrl;
        private int _autocompleteLimit;
        private bool _autocompleteForceSelection;
    }
}
