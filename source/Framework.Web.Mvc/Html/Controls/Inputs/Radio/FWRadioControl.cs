using Framework.Core;
using Framework.Model;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Models;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Radio control.
    /// </summary>
    public class FWRadioControl : FWInputControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns></returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement();
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Id = Id;
            element.DataType = "fw-radiobutton";
            element.AddCssClass("m-form__group form-group");

            if (_isBoolean)
            {
                element.Attributes.Add("data-text-true", ViewResources.Yes);
                element.Attributes.Add("data-text-false", ViewResources.No);
            }

            if (DisplayLabel)
            {
                var label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                element.Add(label.ToString());
            }

            var innerDiv = new FWDivElement();
            innerDiv.AddCssClass((_orientation == FWOrientation.Vertical) ? "m-radio-list" : "m-radio-inline");

            for (int i = 0; i < _values.Count; i++)
            {
                innerDiv.Add(CreateRadiobutton(_values.ElementAt(i).Key, _values.ElementAt(i).Value, $"{Name}_{i}"));
            }

            element.Add(innerDiv);

            return element;
        }

        private FWLabelElement CreateRadiobutton(string key, string value, string id)
        {
            var label = new FWLabelElement();
            label.AddCssClass("m-radio");

            var radiobutton = new FWRadiobuttonElement(Name)
            {
                Id = id,
                IsChecked = _selected.ToLower() == key.ToLower(),
                Value = key
            };
            radiobutton.Attributes.Add("data-rule-required", IsRequired.ToString().ToLower());
            radiobutton.Attributes.Add("data-msg-required", ViewResources.Validation_Required_Selection);

            if (IsDisabled)
            {
                label.AddCssClass("m-radio--disabled");
                radiobutton.Attributes.Add("disabled", "disabled");
            }

            if (DataBind)
            {
                DataBind.AddMainBind(FWBindConfiguration.CHECKED);
                radiobutton.DataBind = DataBind.CreateBind();
            }

            label.Add(radiobutton);
            label.Add(value);
            label.Add(new FWSpanElement());

            return label;
        }

        /// <summary>
        /// Sets the radiobutton aligment.
        /// </summary>
        /// <param name="orientation">The aligment.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWRadioControl Orientation(FWOrientation orientation)
        {
            _orientation = orientation;
            return this;
        }

        /// <summary>
        /// Configures if the control label should be displayed or not.
        /// </summary>
        /// <param name="hideLabel">True to hide the label.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWRadioControl HideLabel(bool hideLabel = true)
        {
            DisplayLabel = !hideLabel;
            return this;
        }

        /// <summary>
        /// Creates a new Radio control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWRadioControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        {
            if (FWReflectionHelper.CheckType<bool>(metadata.ModelType))
            {
                _isBoolean = true;
                _values = new Dictionary<string, string>
                {
                    { "true", GetModelResource("TRUE") },
                    { "false", GetModelResource("FALSE") }
                };
                if (model != null)
                    _selected = model.ToString();
            }
            else if (FWReflectionHelper.IsEnum(metadata.ModelType))
            {
                var enumDictionary = FWEnumHelper.GetEnumAsDictonary(metadata.ModelType);
                _values = new Dictionary<string, string>();
                // Gets the resource values for the enum items.
                foreach (var item in enumDictionary)
                {
                    _values.Add(item.Key, GetModelResource(item.Value));
                }
                _selected = FWEnumHelper.GetValues(model as Enum).FirstOrDefault();
            }
            else
            {
                throw new FWMissingDatasourceException(Id);
            }
        }

        /// <summary>
        /// Creates a new Radio control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="datasource">The radiobutton datasource.</param>
        public FWRadioControl(FWRequestContext requestContext, object model, ModelMetadata metadata, IEnumerable<FWDatasourceItem> datasource)
            : base(requestContext, model, metadata)
        {
            _values = new Dictionary<string, string>();
            foreach (var item in datasource)
            {
                _values.Add(item.Id, item.Value);
            }

            if (model != null)
                _selected = model.ToString();
        }

        private string _selected = ""; //Initializes the _selected variable in case of null models.
        private Dictionary<string, string> _values;

        private FWOrientation _orientation = FWOrientation.Vertical;

        private bool _isBoolean;
    }
}
